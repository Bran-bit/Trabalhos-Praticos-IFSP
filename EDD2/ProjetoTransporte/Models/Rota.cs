namespace ProjetoTransporte.Models;

/// <summary>Agrupa as viagens de uma origem para um destino, com totais prontos (consultas O(1)).</summary>
public class Rota
{
    private readonly List<Viagem> _viagens = new();

    public Garagem Origem { get; }
    public Garagem Destino { get; }
    public IReadOnlyList<Viagem> Viagens => _viagens;
    public int QuantidadeViagens => _viagens.Count;
    public int TotalPassageiros { get; private set; }

    public Rota(Garagem origem, Garagem destino)
    {
        Origem = origem;
        Destino = destino;
    }

    public void Registrar(Viagem viagem)
    {
        _viagens.Add(viagem);
        TotalPassageiros += viagem.Passageiros;
    }
}
