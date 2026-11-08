using System.Collections.Generic;

namespace FilasConsole.Models;

public class Ambiente
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public Queue<Log> Logs { get; set; } = new();

    public void RegistrarLog(Log log)
    {
        if (log is null) return;

        Logs.Enqueue(log);

        while (Logs.Count > 100)
            Logs.Dequeue();
    }
}
