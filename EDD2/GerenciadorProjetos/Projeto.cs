using System;
using System.Collections.Generic;

namespace GerenciadorProjetos
{
    public class Projeto
    {
        private static int proximoId = 1;

        private int id;
        private string nome;
        private List<Tarefa> tarefas = new List<Tarefa>();

        public Projeto(string nome)
        {
            Nome = nome;
            this.id = proximoId++;
        }

        public Projeto(int id)
        {
            this.id = id;
        }

        public int Id => id;

        public string Nome
        {
            get => nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("O nome do projeto é obrigatório.");
                nome = value.Trim();
            }
        }

        public IReadOnlyList<Tarefa> Tarefas => tarefas;

        public void adicionarTarefa(Tarefa t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            if (tarefas.Contains(t))
                throw new InvalidOperationException("Já existe uma tarefa com este id no projeto.");

            tarefas.Add(t);
        }

        public bool removerTarefa(Tarefa t)
        {
            if (t == null) return false;
            return tarefas.Remove(t);
        }

        public Tarefa buscarTarefa(Tarefa t)
        {
            if (t == null) return null;

            int idx = tarefas.IndexOf(t);
            return idx >= 0 ? tarefas[idx] : null;
        }

        public List<Tarefa> tarefasPorStatus(string s)
        {
            List<Tarefa> resultado = new List<Tarefa>();

            foreach (Tarefa t in tarefas)
            {
                if (string.Equals(t.Status, s, StringComparison.OrdinalIgnoreCase))
                    resultado.Add(t);
            }

            return resultado;
        }

        public List<Tarefa> tarefasPorPrioridade(int p)
        {
            List<Tarefa> resultado = new List<Tarefa>();

            foreach (Tarefa t in tarefas)
            {
                if (t.Prioridade == p)
                    resultado.Add(t);
            }

            return resultado;
        }

        public int totalAbertas()
        {
            return tarefasPorStatus(Tarefa.StatusAberta).Count;
        }

        public int totalFechadas()
        {
            return tarefasPorStatus(Tarefa.StatusFechada).Count;
        }

        public override string ToString()
        {
            return $"[{id}] {nome}";
        }

        public override bool Equals(object obj)
        {
            Projeto outro = obj as Projeto;
            if (outro == null)
                return false;

            return id == outro.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
