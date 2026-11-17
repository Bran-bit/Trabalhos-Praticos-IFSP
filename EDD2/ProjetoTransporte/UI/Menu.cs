using ProjetoTransporte.Models;
using ProjetoTransporte.Services;

namespace ProjetoTransporte.UI;

public class Menu
{
    private readonly Transportadora _empresa;

    public Menu(Transportadora empresa) => _empresa = empresa;

    public void Executar()
    {
        int opcao;
        do
        {
            ExibirOpcoes();
            opcao = LerInteiro("Opção: ", 0, 9);
            Console.WriteLine();

            try
            {
                switch (opcao)
                {
                    case 1: CadastrarVeiculo(); break;
                    case 2: CadastrarGaragem(); break;
                    case 3: IniciarJornada(); break;
                    case 4: EncerrarJornada(); break;
                    case 5: LiberarViagem(); break;
                    case 6: ListarVeiculosDaGaragem(); break;
                    case 7: InformarQuantidadeViagens(); break;
                    case 8: ListarViagens(); break;
                    case 9: InformarPassageiros(); break;
                }
            }
            catch (DomainException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"✗ {ex.Message}");
                Console.ResetColor();
            }

            if (opcao != 0)
            {
                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();
            }
        } while (opcao != 0);
    }

    private void ExibirOpcoes()
    {
        try { Console.Clear(); } catch (IOException) { }
        Console.WriteLine("=== TRANSPORTE — CONTROLE DE FROTA ===");
        Console.WriteLine($"Jornada: {(_empresa.JornadaIniciada ? "INICIADA" : "ENCERRADA")}  |  " +
                          $"Veículos: {_empresa.Frota.Count}  |  Garagens: {_empresa.Garagens.Count}\n");
        Console.WriteLine("0. Finalizar");
        Console.WriteLine("1. Cadastrar veículo");
        Console.WriteLine("2. Cadastrar garagem");
        Console.WriteLine("3. Iniciar jornada");
        Console.WriteLine("4. Encerrar jornada");
        Console.WriteLine("5. Liberar viagem (origem -> destino)");
        Console.WriteLine("6. Listar veículos em uma garagem");
        Console.WriteLine("7. Quantidade de viagens (origem -> destino)");
        Console.WriteLine("8. Listar viagens (origem -> destino)");
        Console.WriteLine("9. Passageiros transportados (origem -> destino)\n");
    }

    // ---------- Opções ----------

    private void CadastrarVeiculo()
    {
        int capacidade = LerInteiro("Capacidade de passageiros: ", 1, 200);
        var v = _empresa.CadastrarVeiculo(capacidade);
        Sucesso($"{v} cadastrada.");
    }

    private void CadastrarGaragem()
    {
        Console.Write("Nome da garagem (ex.: Viracopos): ");
        var g = _empresa.CadastrarGaragem(Console.ReadLine() ?? "");
        Sucesso($"Garagem {g.Id} - {g.Nome} cadastrada.");
    }

    private void IniciarJornada()
    {
        _empresa.IniciarJornada();
        Sucesso("Jornada iniciada. Veículos distribuídos alternadamente entre as garagens:");
        foreach (var g in _empresa.Garagens)
            Console.WriteLine($"  {g.Nome}: {g.Quantidade} veículo(s)");
    }

    private void EncerrarJornada()
    {
        var relatorio = _empresa.EncerrarJornada();
        Sucesso("Jornada encerrada.\n");
        Console.WriteLine("Veículo | Viagens | Passageiros");
        Console.WriteLine("--------+---------+------------");
        foreach (var l in relatorio)
            Console.WriteLine($"  {l.NumeroVeiculo,4}  | {l.Viagens,7} | {l.Passageiros,10}");
        Console.WriteLine("--------+---------+------------");
        Console.WriteLine($"  Total | {relatorio.Sum(l => l.Viagens),7} | {relatorio.Sum(l => l.Passageiros),10}");
    }

    private void LiberarViagem()
    {
        var (origem, destino) = EscolherRota();
        var v = _empresa.LiberarViagem(origem, destino);
        Sucesso($"Viagem #{v.Sequencial}: Van {v.Veiculo.Numero:00} saiu de {origem.Nome} para {destino.Nome} " +
                $"com {v.Passageiros} passageiros e estacionou no destino.");
    }

    private void ListarVeiculosDaGaragem()
    {
        var g = EscolherGaragem("Garagem");
        Console.WriteLine($"\nGaragem {g.Nome} — {g.Quantidade} veículo(s), potencial de transporte: {g.PotencialTransporte} passageiros");
        if (g.EstaVazia) return;

        Console.WriteLine("(do primeiro a sair ao último)");
        int pos = 1;
        foreach (var v in g.Veiculos)
            Console.WriteLine($"  {pos++}º  {v}");
    }

    private void InformarQuantidadeViagens()
    {
        var (o, d) = EscolherRota();
        Console.WriteLine($"\n{o.Nome} -> {d.Nome}: {_empresa.QuantidadeViagens(o, d)} viagem(ns).");
    }

    private void ListarViagens()
    {
        var (o, d) = EscolherRota();
        var viagens = _empresa.ListarViagens(o, d);
        Console.WriteLine($"\nViagens {o.Nome} -> {d.Nome}: {viagens.Count}");
        foreach (var v in viagens)
            Console.WriteLine($"  #{v.Sequencial,-3} {v.Saida:HH:mm:ss}  Van {v.Veiculo.Numero:00}  {v.Passageiros} passageiros");
    }

    private void InformarPassageiros()
    {
        var (o, d) = EscolherRota();
        Console.WriteLine($"\n{o.Nome} -> {d.Nome}: {_empresa.TotalPassageiros(o, d)} passageiro(s) transportado(s).");
    }

    // ---------- Entrada de dados ----------

    private (Garagem Origem, Garagem Destino) EscolherRota()
    {
        var origem = EscolherGaragem("Origem");
        var destino = EscolherGaragem("Destino");
        return (origem, destino);
    }

    private Garagem EscolherGaragem(string rotulo)
    {
        if (_empresa.Garagens.Count == 0)
            throw new DomainException("Nenhuma garagem cadastrada.");

        Console.WriteLine("Garagens:");
        foreach (var g in _empresa.Garagens)
            Console.WriteLine($"  {g.Id} - {g.Nome}");

        while (true)
        {
            int id = LerInteiro($"{rotulo} (código): ", 1, int.MaxValue);
            var garagem = _empresa.BuscarGaragem(id);
            if (garagem != null) return garagem;
            Console.WriteLine("Código inexistente.");
        }
    }

    private static int LerInteiro(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            var linha = Console.ReadLine();
            if (linha == null) return 0;   // fim da entrada (EOF): sai com segurança
            if (int.TryParse(linha, out int valor) && valor >= min && valor <= max)
                return valor;
            Console.WriteLine("Valor inválido.");
        }
    }

    private static void Sucesso(string msg)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✓ {msg}");
        Console.ResetColor();
    }
}
