using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoCurso
{
    internal class Escola
    {
        private Curso[] cursos;

        internal Curso[] Cursos => cursos;

        public Escola()
        {
            cursos = new Curso[5];
        }

        public bool adicionarCurso(Curso curso)
        {
            if (curso == null)
                return false;

            for (int i = 0; i < cursos.Length; i++)
            {
                if (cursos[i] == null)
                {
                    cursos[i] = curso;
                    return true;
                }
            }

            return false;
        }

        public Curso pesquisarCurso(Curso curso)
        {
            if (curso == null)
                return null;

            for (int i = 0; i < cursos.Length; i++)
            {
                if (cursos[i] != null && cursos[i].Id == curso.Id)
                    return cursos[i];
            }

            return null;
        }

        public bool removerCurso(Curso curso)
        {
            if (curso == null)
                return false;

            for (int i = 0; i < cursos.Length; i++)
            {
                if (cursos[i] != null && cursos[i].Id == curso.Id)
                {
                    cursos[i] = null;
                    return true;
                }
            }

            return false;
        }
    }
}
