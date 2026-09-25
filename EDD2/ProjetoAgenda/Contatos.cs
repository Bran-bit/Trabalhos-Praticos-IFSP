using System.Collections.Generic;

namespace Agenda
{
    public class Contatos
    {
        private List<Contato> agenda = new List<Contato>();

        public IReadOnlyList<Contato> Agenda => agenda;

        public bool adicionar(Contato c)
        {
            if (c == null) return false;
            if (agenda.Contains(c)) return false;

            agenda.Add(c);
            return true;
        }

        public Contato pesquisar(Contato c)
        {
            if (c == null) return null;

            int idx = agenda.IndexOf(c);
            return idx >= 0 ? agenda[idx] : null;
        }

        public bool alterar(Contato c)
        {
            if (c == null) return false;

            int idx = agenda.IndexOf(c);
            if (idx == -1) return false;

            agenda[idx] = c;
            return true;
        }

        public bool remover(Contato c)
        {
            if (c == null) return false;
            return agenda.Remove(c);
        }
    }
}
