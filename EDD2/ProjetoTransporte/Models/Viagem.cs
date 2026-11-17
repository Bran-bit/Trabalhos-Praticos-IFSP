namespace ProjetoTransporte.Models;

/// <summary>Registro de uma viagem efetuada.</summary>
public record Viagem(
    int Sequencial,
    Garagem Origem,
    Garagem Destino,
    Veiculo Veiculo,
    int Passageiros,
    DateTime Saida);
