using ProjetoRestaurante;
using System.Globalization;

class Program
{
    static Restaurante restaurante = new Restaurante();

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
                case "1": CriarNovoPedido(); break;
                case "2": AdicionarItemAoPedido(); break;
                case "3": RemoverItemDoPedido(); break;
                case "4": ConsultarPedido(); break;
                case "5": CancelarPedido(); break;
                case "6": ListarTodosOsPedidos(); break;
                default: Console.WriteLine("Opção inválida! Tente novamente."); break;
            }

            Console.WriteLine();
        }

        Console.WriteLine("Encerrando o sistema. Até logo!");
    }

    static void ExibirMenu()
    {
        Console.WriteLine("========== Cozinha Industrial ==========");
        Console.WriteLine("0. Sair");
        Console.WriteLine("1. Criar novo pedido");
        Console.WriteLine("2. Adicionar item ao pedido");
        Console.WriteLine("3. Remover item do pedido");
        Console.WriteLine("4. Consultar pedido");
        Console.WriteLine("5. Cancelar pedido");
        Console.WriteLine("6. Listar todos os pedidos");
        Console.Write("Escolha uma opção: ");
    }

    static void CriarNovoPedido()
    {
        Console.Write("Nome do cliente: ");
        string cliente = Console.ReadLine() ?? "";

        int id = restaurante.gerarProximoIdPedido();
        Pedido pedido = new Pedido(id, cliente);

        if (!restaurante.novoPedido(pedido))
        {
            Console.WriteLine("Não foi possível criar o pedido: limite diário de 50 pedidos atingido.");
            return;
        }

        Console.WriteLine($"Pedido criado! Número: {id}");
        Console.WriteLine("Adicione pelo menos 1 item. Digite 'fim' na descrição para encerrar.");

        while (pedido.QuantidadeItens < 10)
        {
            Console.Write("Descrição do item (ou 'fim' para encerrar): ");
            string descricao = Console.ReadLine() ?? "";

            if (descricao.Trim().ToLower() == "fim") break;

            double preco = LerDouble("Preço do item (ex: 12.50): ");
            Item item = new Item(descricao, preco);

            if (pedido.adicionarItem(item))
                Console.WriteLine($"Item adicionado. ({pedido.QuantidadeItens}/10)");
            else
            {
                Console.WriteLine("Limite de 10 itens atingido.");
                break;
            }
        }

        // Se o usuário encerrou sem nenhum item, cancela o pedido
        if (pedido.QuantidadeItens == 0)
        {
            restaurante.cancelarPedido(id);
            Console.WriteLine("Nenhum item foi adicionado. O pedido foi cancelado.");
        }
        else
        {
            Console.WriteLine($"Pedido #{id} finalizado com {pedido.QuantidadeItens} item(ns).");
        }
    }

    static void AdicionarItemAoPedido()
    {
        Pedido pedido = LocalizarPedidoPorId();
        if (pedido == null) return;

        Console.Write("Descrição do item: ");
        string descricao = Console.ReadLine() ?? "";
        double preco = LerDouble("Preço do item (ex: 12.50): ");

        Item item = new Item(descricao, preco);

        // A validação do limite de 10 itens fica centralizada em Pedido.adicionarItem().
        if (pedido.adicionarItem(item))
            Console.WriteLine("Item adicionado com sucesso!");
        else
            Console.WriteLine("Não foi possível adicionar o item: este pedido já possui 10 itens (limite máximo).");
    }

    static void RemoverItemDoPedido()
    {
        Pedido pedido = LocalizarPedidoPorId();
        if (pedido == null) return;

        if (pedido.QuantidadeItens == 0)
        {
            Console.WriteLine("Este pedido não possui itens cadastrados.");
            return;
        }

        IReadOnlyList<Item> itens = pedido.Itens; // cópia local

        Console.WriteLine("Itens do pedido:");
        for (int i = 0; i < itens.Count; i++)
        {
            Console.WriteLine($"  [{itens[i].Id}] {itens[i].Descricao} - R$ {itens[i].Preco.ToString("F2", CultureInfo.InvariantCulture)}");
        }

        int itemId = LerInteiro("Digite o ID do item que deseja remover: ");

        Item itemParaRemover = null;
        for (int i = 0; i < itens.Count; i++)
        {
            if (itens[i].Id == itemId)
            {
                itemParaRemover = itens[i];
                break;
            }
        }

        if (itemParaRemover == null)
        {
            Console.WriteLine("Item não encontrado neste pedido.");
            return;
        }

        if (pedido.removerItem(itemParaRemover))
            Console.WriteLine("Item removido com sucesso!");
        else
            Console.WriteLine("Não foi possível remover o item.");
    }

    static void ConsultarPedido()
    {
        Pedido pedido = LocalizarPedidoPorId();
        if (pedido == null) return;

        Console.WriteLine(pedido.dadosDoPedido());
    }

    static void CancelarPedido()
    {
        int id = LerInteiro("Número do pedido: ");

        // Usa a sobrecarga por int — não cria mais um Pedido "fantasma".
        if (restaurante.cancelarPedido(id))
            Console.WriteLine("Pedido cancelado com sucesso!");
        else
            Console.WriteLine("Pedido não encontrado.");
    }

    static void ListarTodosOsPedidos()
    {
        if (restaurante.QuantidadePedidos == 0)
        {
            Console.WriteLine("Nenhum pedido registrado no momento.");
            return;
        }

        IReadOnlyList<Pedido> pedidos = restaurante.Pedidos; // cópia local
        double somaGeral = 0;

        Console.WriteLine("========== Pedidos do dia ==========");
        for (int i = 0; i < pedidos.Count; i++)
        {
            double total = pedidos[i].calcularTotal();
            somaGeral += total;
            Console.WriteLine($"Pedido #{pedidos[i].Id} - Cliente: {pedidos[i].Cliente} - Total: R$ {total.ToString("F2", CultureInfo.InvariantCulture)}");
        }
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"Soma geral do dia: R$ {somaGeral.ToString("F2", CultureInfo.InvariantCulture)}");
    }

    static Pedido LocalizarPedidoPorId()
    {
        int id = LerInteiro("Número do pedido: ");
        Pedido pedido = restaurante.buscarPedido(id);

        if (pedido == null)
            Console.WriteLine("Pedido não encontrado.");

        return pedido;
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

    static double LerDouble(string mensagem)
    {
        double valor;
        Console.Write(mensagem);
        while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out valor))
        {
            Console.Write("Valor inválido. Digite um número (use ponto para decimais): ");
        }
        return valor;
    }
}