namespace ProjetoTransporte.Models;

/// <summary>
/// Garagem com um único acesso: o último veículo estacionado é o primeiro a sair (LIFO).
/// Usa a Stack&lt;T&gt; nativa do C#.
/// </summary>
public class Garagem
{
    private readonly Stack<Veiculo> _veiculos = new();

    public int Id { get; }
    public string Nome { get; }

    public int Quantidade => _veiculos.Count;
    public int PotencialTransporte { get; private set; }   // soma das capacidades (mantido incrementalmente, O(1))
    public bool EstaVazia => _veiculos.Count == 0;

    /// <summary>Veículos do topo (próximo a sair) até o fundo da pilha.</summary>
    public IEnumerable<Veiculo> Veiculos => _veiculos;

    public Garagem(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    /// <summary>Estaciona de ré: o veículo passa a ser o primeiro da fila de saída.</summary>
    public void Estacionar(Veiculo veiculo)
    {
        _veiculos.Push(veiculo);
        PotencialTransporte += veiculo.Capacidade;
    }

    /// <summary>Retira o veículo posicionado na frente (topo da pilha).</summary>
    public Veiculo Liberar()
    {
        var veiculo = _veiculos.Pop();
        PotencialTransporte -= veiculo.Capacidade;
        return veiculo;
    }

    public Veiculo? ProximoASair => _veiculos.TryPeek(out var v) ? v : null;

    public void Esvaziar()
    {
        _veiculos.Clear();
        PotencialTransporte = 0;
    }

    public override string ToString() => Nome;
}
