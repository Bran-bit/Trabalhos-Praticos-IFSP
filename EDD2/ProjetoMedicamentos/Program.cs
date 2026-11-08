using System;
using System.Globalization;
using System.Linq;
using System.Text;
using MedicamentosConsole.Models;
using MedicamentosConsole.Services;

namespace MedicamentosConsole;

public static class Program
{
    private static readonly Medicamentos medicamentos = new();

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== CONTROLE DE MEDICAMENTOS =====");
            Console.WriteLine("0. Finalizar processo");
            Console.WriteLine("1. Cadastrar medicamento");
            Console.WriteLine("2. Consultar medicamento (sintético)");
            Console.WriteLine("3. Consultar medicamento (analítico)");
            Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
            Console.WriteLine("5. Vender medicamento");
            Console.WriteLine("6. Listar medicamentos");
            Console.Write("Opção: ");

            var entrada = Console.ReadLine();

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
                case 1: CadastrarMedicamento(); break;
                case 2: ConsultarSintetico(); break;
                case 3: ConsultarAnalitico(); break;
                case 4: ComprarMedicamento(); break;
                case 5: VenderMedicamento(); break;
                case 6: ListarMedicamentos(); break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        Console.WriteLine("Processo finalizado.");
    }

    private static void CadastrarMedicamento()
    {
        var id = LerInteiro("ID do medicamento: ");

        if (id is null || id <= 0)
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        if (Buscar(id.Value) is not null)
        {
            Console.WriteLine("Já existe um medicamento com esse ID.");
            return;
        }

        var nome = LerTexto("Nome: ");

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome obrigatório.");
            return;
        }

        var laboratorio = LerTexto("Laboratório: ");

        if (string.IsNullOrWhiteSpace(laboratorio))
        {
            Console.WriteLine("Laboratório obrigatório.");
            return;
        }

        medicamentos.Adicionar(new Medicamento(id.Value, nome, laboratorio));

        Console.WriteLine("Medicamento cadastrado.");
    }

    private static void ConsultarSintetico()
    {
        var medicamento = LerMedicamento();
        if (medicamento is null) return;

        Console.WriteLine(medicamento);
    }

    private static void ConsultarAnalitico()
    {
        var medicamento = LerMedicamento();
        if (medicamento is null) return;

        Console.WriteLine(medicamento);

        if (medicamento.Lotes.Count == 0)
        {
            Console.WriteLine("Nenhum lote em estoque.");
            return;
        }

        Console.WriteLine("Lotes (do mais antigo para o mais recente):");

        foreach (var lote in medicamento.Lotes)
            Console.WriteLine($"  {lote}");
    }

    private static void ComprarMedicamento()
    {
        var medicamento = LerMedicamento();
        if (medicamento is null) return;

        var idLote = LerInteiro("ID do lote: ");

        if (idLote is null || idLote <= 0)
        {
            Console.WriteLine("ID de lote inválido.");
            return;
        }

        if (medicamento.Lotes.Any(l => l.Id == idLote.Value))
        {
            Console.WriteLine("Já existe um lote com esse ID neste medicamento.");
            return;
        }

        var qtde = LerInteiro("Quantidade: ");

        if (qtde is null || qtde <= 0)
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        Console.Write("Vencimento (dd/MM/aaaa): ");

        if (!DateTime.TryParseExact(
                Console.ReadLine()?.Trim(),
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var venc))
        {
            Console.WriteLine("Data inválida.");
            return;
        }

        medicamento.Comprar(new Lote(idLote.Value, qtde.Value, venc));

        Console.WriteLine($"Lote cadastrado. Disponível: {medicamento.QtdeDisponivel()}.");
    }

    private static void VenderMedicamento()
    {
        var medicamento = LerMedicamento();
        if (medicamento is null) return;

        var qtde = LerInteiro("Quantidade a vender: ");

        if (qtde is null || qtde <= 0)
        {
            Console.WriteLine("Quantidade inválida.");
            return;
        }

        if (medicamento.Vender(qtde.Value))
            Console.WriteLine($"Venda realizada. Restam {medicamento.QtdeDisponivel()} unidades.");
        else
            Console.WriteLine($"Venda não realizada: quantidade indisponível. Disponível: {medicamento.QtdeDisponivel()}.");
    }

    private static void ListarMedicamentos()
    {
        if (medicamentos.ListaMedicamentos.Count == 0)
        {
            Console.WriteLine("Nenhum medicamento cadastrado.");
            return;
        }

        foreach (var medicamento in medicamentos.ListaMedicamentos)
            Console.WriteLine(medicamento);
    }

    private static Medicamento? Buscar(int id)
    {
        var encontrado = medicamentos.Pesquisar(new Medicamento(id, string.Empty, string.Empty));
        return encontrado.Id == 0 ? null : encontrado;
    }

    private static Medicamento? LerMedicamento()
    {
        var id = LerInteiro("ID do medicamento: ");

        if (id is null)
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        var medicamento = Buscar(id.Value);

        if (medicamento is null)
            Console.WriteLine("Medicamento não encontrado.");

        return medicamento;
    }

    private static int? LerInteiro(string mensagem)
    {
        Console.Write(mensagem);
        return int.TryParse(Console.ReadLine(), out int valor) ? valor : null;
    }

    private static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }
}
