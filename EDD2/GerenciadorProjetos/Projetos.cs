using System;
using System.Collections.Generic;

namespace GerenciadorProjetos
{
    public class Projetos
    {
        private List<Projeto> itens = new List<Projeto>();

        public bool adicionar(Projeto p)
        {
            if (p == null) return false;
            if (itens.Contains(p)) return false;

            foreach (Projeto existente in itens)
            {
                if (string.Equals(existente.Nome, p.Nome, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            itens.Add(p);
            return true;
        }

        public bool remover(Projeto p)
        {
            if (p == null) return false;

            Projeto existente = buscar(p);
            if (existente == null) return false;
            if (existente.Tarefas.Count > 0) return false;

            return itens.Remove(existente);
        }

        public Projeto buscar(Projeto p)
        {
            if (p == null) return null;

            int idx = itens.IndexOf(p);
            return idx >= 0 ? itens[idx] : null;
        }

        public List<Projeto> listar()
        {
            return new List<Projeto>(itens);
        }
    }
}
