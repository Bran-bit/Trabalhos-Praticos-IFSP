using ProjetoTransporte.Models;

namespace ProjetoTransporte.Services;

/// <summary>
/// Regras de negócio da frota. Não conhece o console: pode ser reutilizada em outra interface.
/// Escalável: qualquer quantidade de veículos e de garagens (aeroportos).
/// </summary>
public class Transportadora
{
    private readonly List<Veiculo> _frota = new();
    private readonly List<Garagem> _garagens = new();
    private readonly Dictionary<(int Origem, int Destino), Rota> _rotas = new();
    private int _proximoNumeroVeiculo = 1;
    private int _proximoIdGaragem = 1;
    private int _sequencialViagem = 1;

    public bool JornadaIniciada { get; private set; }
    public IReadOnlyList<Veiculo> Frota => _frota;
    public IReadOnlyList<Garagem> Garagens => _garagens;

    // ---------- Cadastros (só com a jornada encerrada) ----------

    public Veiculo CadastrarVeiculo(int capacidade)
    {
        ExigirJornadaEncerrada("Cadastro de veículo");
        if (capacidade <= 0)
            throw new DomainException("A capacidade deve ser maior que zero.");

        var veiculo = new Veiculo(_proximoNumeroVeiculo++, capacidade);
        _frota.Add(veiculo);   // fica "aguardando" até o início da jornada, quando é distribuído
        return veiculo;
    }

    public Garagem CadastrarGaragem(string nome)
    {
        ExigirJornadaEncerrada("Cadastro de garagem");
        nome = (nome ?? string.Empty).Trim();
        if (nome.Length == 0)
            throw new DomainException("O nome da garagem é obrigatório.");
        if (_garagens.Any(g => g.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException($"Já existe uma garagem chamada \"{nome}\".");

        var garagem = new Garagem(_proximoIdGaragem++, nome);
        _garagens.Add(garagem);
        return garagem;
    }

    // ---------- Jornada ----------

    /// <summary>Distribui os veículos alternadamente (round-robin) entre as garagens.</summary>
    public void IniciarJornada()
    {
        if (JornadaIniciada)
            throw new DomainException("A jornada já está iniciada.");
        if (_garagens.Count < 2)
            throw new DomainException("Cadastre pelo menos 2 garagens para iniciar a jornada.");
        if (_frota.Count == 0)
            throw new DomainException("Cadastre pelo menos 1 veículo para iniciar a jornada.");

        foreach (var g in _garagens) g.Esvaziar();

        // Distribuição alternada (round-robin): van i vai para a garagem i % N.
        var porGaragem = _garagens.Select(_ => new List<Veiculo>()).ToList();
        for (int i = 0; i < _frota.Count; i++)
            porGaragem[i % _garagens.Count].Add(_frota[i]);

        // Como a garagem é uma pilha (LIFO), empilhamos do maior número para o menor:
        // assim o menor número de cada garagem fica no topo e é o primeiro a sair
        // (Congonhas: Van 01 na frente; Guarulhos: Van 02 na frente).
        for (int g = 0; g < _garagens.Count; g++)
            for (int j = porGaragem[g].Count - 1; j >= 0; j--)
                _garagens[g].Estacionar(porGaragem[g][j]);

        JornadaIniciada = true;
    }

    /// <summary>Gera o relatório veículo a veículo e limpa as ocorrências das viagens.</summary>
    public IReadOnlyList<RelatorioVeiculo> EncerrarJornada()
    {
        if (!JornadaIniciada)
            throw new DomainException("A jornada não está iniciada.");

        var relatorio = _frota
            .Select(v => new RelatorioVeiculo(v.Numero, v.ViagensNaJornada, v.PassageirosNaJornada))
            .ToList();

        foreach (var v in _frota) v.ZerarJornada();
        foreach (var g in _garagens) g.Esvaziar();   // veículos voltam ao "pátio" até a próxima jornada
        _rotas.Clear();
        _sequencialViagem = 1;
        JornadaIniciada = false;

        return relatorio;
    }

    // ---------- Viagens ----------

    /// <summary>
    /// Libera o veículo da frente da garagem de origem. Ao chegar, ele é estacionado no destino.
    /// A viagem sai com a lotação completa.
    /// </summary>
    public Viagem LiberarViagem(Garagem origem, Garagem destino)
    {
        if (!JornadaIniciada)
            throw new DomainException("A jornada diária ainda não foi iniciada.");
        if (origem == destino)
            throw new DomainException("Origem e destino devem ser diferentes.");
        if (origem.EstaVazia)
            throw new DomainException(
                $"A garagem {origem.Nome} está vazia. Nova viagem só pode sair dela quando um veículo retornar.");

        var veiculo = origem.Liberar();
        int passageiros = veiculo.Capacidade;
        veiculo.RegistrarViagem(passageiros);
        destino.Estacionar(veiculo);

        var viagem = new Viagem(_sequencialViagem++, origem, destino, veiculo, passageiros, DateTime.Now);
        ObterOuCriarRota(origem, destino).Registrar(viagem);
        return viagem;
    }

    // ---------- Consultas ----------

    public Garagem? BuscarGaragem(int id) => _garagens.FirstOrDefault(g => g.Id == id);

    public int QuantidadeViagens(Garagem origem, Garagem destino) =>
        _rotas.TryGetValue((origem.Id, destino.Id), out var r) ? r.QuantidadeViagens : 0;

    public IReadOnlyList<Viagem> ListarViagens(Garagem origem, Garagem destino) =>
        _rotas.TryGetValue((origem.Id, destino.Id), out var r) ? r.Viagens : Array.Empty<Viagem>();

    public int TotalPassageiros(Garagem origem, Garagem destino) =>
        _rotas.TryGetValue((origem.Id, destino.Id), out var r) ? r.TotalPassageiros : 0;

    // ---------- Internos ----------

    private Rota ObterOuCriarRota(Garagem origem, Garagem destino)
    {
        var chave = (origem.Id, destino.Id);
        if (!_rotas.TryGetValue(chave, out var rota))
            _rotas[chave] = rota = new Rota(origem, destino);
        return rota;
    }

    private void ExigirJornadaEncerrada(string operacao)
    {
        if (JornadaIniciada)
            throw new DomainException($"{operacao} só é permitido com a jornada diária encerrada.");
    }
}
