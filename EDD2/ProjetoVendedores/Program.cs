using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vendedores equipe = new Vendedores(10);
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("=== Sistema de Vendas ===");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                    Console.WriteLine("Opção inválida.");
                }
                else
                {
                    switch (opcao)
                    {
                        case 1: CadastrarVendedor(equipe); break;
                        case 2: ConsultarVendedor(equipe); break;
                        case 3: ExcluirVendedor(equipe); break;
                        case 4: RegistrarVenda(equipe); break;
                        case 5: ListarVendedores(equipe); break;
                        case 0: Console.WriteLine("Encerrando..."); break;
                        default: Console.WriteLine("Opção inválida."); break;
                    }
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();

            } while (opcao != 0);
        }

        static void CadastrarVendedor(Vendedores equipe)
        {
            Console.WriteLine("Cadastro de Vendedor");

            if (equipe.Qtde >= equipe.Max)
            {
                Console.WriteLine("Limite de vendedores atingido.");
                return;
            }

            int id = LerInteiroPositivo("ID: ");
            Vendedor existente = equipe.searchVendedor(new Vendedor(id, "Temp", 0));
            if (existente != null)
            {
                Console.WriteLine("Já existe um vendedor com esse ID.");
                return;
            }

            string nome = LerStringNaoVazia("Nome: ");
            double perc = LerDoublePercentual("Percentual de comissão (0 a 100): ");

            Vendedor v = new Vendedor(id, nome, perc);
            bool adicionado = equipe.addVendedor(v);
            Console.WriteLine(adicionado ? "Vendedor cadastrado com sucesso." : "Não foi possível cadastrar.");
        }

        static void ConsultarVendedor(Vendedores equipe)
        {
            Console.WriteLine("Consulta de Vendedor");
            int id = LerInteiroPositivo("ID do vendedor: ");
            Vendedor v = equipe.searchVendedor(new Vendedor(id, "Temp", 0));

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            double totalVendas = v.valorVendas();
            int diasComVenda = v.qtdeDiasComVenda();
            double valorMedioDiario = (diasComVenda > 0) ? totalVendas / diasComVenda : 0;

            Console.WriteLine($"ID: {v.Id}");
            Console.WriteLine($"Nome: {v.Nome}");
            Console.WriteLine($"Valor total das vendas: {totalVendas:F2}");
            Console.WriteLine($"Valor da comissão devida: {v.valorComissao():F2}");
            Console.WriteLine($"Valor médio das vendas diárias (dias com venda): {valorMedioDiario:F2}");
        }

        static void ExcluirVendedor(Vendedores equipe)
        {
            Console.WriteLine("Exclusão de Vendedor");
            int id = LerInteiroPositivo("ID do vendedor: ");
            Vendedor v = equipe.searchVendedor(new Vendedor(id, "Temp", 0));

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            if (equipe.delVendedor(v))
            {
                Console.WriteLine("Vendedor excluído com sucesso.");
            }
            else
            {
                Console.WriteLine("Não foi possível excluir. O vendedor pode ter vendas associadas.");
            }
        }

        static void RegistrarVenda(Vendedores equipe)
        {
            Console.WriteLine("Registro de Venda");
            int id = LerInteiroPositivo("ID do vendedor: ");
            Vendedor v = equipe.searchVendedor(new Vendedor(id, "Temp", 0));

            if (v == null)
            {
                Console.WriteLine("Vendedor não encontrado.");
                return;
            }

            int dia = LerInteiroIntervalo("Dia (1-31): ", 1, 31);
            int qtde = LerInteiroNaoNegativo("Quantidade de vendas: ");
            double valor = LerDoubleNaoNegativo("Valor total das vendas: ");

            Venda venda = new Venda(qtde, valor);
            v.registrarVenda(dia, venda);
            Console.WriteLine("Venda registrada com sucesso.");
        }

        static void ListarVendedores(Vendedores equipe)
        {
            Console.WriteLine("Listagem de Vendedores");

            if (equipe.Qtde == 0)
            {
                Console.WriteLine("Nenhum vendedor cadastrado.");
                return;
            }

            double totalGeralVendas = 0;
            double totalGeralComissoes = 0;

            for (int i = 0; i < equipe.Qtde; i++)
            {
                Vendedor v = equipe.OsVendedores[i];
                if (v == null)
                    continue;

                double totalVendas = v.valorVendas();
                double totalComissao = v.valorComissao();

                Console.WriteLine($"ID: {v.Id} | Nome: {v.Nome} | Total de vendas: {totalVendas:F2} | Comissão: {totalComissao:F2}");

                totalGeralVendas += totalVendas;
                totalGeralComissoes += totalComissao;
            }

            Console.WriteLine($"\nTotal geral de vendas: {totalGeralVendas:F2}");
            Console.WriteLine($"Total geral de comissões: {totalGeralComissoes:F2}");
        }

        // Métodos auxiliares de validação, já que não posso jogar tudo no set das classes
        static int LerInteiroPositivo(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor) || valor <= 0)
            {
                Console.Write("Valor inválido. Deve ser um inteiro positivo. " + mensagem);
            }
            return valor;
        }

        static int LerInteiroNaoNegativo(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor) || valor < 0)
            {
                Console.Write("Valor inválido. Deve ser um inteiro não negativo. " + mensagem);
            }
            return valor;
        }

        static int LerInteiroIntervalo(string mensagem, int min, int max)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor) || valor < min || valor > max)
            {
                Console.Write($"Valor inválido. Deve estar entre {min} e {max}. " + mensagem);
            }
            return valor;
        }

        static double LerDoubleNaoNegativo(string mensagem)
        {
            double valor;
            Console.Write(mensagem);
            while (!double.TryParse(Console.ReadLine(), out valor) || valor < 0)
            {
                Console.Write("Valor inválido. Deve ser um número não negativo. " + mensagem);
            }
            return valor;
        }

        static double LerDoublePercentual(string mensagem)
        {
            double valor;
            Console.Write(mensagem);
            while (!double.TryParse(Console.ReadLine(), out valor) || valor < 0 || valor > 100)
            {
                Console.Write("Valor inválido. Deve estar entre 0 e 100. " + mensagem);
            }
            return valor;
        }

        static string LerStringNaoVazia(string mensagem)
        {
            string valor;
            Console.Write(mensagem);
            while (true)
            {
                valor = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(valor))
                    return valor;
                Console.Write("Valor inválido. Não pode ser vazio. " + mensagem);
            }
        }
    }
}
