using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoCurso
{
    internal class Disciplina
    {
        private int id;
        private string descricao;
        private Aluno[] alunos;

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

        internal Aluno[] Alunos => alunos;

        public Disciplina(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
            alunos = new Aluno[15];
        }

        public bool matricularAluno(Aluno aluno)
        {
            if (aluno == null)
                return false;

            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] != null && alunos[i].Id == aluno.Id)
                    return false;
            }

            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] == null)
                {
                    alunos[i] = aluno;
                    return true;
                }
            }

            return false;
        }

        public bool desmatricularAluno(Aluno aluno)
        {
            if (aluno == null)
                return false;

            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] != null && alunos[i].Id == aluno.Id)
                {
                    alunos[i] = null;
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
