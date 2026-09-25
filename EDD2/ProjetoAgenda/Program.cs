using System;

namespace Agenda
{
    class Program
    {
        static Contatos contatos = new Contatos();

        static void Main(string[] args)
        {
            bool sair = false;

            while (!sair)
            {
                ExibirMenu();
                string opcao = Console.ReadLine();
                Console.WriteLine();

                switch (opcao)
                {
                    case "0": sair = true; break;
                    case "1": AdicionarContato(); break;
                    case "2": PesquisarContato(); break;
                    case "3": AlterarContato(); break;
                    case "4": RemoverContato(); break;
                    case "5": ListarContatos(); break;
                    default: Console.WriteLine("Opção inválida! Tente novamente."); break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Encerrando o sistema. Até logo!");
        }

        static void ExibirMenu()
        {
            Console.WriteLine("========== Agenda de Contatos ==========");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar contato");
            Console.WriteLine("2. Pesquisar contato");
            Console.WriteLine("3. Alterar contato");
            Console.WriteLine("4. Remover contato");
            Console.WriteLine("5. Listar contatos");
            Console.Write("Escolha uma opção: ");
        }

        static void AdicionarContato()
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine("Data de nascimento:");
            Data dtNasc = LerData();

            Contato contato = new Contato(email, nome, dtNasc);

            AdicionarTelefonesInterativo(contato);

            if (contatos.adicionar(contato))
                Console.WriteLine("Contato adicionado com sucesso!");
            else
                Console.WriteLine("Não foi possível adicionar: já existe um contato com este e-mail.");
        }

        static void PesquisarContato()
        {
            Console.Write("E-mail do contato: ");
            string email = Console.ReadLine() ?? "";

            Contato encontrado = contatos.pesquisar(new Contato(email));

            if (encontrado == null)
                Console.WriteLine("Contato não encontrado.");
            else
                Console.WriteLine(encontrado);
        }

        static void AlterarContato()
        {
            Console.Write("E-mail do contato a alterar: ");
            string email = Console.ReadLine() ?? "";

            Contato existente = contatos.pesquisar(new Contato(email));
            if (existente == null)
            {
                Console.WriteLine("Contato não encontrado.");
                return;
            }

            Console.WriteLine("Dados atuais:");
            Console.WriteLine(existente);
            Console.WriteLine();

            Console.Write($"Novo nome (Enter para manter \"{existente.Nome}\"): ");
            string novoNome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(novoNome))
                novoNome = existente.Nome;

            Data novaData = existente.DtNasc;
            Console.Write("Deseja alterar a data de nascimento? (s/n): ");
            if ((Console.ReadLine() ?? "").Trim().ToLower() == "s")
            {
                Console.WriteLine("Nova data de nascimento:");
                novaData = LerData();
            }

            Contato atualizado = new Contato(email, novoNome, novaData);

            foreach (Telefone t in existente.Telefones)
                atualizado.adicionarTelefone(t);

            Console.Write("Deseja adicionar novos telefones a este contato? (s/n): ");
            if ((Console.ReadLine() ?? "").Trim().ToLower() == "s")
                AdicionarTelefonesInterativo(atualizado);

            if (contatos.alterar(atualizado))
                Console.WriteLine("Contato alterado com sucesso!");
            else
                Console.WriteLine("Não foi possível alterar o contato.");
        }

        static void RemoverContato()
        {
            Console.Write("E-mail do contato a remover: ");
            string email = Console.ReadLine() ?? "";

            if (contatos.remover(new Contato(email)))
                Console.WriteLine("Contato removido com sucesso!");
            else
                Console.WriteLine("Contato não encontrado.");
        }

        static void ListarContatos()
        {
            if (contatos.Agenda.Count == 0)
            {
                Console.WriteLine("Nenhum contato cadastrado.");
                return;
            }

            Console.WriteLine("========== Lista de Contatos ==========");
            int i = 1;
            foreach (Contato c in contatos.Agenda)
            {
                Console.WriteLine($"--- Contato {i} ---");
                Console.WriteLine(c);
                Console.WriteLine();
                i++;
            }
            Console.WriteLine($"Total de contatos: {contatos.Agenda.Count}");
        }

        static void AdicionarTelefonesInterativo(Contato contato)
        {
            Console.WriteLine("Cadastro de telefones (digite 'fim' no tipo para encerrar).");
            while (true)
            {
                Console.Write("Tipo do telefone (Celular, Residencial, Comercial...) ou 'fim': ");
                string tipo = Console.ReadLine() ?? "";
                if (tipo.Trim().ToLower() == "fim") break;

                Console.Write("Número: ");
                string numero = Console.ReadLine() ?? "";

                Console.Write("É o telefone principal? (s/n): ");
                bool principal = (Console.ReadLine() ?? "").Trim().ToLower() == "s";

                contato.adicionarTelefone(new Telefone(tipo, numero, principal));
                Console.WriteLine("Telefone adicionado.");
            }
        }

        static Data LerData()
        {
            while (true)
            {
                int dia = LerInteiro("  Dia: ");
                int mes = LerInteiro("  Mês: ");
                int ano = LerInteiro("  Ano: ");

                try
                {
                    return new Data(dia, mes, ano);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"  Data inválida: {ex.Message} Tente novamente.");
                }
            }
        }

        static int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("  Valor inválido. Digite um número inteiro: ");
            }
            return valor;
        }
    }
}
