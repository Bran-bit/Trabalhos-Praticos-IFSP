namespace Biblioteca;

public class Exemplar
{
    public int Tombo { get; private set; }
    public List<Emprestimo> Emprestimos { get; private set; }

    public Exemplar(int tombo)
    {
        Tombo = tombo;
        Emprestimos = new List<Emprestimo>();
    }

    public bool Emprestar()
    {
        if (!Disponivel())
        {
            return false;
        }

        Emprestimos.Add(new Emprestimo());
        return true;
    }

    public bool Devolver()
    {
        if (Disponivel())
        {
            return false;
        }

        Emprestimos[Emprestimos.Count - 1].RegistrarDevolucao();
        return true;
    }

    public bool Disponivel()
    {
        if (Emprestimos.Count == 0)
        {
            return true;
        }

        return Emprestimos[Emprestimos.Count - 1].Devolvido();
    }

    public int QtdeEmprestimos()
    {
        return Emprestimos.Count;
    }
}
