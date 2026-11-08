using System;

namespace MedicamentosConsole.Models;

public class Lote
{
    public int Id { get; set; }
    public int Qtde { get; set; }
    public DateTime Venc { get; set; }

    public Lote() { }

    public Lote(int id, int qtde, DateTime venc)
    {
        Id = id;
        Qtde = qtde;
        Venc = venc;
    }

    public override string ToString()
    {
        return $"{Id}-{Qtde}-{Venc:dd/MM/yyyy}";
    }
}
