using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FilasConsole.Models;

namespace FilasConsole.Services;

public class Cadastro
{
    private readonly List<Usuario> usuarios = new();
    private readonly List<Ambiente> ambientes = new();
    private readonly string arquivo = Path.Combine(AppContext.BaseDirectory, "dados.json");

    public IReadOnlyList<Usuario> Usuarios => usuarios;
    public IReadOnlyList<Ambiente> Ambientes => ambientes;

    public void AdicionarUsuario(Usuario usuario)
    {
        if (usuario is null) return;

        if (usuario.Id <= 0)
            usuario.Id = ProximoUsuarioId();

        // Salvaguarda: o Id é gerado acima, então na prática nunca colide.
        // A checagem existe só para proteger chamadas diretas ao serviço.
        if (usuarios.Any(u => u.Id == usuario.Id))
            return;

        usuarios.Add(usuario);
    }

    public bool RemoverUsuario(Usuario usuario)
    {
        if (usuario is null) return false;

        var existente = usuarios.FirstOrDefault(u => u.Id == usuario.Id);
        if (existente is null) return false;

        // Usuário só pode ser removido se não tiver nenhuma permissão.
        if (existente.Ambientes.Count > 0) return false;

        return usuarios.Remove(existente);
    }

    public Usuario? PesquisarUsuario(Usuario usuario)
    {
        if (usuario is null) return null;
        return usuarios.FirstOrDefault(u => u.Id == usuario.Id);
    }

    public void AdicionarAmbiente(Ambiente ambiente)
    {
        if (ambiente is null) return;

        if (ambiente.Id <= 0)
            ambiente.Id = ProximoAmbienteId();

        if (ambientes.Any(a => a.Id == ambiente.Id))
            return;

        ambientes.Add(ambiente);
    }

    public bool RemoverAmbiente(Ambiente ambiente)
    {
        if (ambiente is null) return false;

        var existente = ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
        if (existente is null) return false;

        ambientes.Remove(existente);

        // Remove permissões vinculadas ao ambiente excluído.
        foreach (var usuario in usuarios)
            usuario.RevogarPermissao(existente);

        return true;
    }

    public Ambiente? PesquisarAmbiente(Ambiente ambiente)
    {
        if (ambiente is null) return null;
        return ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
    }

    public bool ExisteAmbienteComNome(string nome)
    {
        return !string.IsNullOrWhiteSpace(nome) &&
               ambientes.Any(a => string.Equals(a.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    public bool ExisteUsuarioComNome(string nome)
    {
        return !string.IsNullOrWhiteSpace(nome) &&
               usuarios.Any(u => string.Equals(u.Nome, nome, StringComparison.OrdinalIgnoreCase));
    }

    private int ProximoUsuarioId()
    {
        int maiorUsuario = usuarios.Count == 0 ? 0 : usuarios.Max(u => u.Id);

        int maiorLog = ambientes
            .SelectMany(a => a.Logs)
            .Select(l => l.UsuarioId)
            .DefaultIfEmpty(0)
            .Max();

        return Math.Max(maiorUsuario, maiorLog) + 1;
    }

    private int ProximoAmbienteId()
    {
        return ambientes.Count == 0 ? 1 : ambientes.Max(a => a.Id) + 1;
    }


    public void Upload()
    {
        try
        {
            var dto = new CadastroDto
            {
                Usuarios = usuarios.Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    AmbienteIds = u.Ambientes.Select(a => a.Id).ToList()
                }).ToList(),

                Ambientes = ambientes.Select(a => new AmbienteDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Logs = a.Logs.Select(l => new LogDto
                    {
                        DtAcesso = l.DtAcesso,
                        UsuarioId = l.UsuarioId,
                        UsuarioNome = l.UsuarioNome,
                        TipoAcesso = l.TipoAcesso
                    }).ToList()
                }).ToList()
            };

            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Escrita atômica: grava em .tmp e move por cima.
            // Se o processo cair no meio, o dados.json antigo continua íntegro.
            var temporario = arquivo + ".tmp";
            File.WriteAllText(temporario, json);
            File.Move(temporario, arquivo, overwrite: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
        }
    }

    public void Download()
    {
        if (!File.Exists(arquivo)) return;

        try
        {
            var json = File.ReadAllText(arquivo);
            var dto = JsonSerializer.Deserialize<CadastroDto>(json);
            if (dto is null) return;

            var novosAmbientes = new List<Ambiente>();
            var mapaAmbientes = new Dictionary<int, Ambiente>();

            foreach (var ad in dto.Ambientes)
            {
                var ambiente = new Ambiente { Id = ad.Id, Nome = ad.Nome };
                novosAmbientes.Add(ambiente);
                mapaAmbientes[ambiente.Id] = ambiente;
            }

            var novosUsuarios = new List<Usuario>();
            foreach (var ud in dto.Usuarios)
            {
                var usuario = new Usuario { Id = ud.Id, Nome = ud.Nome };

                foreach (var ambienteId in ud.AmbienteIds)
                {
                    if (mapaAmbientes.TryGetValue(ambienteId, out var ambiente))
                        usuario.Ambientes.Add(ambiente);
                }

                novosUsuarios.Add(usuario);
            }

            // Reconstrói logs apontando para as instâncias já carregadas.
            foreach (var ad in dto.Ambientes)
            {
                if (!mapaAmbientes.TryGetValue(ad.Id, out var ambiente))
                    continue;

                foreach (var ld in ad.Logs)
                {
                    var usuario = novosUsuarios.FirstOrDefault(u => u.Id == ld.UsuarioId);

                    ambiente.RegistrarLog(new Log
                    {
                        DtAcesso = ld.DtAcesso,
                        UsuarioId = ld.UsuarioId,
                        UsuarioNome = ld.UsuarioNome,
                        Usuario = usuario,
                        TipoAcesso = ld.TipoAcesso
                    });
                }
            }

            // Só chega aqui se tudo acima foi bem-sucedido.
            usuarios.Clear();
            usuarios.AddRange(novosUsuarios);
            ambientes.Clear();
            ambientes.AddRange(novosAmbientes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar dados: {ex.Message}");

            // Preserva o original para inspeção/conserto manual.
            try
            {
                File.Copy(arquivo, arquivo + ".bak", overwrite: true);
                Console.WriteLine($"Cópia do arquivo original preservada em {arquivo}.bak");
            }
            catch
            {
                // melhor esforço: se nem copiar der, seguimos em frente
            }
        }
    }

    private class CadastroDto
    {
        public List<UsuarioDto> Usuarios { get; set; } = new();
        public List<AmbienteDto> Ambientes { get; set; } = new();
    }

    private class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<int> AmbienteIds { get; set; } = new();
    }

    private class AmbienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<LogDto> Logs { get; set; } = new();
    }

    private class LogDto
    {
        public DateTime DtAcesso { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; } = string.Empty;
        public bool TipoAcesso { get; set; }
    }
}
