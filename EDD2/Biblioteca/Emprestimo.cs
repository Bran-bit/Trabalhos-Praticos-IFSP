namespace Biblioteca;

public class Emprestimo
{
    public DateTime DtEmprestimo { get; private set; }
    public DateTime? DtDevolucao { get; private set; }

    public Emprestimo()
    {
        DtEmprestimo = DateTime.Now;
        DtDevolucao = null;
    }

    public bool Devolvido()
    {
        return DtDevolucao.HasValue;
    }

    public void RegistrarDevolucao()
    {
        DtDevolucao = DateTime.Now;
    }
}
