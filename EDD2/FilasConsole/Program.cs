using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FilasConsole.Models;
using FilasConsole.Services;

namespace FilasConsole;

public static class Program
{
    private static readonly Cadastro cadastro = new();

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        cadastro.Download();

        try
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== CONTROLE DE ACESSOS =====");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar ambiente");
                Console.WriteLine("2. Consultar ambiente");
                Console.WriteLine("3. Excluir ambiente");
                Console.WriteLine("4. Cadastrar usuário");
                Console.WriteLine("5. Consultar usuário");
                Console.WriteLine("6. Excluir usuário");
                Console.WriteLine("7. Conceder permissão de acesso ao usuário");
                Console.WriteLine("8. Revogar permissão de acesso ao usuário");
                Console.WriteLine("9. Registrar acesso");
                Console.WriteLine("10. Consultar logs de acesso");
                Console.Write("Opção: ");

                var entrada = Console.ReadLine();

                /* Entrada encerrada : sai do laço normalmente; o finally ainda executa
                 Upload antes de terminar.
                */
                if (entrada is null)
                {
                    Console.WriteLine();
                    break;
                }

                if (!int.TryParse(entrada, out int opcao))
                {
                    Console.WriteLine("Opção inválida.");
                    continue;
                }

                if (opcao == 0)
                    break;

                switch (opcao)
                {
                    case 1: CadastrarAmbiente(); break;
                    case 2: ConsultarAmbiente(); break;
                    case 3: ExcluirAmbiente(); break;
                    case 4: CadastrarUsuario(); break;
                    case 5: ConsultarUsuario(); break;
                    case 6: ExcluirUsuario(); break;
                    case 7: ConcederPermissao(); break;
                    case 8: RevogarPermissao(); break;
                    case 9: RegistrarAcesso(); break;
                    case 10: ConsultarLogs(); break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }
        finally
        {
            cadastro.Upload();
            Console.WriteLine("Dados salvos. Encerrando...");
        }
    }

    private static void CadastrarAmbiente()
    {
        Console.Write("Nome do ambiente: ");
        var nome = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome obrigatório.");
            return;
        }

        if (cadastro.ExisteAmbienteComNome(nome))
            Console.WriteLine("Aviso: já existe um ambiente com esse nome. Prosseguindo mesmo assim.");

        var ambiente = new Ambiente { Nome = nome };
        cadastro.AdicionarAmbiente(ambiente);

        Console.WriteLine($"Ambiente cadastrado com ID {ambiente.Id}.");
    }

    private static void ConsultarAmbiente()
    {
        Console.Write("ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var ambiente = cadastro.PesquisarAmbiente(new Ambiente { Id = id });

        if (ambiente is null)
        {
            Console.WriteLine("Ambiente não encontrado.");
            return;
        }

        Console.WriteLine($"ID: {ambiente.Id} | Nome: {ambiente.Nome} | Logs: {ambiente.Logs.Count}");
    }

    private static void ExcluirAmbiente()
    {
        Console.Write("ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var ok = cadastro.RemoverAmbiente(new Ambiente { Id = id });

        Console.WriteLine(ok
            ? "Ambiente removido."
            : "Ambiente não encontrado.");
    }

    private static void CadastrarUsuario()
    {
        Console.Write("Nome do usuário: ");
        var nome = Console.ReadLine()?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome obrigatório.");
            return;
        }

        if (cadastro.ExisteUsuarioComNome(nome))
            Console.WriteLine("Aviso: já existe um usuário com esse nome. Prosseguindo mesmo assim.");

        var usuario = new Usuario { Nome = nome };
        cadastro.AdicionarUsuario(usuario);

        Console.WriteLine($"Usuário cadastrado com ID {usuario.Id}.");
    }

    private static void ConsultarUsuario()
    {
        Console.Write("ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var usuario = cadastro.PesquisarUsuario(new Usuario { Id = id });

        if (usuario is null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        Console.WriteLine($"ID: {usuario.Id} | Nome: {usuario.Nome}");

        if (usuario.Ambientes.Count == 0)
            Console.WriteLine("Permissões: nenhuma");
        else
            Console.WriteLine("Permissões: " +
                string.Join(", ", usuario.Ambientes.Select(a => $"{a.Id}-{a.Nome}")));
    }

    private static void ExcluirUsuario()
    {
        Console.Write("ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        var usuario = cadastro.PesquisarUsuario(new Usuario { Id = id });

        if (usuario is null)
        {
            Console.WriteLine("Usuário não encontrado.");
            return;
        }

        var ok = cadastro.RemoverUsuario(usuario);

        if (ok)
            Console.WriteLine("Usuário removido.");
        else
            Console.WriteLine(
                "Não foi possível remover: o usuário ainda possui permissões. " +
                "Revogue-as antes pela opção 8.");
    }

    private static void ConcederPermissao()
    {
        var ambiente = LerAmbiente();
        if (ambiente is null) return;

        var usuario = LerUsuario();
        if (usuario is null) return;

        var ok = usuario.ConcederPermissao(ambiente);

        Console.WriteLine(ok
            ? "Permissão concedida."
            : "Usuário já possui permissão para este ambiente.");
    }

    private static void RevogarPermissao()
    {
        var ambiente = LerAmbiente();
        if (ambiente is null) return;

        var usuario = LerUsuario();
        if (usuario is null) return;

        var ok = usuario.RevogarPermissao(ambiente);

        Console.WriteLine(ok
            ? "Permissão revogada."
            : "Usuário não possui permissão para este ambiente.");
    }

    private static void RegistrarAcesso()
    {
        var ambiente = LerAmbiente();
        if (ambiente is null) return;

        var usuario = LerUsuario();
        if (usuario is null) return;

        bool autorizado = usuario.TemPermissao(ambiente);

        ambiente.RegistrarLog(new Log
        {
            DtAcesso = DateTime.Now,
            UsuarioId = usuario.Id,
            UsuarioNome = usuario.Nome,
            Usuario = usuario,
            TipoAcesso = autorizado
        });

        Console.WriteLine(autorizado
            ? "Acesso AUTORIZADO registrado."
            : "Acesso NEGADO registrado.");
    }

    private static void ConsultarLogs()
    {
        var ambiente = LerAmbiente();
        if (ambiente is null) return;

        Console.WriteLine("Filtro: 1-Todos 2-Autorizados 3-Negados");
        Console.Write("Filtro: ");

        if (!int.TryParse(Console.ReadLine(), out int filtro))
        {
            Console.WriteLine("Filtro inválido.");
            return;
        }

        IEnumerable<Log> logs = ambiente.Logs;

        if (filtro == 2)
            logs = logs.Where(l => l.TipoAcesso);
        else if (filtro == 3)
            logs = logs.Where(l => !l.TipoAcesso);
        else if (filtro != 1)
        {
            Console.WriteLine("Filtro inválido.");
            return;
        }

        var lista = logs.ToList();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum log encontrado.");
            return;
        }

        foreach (var log in lista)
        {
            Console.WriteLine(
                $"{log.DtAcesso:dd/MM/yyyy HH:mm:ss} | " +
                $"Usuário: {log.UsuarioId}-{log.UsuarioNome} | " +
                $"{(log.TipoAcesso ? "AUTORIZADO" : "NEGADO")}");
        }
    }

    private static Ambiente? LerAmbiente()
    {
        Console.Write("ID do ambiente: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        var ambiente = cadastro.PesquisarAmbiente(new Ambiente { Id = id });

        if (ambiente is null)
            Console.WriteLine("Ambiente não encontrado.");

        return ambiente;
    }

    private static Usuario? LerUsuario()
    {
        Console.Write("ID do usuário: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        var usuario = cadastro.PesquisarUsuario(new Usuario { Id = id });

        if (usuario is null)
            Console.WriteLine("Usuário não encontrado.");

        return usuario;
    }
}
