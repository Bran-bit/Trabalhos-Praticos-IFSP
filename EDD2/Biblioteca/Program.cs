using Biblioteca;

Livros livros = new Livros();
int opcao;

do
{
    Console.WriteLine();
    Console.WriteLine("===== BIBLIOTECA =====");
    Console.WriteLine("1 - Adicionar livro");
    Console.WriteLine("2 - Pesquisar livro (sintético)");
    Console.WriteLine("3 - Pesquisar livro (analítico)");
    Console.WriteLine("4 - Adicionar exemplar");
    Console.WriteLine("5 - Registrar empréstimo");
    Console.WriteLine("6 - Registrar devolução");
    Console.WriteLine("0 - Sair");
    opcao = LerInteiro("Opção: ");
    Console.WriteLine();

    switch (opcao)
    {
        case 1:
            AdicionarLivro();
            break;
        case 2:
            PesquisarSintetico();
            break;
        case 3:
            PesquisarAnalitico();
            break;
        case 4:
            AdicionarExemplar();
            break;
        case 5:
            RegistrarEmprestimo();
            break;
        case 6:
            RegistrarDevolucao();
            break;
        case 0:
            Console.WriteLine("Encerrando a aplicação.");
            break;
        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
} while (opcao != 0);

int LerInteiro(string mensagem)
{
    int valor;
    Console.Write(mensagem);

    while (true)
    {
        string? linha = Console.ReadLine();

        if (linha == null)
        {
            Environment.Exit(0);
        }

        if (int.TryParse(linha, out valor))
        {
            return valor;
        }

        Console.Write("Valor inválido. " + mensagem);
    }
}

string LerTexto(string mensagem)
{
    Console.Write(mensagem);
    return Console.ReadLine() ?? string.Empty;
}

Livro? BuscarLivro(int isbn)
{
    return livros.Pesquisar(new Livro(isbn, string.Empty, string.Empty, string.Empty));
}

Livro? LerLivroExistente()
{
    int isbn = LerInteiro("ISBN: ");
    Livro? livro = BuscarLivro(isbn);

    if (livro == null)
    {
        Console.WriteLine("Livro não encontrado.");
    }

    return livro;
}

Exemplar? BuscarExemplar(Livro livro, int tombo)
{
    foreach (Exemplar exemplar in livro.Exemplares)
    {
        if (exemplar.Tombo == tombo)
        {
            return exemplar;
        }
    }

    return null;
}

bool TomboExiste(int tombo)
{
    foreach (Livro livro in livros.Acervo)
    {
        if (BuscarExemplar(livro, tombo) != null)
        {
            return true;
        }
    }

    return false;
}

void ExibirSintetico(Livro livro)
{
    Console.WriteLine("ISBN: " + livro.Isbn);
    Console.WriteLine("Título: " + livro.Titulo);
    Console.WriteLine("Autor: " + livro.Autor);
    Console.WriteLine("Editora: " + livro.Editora);
    Console.WriteLine("Total de exemplares: " + livro.QtdeExemplares());
    Console.WriteLine("Exemplares disponíveis: " + livro.QtdeDisponiveis());
    Console.WriteLine("Total de empréstimos: " + livro.QtdeEmprestimos());
    Console.WriteLine("Disponibilidade: " + livro.PercDisponibilidade().ToString("F2") + "%");
}

void AdicionarLivro()
{
    int isbn = LerInteiro("ISBN: ");

    if (BuscarLivro(isbn) != null)
    {
        Console.WriteLine("Já existe um livro com este ISBN.");
        return;
    }

    string titulo = LerTexto("Título: ");
    string autor = LerTexto("Autor: ");
    string editora = LerTexto("Editora: ");

    livros.Adicionar(new Livro(isbn, titulo, autor, editora));
    Console.WriteLine("Livro adicionado com sucesso.");
}

void PesquisarSintetico()
{
    Livro? livro = LerLivroExistente();

    if (livro == null)
    {
        return;
    }

    Console.WriteLine();
    ExibirSintetico(livro);
}

void PesquisarAnalitico()
{
    Livro? livro = LerLivroExistente();

    if (livro == null)
    {
        return;
    }

    Console.WriteLine();
    ExibirSintetico(livro);

    foreach (Exemplar exemplar in livro.Exemplares)
    {
        Console.WriteLine();
        Console.WriteLine("  Tombo: " + exemplar.Tombo);
        Console.WriteLine("  Situação: " + (exemplar.Disponivel() ? "Disponível" : "Emprestado"));
        Console.WriteLine("  Empréstimos: " + exemplar.QtdeEmprestimos());

        foreach (Emprestimo emprestimo in exemplar.Emprestimos)
        {
            string devolucao = emprestimo.DtDevolucao.HasValue
                ? emprestimo.DtDevolucao.Value.ToString("dd/MM/yyyy HH:mm")
                : "Em aberto";

            Console.WriteLine("    Empréstimo: " + emprestimo.DtEmprestimo.ToString("dd/MM/yyyy HH:mm") + " | Devolução: " + devolucao);
        }
    }
}

void AdicionarExemplar()
{
    Livro? livro = LerLivroExistente();

    if (livro == null)
    {
        return;
    }

    int tombo = LerInteiro("Tombo: ");

    if (TomboExiste(tombo))
    {
        Console.WriteLine("Já existe um exemplar com este tombo.");
        return;
    }

    livro.AdicionarExemplar(new Exemplar(tombo));
    Console.WriteLine("Exemplar adicionado com sucesso.");
}

void RegistrarEmprestimo()
{
    Livro? livro = LerLivroExistente();

    if (livro == null)
    {
        return;
    }

    int tombo = LerInteiro("Tombo: ");
    Exemplar? exemplar = BuscarExemplar(livro, tombo);

    if (exemplar == null)
    {
        Console.WriteLine("Exemplar não encontrado.");
        return;
    }

    if (exemplar.Emprestar())
    {
        Console.WriteLine("Empréstimo registrado com sucesso.");
    }
    else
    {
        Console.WriteLine("Exemplar indisponível para empréstimo.");
    }
}

void RegistrarDevolucao()
{
    Livro? livro = LerLivroExistente();

    if (livro == null)
    {
        return;
    }

    int tombo = LerInteiro("Tombo: ");
    Exemplar? exemplar = BuscarExemplar(livro, tombo);

    if (exemplar == null)
    {
        Console.WriteLine("Exemplar não encontrado.");
        return;
    }

    if (exemplar.Devolver())
    {
        Console.WriteLine("Devolução registrada com sucesso.");
    }
    else
    {
        Console.WriteLine("Este exemplar não está emprestado.");
    }
}
