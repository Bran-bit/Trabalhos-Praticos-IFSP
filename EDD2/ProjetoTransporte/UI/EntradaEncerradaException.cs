namespace ProjetoTransporte.UI;

/// <summary>Lançada quando a entrada padrão termina (EOF / Ctrl+D / Ctrl+Z).</summary>
public class EntradaEncerradaException : Exception
{
    public EntradaEncerradaException() : base("Entrada encerrada.") { }
}
