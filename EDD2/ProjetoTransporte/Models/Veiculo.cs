namespace ProjetoTransporte.Models;

/// <summary>Van da frota. O número é gerado pelo sistema (1, 2, 3, ...).</summary>
public class Veiculo
{
    public int Numero { get; }
    public int Capacidade { get; }                       // lotação / potencial de transporte
    public int ViagensNaJornada { get; private set; }
    public int PassageirosNaJornada { get; private set; }

    public Veiculo(int numero, int capacidade)
    {
        if (capacidade <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacidade), "A capacidade deve ser maior que zero.");

        Numero = numero;
        Capacidade = capacidade;
    }

    /// <summary>Registra uma viagem concluída (lotação completa).</summary>
    public void RegistrarViagem(int passageiros)
    {
        ViagensNaJornada++;
        PassageirosNaJornada += passageiros;
    }

    /// <summary>"Limpeza" das ocorrências das viagens anteriores.</summary>
    public void ZerarJornada()
    {
        ViagensNaJornada = 0;
        PassageirosNaJornada = 0;
    }

    public override string ToString() => $"Van {Numero:00} (capacidade: {Capacidade})";
}
