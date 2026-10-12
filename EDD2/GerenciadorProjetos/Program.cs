using System;
using System.Collections.Generic;

namespace GerenciadorProjetos
{
    class Program
    {
        static Projetos projetos = new Projetos();

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
                    case "1": AdicionarProjeto(); break;
                    case "2": PesquisarProjeto(); break;
                    case "3": RemoverProjeto(); break;
                    case "4": AdicionarTarefa(); break;
                    case "5": AplicarAcaoNaTarefa(t => t.concluir(), "Tarefa concluída com sucesso!"); break;
                    case "6": AplicarAcaoNaTarefa(t => t.cancelar(), "Tarefa cancelada com sucesso!"); break;
                    case "7": AplicarAcaoNaTarefa(t => t.reabrir(), "Tarefa reaberta com sucesso!"); break;
                    case "8": ListarTarefasDoProjeto(); break;
                    case "9": FiltrarEmUmProjeto(); break;
                    case "10": FiltrarEmTodosOsProjetos(); break;
                    case "11": ResumoGeral(); break;
                    default: Console.WriteLine("Opção inválida! Tente novamente."); break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("Encerrando o sistema. Até logo!");
        }

        static void ExibirMenu()
        {
            Console.WriteLine("======= Gerenciador de Projetos =======");
            Console.WriteLine(" 0. Sair");
            Console.WriteLine(" 1. Adicionar projeto");
            Console.WriteLine(" 2. Pesquisar projeto");
            Console.WriteLine(" 3. Remover projeto");
            Console.WriteLine(" 4. Adicionar tarefa em projeto");
            Console.WriteLine(" 5. Concluir tarefa");
            Console.WriteLine(" 6. Cancelar tarefa");
            Console.WriteLine(" 7. Reabrir tarefa");
            Console.WriteLine(" 8. Listar tarefas de um projeto");
            Console.WriteLine(" 9. Filtrar tarefas em um projeto");
            Console.WriteLine("10. Filtrar tarefas em todos os projetos");
            Console.WriteLine("11. Resumo geral");
            Console.Write("Escolha uma opção: ");
        }

        static void AdicionarProjeto()
        {
            string nome = LerTextoObrigatorio("Nome do projeto: ");
            Projeto projeto = new Projeto(nome);

            if (projetos.adicionar(projeto))
                Console.WriteLine($"Projeto adicionado com sucesso! Id: {projeto.Id}");
            else
                Console.WriteLine("Não foi possível adicionar: já existe um projeto com este nome.");
        }

        static void PesquisarProjeto()
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            Console.WriteLine();
            Console.WriteLine($"Projeto {projeto}");

            List<Tarefa> abertas = projeto.tarefasPorStatus(Tarefa.StatusAberta);
            List<Tarefa> fechadas = projeto.tarefasPorStatus(Tarefa.StatusFechada);
            List<Tarefa> canceladas = projeto.tarefasPorStatus(Tarefa.StatusCancelada);

            ExibirGrupo("Abertas", abertas);
            ExibirGrupo("Fechadas", fechadas);
            ExibirGrupo("Canceladas", canceladas);

