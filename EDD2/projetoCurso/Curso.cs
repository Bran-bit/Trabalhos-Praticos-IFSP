using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoCurso
{
    internal class Curso
    {
        private int id;
        private string descricao;
        private Disciplina[] disciplinas;

        public int Id
        {
            get => id;
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "ID deve ser maior que zero.");
                id = value;
            }
        }

        public string Descricao
        {
            get => descricao;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Descrição não pode ser vazia.", nameof(Descricao));
                descricao = value;
            }
        }

        internal Disciplina[] Disciplinas => disciplinas;

        public Curso(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            disciplinas = new Disciplina[12];
        }

        public bool adicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return false;

            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] == null)
                {
                    disciplinas[i] = disciplina;
                    return true;
                }
            }

            return false;
        }

        public Disciplina pesquisarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return null;

            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] != null && disciplinas[i].Id == disciplina.Id)
                    return disciplinas[i];
            }

            return null;
        }

        public bool removerDisciplina(Disciplina disciplina)
        {
            if (disciplina == null)
                return false;

            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] != null && disciplinas[i].Id == disciplina.Id)
                {
                    disciplinas[i] = null;
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            return $"{Id} - {Descricao}";
        }
    }
}
