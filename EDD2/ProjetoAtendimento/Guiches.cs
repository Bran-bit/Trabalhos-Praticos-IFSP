using System.Collections.Generic;

namespace ProjetoAtendimento
{
    public class Guiches
    {
        private List<Guiche> listaGuiches;

        public List<Guiche> ListaGuiches { get => listaGuiches; }

        public Guiches()
        {
            listaGuiches = new List<Guiche>();
        }

        public void adicionar(Guiche guiche)
        {
            listaGuiches.Add(guiche);
        }
    }
}