            Console.WriteLine();
            Console.WriteLine("Totais:");
            Console.WriteLine($"  Abertas:    {projeto.totalAbertas()}");
            Console.WriteLine($"  Fechadas:   {projeto.totalFechadas()}");
            Console.WriteLine($"  Canceladas: {canceladas.Count}");
            Console.WriteLine($"  Total:      {projeto.Tarefas.Count}");
        }

        static void RemoverProjeto()
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            if (projeto.Tarefas.Count > 0)
            {
                Console.WriteLine($"Não é possível remover: o projeto possui {projeto.Tarefas.Count} tarefa(s).");
                return;
            }

            if (projetos.remover(projeto))
                Console.WriteLine("Projeto removido com sucesso!");
            else
                Console.WriteLine("Não foi possível remover o projeto.");
        }

        static void AdicionarTarefa()
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            string titulo = LerTextoObrigatorio("Título: ");

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine() ?? "";

            int prioridade = LerPrioridade();

            Tarefa tarefa = new Tarefa(titulo, descricao, prioridade);
            projeto.adicionarTarefa(tarefa);

            Console.WriteLine($"Tarefa adicionada com sucesso! Id: {tarefa.Id}");
        }

        static void AplicarAcaoNaTarefa(Action<Tarefa> acao, string mensagemSucesso)
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            Tarefa tarefa = SelecionarTarefa(projeto);
            if (tarefa == null) return;

            try
            {
                acao(tarefa);
                Console.WriteLine(mensagemSucesso);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ListarTarefasDoProjeto()
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            Console.WriteLine();
            Console.WriteLine($"Tarefas do projeto {projeto}:");

            if (projeto.Tarefas.Count == 0)
            {
                Console.WriteLine("  (nenhuma tarefa cadastrada)");
                return;
            }

            ExibirTarefas(projeto.Tarefas);
            Console.WriteLine($"Total: {projeto.Tarefas.Count} tarefa(s)");
        }

        static void FiltrarEmUmProjeto()
        {
            Projeto projeto = SelecionarProjeto();
            if (projeto == null) return;

            string descricaoFiltro;
            Func<Projeto, List<Tarefa>> filtro = LerFiltro(out descricaoFiltro);

            List<Tarefa> resultado = filtro(projeto);

            Console.WriteLine();
            Console.WriteLine($"Projeto {projeto} - filtro: {descricaoFiltro}");

            if (resultado.Count == 0)
            {
                Console.WriteLine("  (nenhuma tarefa encontrada)");
                return;
            }

            ExibirTarefas(resultado);
            Console.WriteLine($"Encontradas: {resultado.Count} tarefa(s)");
        }

        static void FiltrarEmTodosOsProjetos()
        {
            List<Projeto> lista = projetos.listar();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum projeto cadastrado.");
                return;
            }

            string descricaoFiltro;
            Func<Projeto, List<Tarefa>> filtro = LerFiltro(out descricaoFiltro);

            Console.WriteLine();
            Console.WriteLine($"Filtro: {descricaoFiltro}");

            int total = 0;
            foreach (Projeto projeto in lista)
            {
                List<Tarefa> resultado = filtro(projeto);
                if (resultado.Count == 0) continue;

                Console.WriteLine();
                Console.WriteLine($"Projeto {projeto}:");
                ExibirTarefas(resultado);
                total += resultado.Count;
            }

            Console.WriteLine();
            if (total == 0)
                Console.WriteLine("Nenhuma tarefa encontrada em nenhum projeto.");
            else
                Console.WriteLine($"Total encontrado: {total} tarefa(s)");
        }

        static void ResumoGeral()
        {
            List<Projeto> lista = projetos.listar();

            int abertas = 0;
            int fechadas = 0;
            int canceladas = 0;

            foreach (Projeto projeto in lista)
            {
                abertas += projeto.totalAbertas();
                fechadas += projeto.totalFechadas();
                canceladas += projeto.tarefasPorStatus(Tarefa.StatusCancelada).Count;
            }

            Console.WriteLine("========== Resumo geral ==========");
            Console.WriteLine($"Projetos cadastrados: {lista.Count}");
            Console.WriteLine($"Tarefas abertas:      {abertas}");
            Console.WriteLine($"Tarefas fechadas:     {fechadas}");
            Console.WriteLine($"Tarefas canceladas:   {canceladas}");
            Console.WriteLine($"Total de tarefas:     {abertas + fechadas + canceladas}");

            if (abertas == 0)
                Console.WriteLine("Concluídas em relação às abertas: N/A (não há tarefas abertas)");
            else
                Console.WriteLine($"Concluídas em relação às abertas: {fechadas * 100.0 / abertas:F1}%");
        }

        static Projeto SelecionarProjeto()
        {
            List<Projeto> lista = projetos.listar();
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum projeto cadastrado.");
                return null;
            }

            Console.WriteLine("Projetos cadastrados:");
            foreach (Projeto p in lista)
                Console.WriteLine($"  {p} - {p.Tarefas.Count} tarefa(s)");

            int id = LerInteiro("Id do projeto: ");
            Projeto encontrado = projetos.buscar(new Projeto(id));

            if (encontrado == null)
                Console.WriteLine("Projeto não encontrado.");

            return encontrado;
        }

        static Tarefa SelecionarTarefa(Projeto projeto)
        {
            if (projeto.Tarefas.Count == 0)
            {
                Console.WriteLine("Este projeto não possui tarefas.");
                return null;
            }

            Console.WriteLine();
            Console.WriteLine($"Tarefas do projeto {projeto}:");
            ExibirTarefas(projeto.Tarefas);

            int id = LerInteiro("Id da tarefa: ");
            Tarefa encontrada = projeto.buscarTarefa(new Tarefa(id));

            if (encontrada == null)
                Console.WriteLine("Tarefa não encontrada neste projeto.");

            return encontrada;
        }

        static void ExibirTarefas(IEnumerable<Tarefa> tarefas)
        {
            foreach (Tarefa t in tarefas)
                Console.WriteLine($"  {t}");
        }

        static void ExibirGrupo(string titulo, List<Tarefa> tarefas)
        {
            Console.WriteLine();
            Console.WriteLine($"--- {titulo} ({tarefas.Count}) ---");

            if (tarefas.Count == 0)
                Console.WriteLine("  (nenhuma)");
            else
                ExibirTarefas(tarefas);
        }

        static Func<Projeto, List<Tarefa>> LerFiltro(out string descricao)
        {
            Console.WriteLine("Filtrar por:");
            Console.WriteLine("  1. Status");
            Console.WriteLine("  2. Prioridade");
            int tipo = LerOpcao("Escolha: ", 1, 2);

            if (tipo == 1)
            {
                string status = LerStatus();
                descricao = $"status {status}";
                return p => p.tarefasPorStatus(status);
            }

            int prioridade = LerPrioridade();
            descricao = $"prioridade {Tarefa.DescricaoPrioridade(prioridade)}";
            return p => p.tarefasPorPrioridade(prioridade);
        }

        static string LerStatus()
        {
            Console.WriteLine("Status:");
            Console.WriteLine($"  1. {Tarefa.StatusAberta}");
            Console.WriteLine($"  2. {Tarefa.StatusFechada}");
            Console.WriteLine($"  3. {Tarefa.StatusCancelada}");
            int opcao = LerOpcao("Escolha: ", 1, 3);

            switch (opcao)
            {
                case 1: return Tarefa.StatusAberta;
                case 2: return Tarefa.StatusFechada;
                default: return Tarefa.StatusCancelada;
            }
        }

        static int LerPrioridade()
        {
            return LerOpcao("Prioridade (1=alta, 2=média, 3=baixa): ", 1, 3);
        }

        static string LerTextoObrigatorio(string mensagem)
        {
            Console.Write(mensagem);
            string texto = Console.ReadLine() ?? "";

            while (string.IsNullOrWhiteSpace(texto))
            {
                Console.Write("Campo obrigatório. Digite novamente: ");
                texto = Console.ReadLine() ?? "";
            }

            return texto.Trim();
        }

        static int LerOpcao(string mensagem, int minimo, int maximo)
        {
            int valor = LerInteiro(mensagem);

            while (valor < minimo || valor > maximo)
            {
                valor = LerInteiro($"Opção inválida. Digite um valor entre {minimo} e {maximo}: ");
            }

            return valor;
        }

        static int LerInteiro(string mensagem)
        {
            int valor;
            Console.Write(mensagem);
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor inválido. Digite um número inteiro: ");
            }
            return valor;
        }
    }
}
