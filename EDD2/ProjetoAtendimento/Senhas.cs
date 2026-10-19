using System.Collections.Generic;

namespace ProjetoAtendimento
{
    public class Senhas
    {
        private int proximoAtendimento;
        private Queue<Senha> filaSenhas;

        public Queue<Senha> FilaSenhas { get => filaSenhas; }

        public Senhas()
        {
            filaSenhas = new Queue<Senha>();
            proximoAtendimento = 1;
        }

        public void gerar()
        {
            Senha novaSenha = new Senha(proximoAtendimento);
            filaSenhas.Enqueue(novaSenha);
            proximoAtendimento++;
        }
    }
}