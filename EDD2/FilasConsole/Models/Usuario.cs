using System.Collections.Generic;
using System.Linq;

namespace FilasConsole.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<Ambiente> Ambientes { get; set; } = new();

    public Usuario() { }

    public Usuario(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public bool ConcederPermissao(Ambiente ambiente)
    {
        if (ambiente is null) return false;
        if (Ambientes.Any(a => a.Id == ambiente.Id)) return false;

        Ambientes.Add(ambiente);
        return true;
    }

    public bool RevogarPermissao(Ambiente ambiente)
    {
        if (ambiente is null) return false;

        var existente = Ambientes.FirstOrDefault(a => a.Id == ambiente.Id);
        if (existente is null) return false;

        Ambientes.Remove(existente);
        return true;
    }

    public bool TemPermissao(Ambiente ambiente)
    {
        return ambiente is not null && Ambientes.Any(a => a.Id == ambiente.Id);
    }
}
