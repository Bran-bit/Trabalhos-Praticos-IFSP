using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante
{
    public class Item
    {
        private static int proximoId = 1;

        private int id;
        private string descricao;
        private double preco;

        public Item(string descricao, double preco)
        {
            this.id = proximoId++;
            this.descricao = descricao;
            this.preco = preco;
        }

        public int Id => id;
        public string Descricao => descricao;
        public double Preco => preco;
    }

}
