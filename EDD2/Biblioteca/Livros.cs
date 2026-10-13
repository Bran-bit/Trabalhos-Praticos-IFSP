namespace Biblioteca;

public class Livros
{
    public List<Livro> Acervo { get; private set; }

    public Livros()
    {
        Acervo = new List<Livro>();
    }

    public void Adicionar(Livro livro)
    {
        Acervo.Add(livro);
    }

    public Livro? Pesquisar(Livro livro)
    {
        foreach (Livro item in Acervo)
        {
            if (item.Isbn == livro.Isbn)
            {
                return item;
            }
        }

        return null;
    }
}
