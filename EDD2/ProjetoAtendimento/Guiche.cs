using System;
using System.Collections.Generic;

namespace ProjetoAtendimento
{
    public class Guiche
    {
        private int id;
        private Queue<Senha> atendimentos;

        public int Id { get => id; }
        public Queue<Senha> Atendimentos { get => atendimentos; }

        public Guiche()
        {
            this.id = 0;
            this.atendimentos = new Queue<Senha>();
        }

        public Guiche(int id)
        {
            this.id = id;
            this.atendimentos = new Queue<Senha>();
        }

        public bool chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas.Count > 0)
            {
                Senha senhaChamada = filaSenhas.Dequeue();
                senhaChamada.DataAtend = DateTime.Now.Date;
                senhaChamada.HoraAtend = DateTime.Now;
                
                atendimentos.Enqueue(senhaChamada);
                return true;
            }
            return false;
        }
    }
}