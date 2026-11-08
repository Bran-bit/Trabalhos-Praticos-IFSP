using System.Collections.Generic;
using System.Linq;

namespace MedicamentosConsole.Models;

public class Medicamento
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Laboratorio { get; set; } = string.Empty;
    public Queue<Lote> Lotes { get; set; } = new();

    public Medicamento() { }

    public Medicamento(int id, string nome, string laboratorio)
    {
        Id = id;
        Nome = nome;
        Laboratorio = laboratorio;
    }

    public int QtdeDisponivel()
    {
        return Lotes.Sum(l => l.Qtde);
    }

    public void Comprar(Lote lote)
    {
        if (lote is null) return;

        Lotes.Enqueue(lote);
    }

    public bool Vender(int qtde)
    {
        if (qtde <= 0) return false;
        if (qtde > QtdeDisponivel()) return false;

        int restante = qtde;

        while (restante > 0)
        {
            var lote = Lotes.Peek();

            if (lote.Qtde <= restante)
            {
                restante -= lote.Qtde;
                Lotes.Dequeue();
            }
            else
            {
                lote.Qtde -= restante;
                restante = 0;
            }
        }

        return true;
    }

    public override string ToString()
    {
        return $"{Id}-{Nome}-{Laboratorio}-{QtdeDisponivel()}";
    }

    public override bool Equals(object? obj)
    {
        return obj is Medicamento outro && outro.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
