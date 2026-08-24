using System;

namespace projetoCurso
{
    internal class Aluno
    {
        private int id;
        private string nome;

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

        public string Nome
        {
            get => nome;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser vazio.", nameof(Nome));
                nome = value;
            }
        }

        public Aluno(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public bool podeMatricular(Curso[] cursos)
        {
            if (cursos == null)
                return false;

            int quantidadeDisciplinas = 0;
            int idCursoAtual = -1;
            bool temCurso = false;

            foreach (Curso curso in cursos)
            {
                if (curso == null || curso.Disciplinas == null)
                    continue;

                foreach (Disciplina disciplina in curso.Disciplinas)
                {
                    if (disciplina == null)
                        continue;

                    foreach (Aluno a in disciplina.Alunos)
                    {
                        if (a != null && a.Id == this.Id)
                        {
                            quantidadeDisciplinas++;

                            if (!temCurso)
                            {
                                idCursoAtual = curso.Id;
                                temCurso = true;
                            }
                            else if (curso.Id != idCursoAtual)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return quantidadeDisciplinas < 6;
        }

        public override string ToString()
        {
            return $"{Id} - {Nome}";
        }
    }
}