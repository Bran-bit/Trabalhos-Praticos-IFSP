using System;

namespace FilasConsole.Models;

public class Log
{
    public DateTime DtAcesso { get; set; }
    public int UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;

    /// <summary>
    /// Referência opcional ao usuário. Pode ser <c>null</c> após o Download
    /// caso o usuário tenha sido excluído; nesse caso use <see cref="UsuarioId"/>
    /// e <see cref="UsuarioNome"/> (que são cópias feitas no momento do acesso).
    /// </summary>
    public Usuario? Usuario { get; set; }

    public bool TipoAcesso { get; set; }
}
