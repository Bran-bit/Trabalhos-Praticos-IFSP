namespace ProjetoTransporte.Services;

/// <summary>Violação de uma regra de negócio (mensagem pronta para o usuário).</summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
