namespace Biblioteca;

public class Livro
{
    public int Isbn { get; private set; }
    public string Titulo { get; private set; }
    public string Autor { get; private set; }
    public string Editora { get; private set; }
    public List<Exemplar> Exemplares { get; private set; }

    public Livro(int isbn, string titulo, string autor, string editora)
    {
        Isbn = isbn;
        Titulo = titulo;
        Autor = autor;
        Editora = editora;
        Exemplares = new List<Exemplar>();
    }

    public void AdicionarExemplar(Exemplar exemplar)
    {
        Exemplares.Add(exemplar);
    }

    public int QtdeExemplares()
    {
        return Exemplares.Count;
    }

    public int QtdeDisponiveis()
    {
        int qtde = 0;

        foreach (Exemplar exemplar in Exemplares)
        {
            if (exemplar.Disponivel())
            {
                qtde++;
            }
        }

        return qtde;
    }

    public int QtdeEmprestimos()
    {
        int qtde = 0;

        foreach (Exemplar exemplar in Exemplares)
        {
            qtde += exemplar.QtdeEmprestimos();
        }

        return qtde;
    }

    public double PercDisponibilidade()
    {
        if (QtdeExemplares() == 0)
        {
            return 0;
        }

        return (double)QtdeDisponiveis() / QtdeExemplares() * 100;
    }
}
