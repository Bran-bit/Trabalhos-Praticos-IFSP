using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores
{
    internal class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public int Max => max;
        public int Qtde => qtde;

        public Vendedor[] OsVendedores => osVendedores;

        public Vendedores(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException(nameof(max), "Capacidade máxima deve ser maior que zero.");
            this.max = max;
            qtde = 0;
            osVendedores = new Vendedor[max];
        }

        public bool addVendedor(Vendedor v)
        {
            if (v == null)
                return false;
            if (searchVendedor(v) != null)
                return false;
            if (qtde >= max)
                return false;

            osVendedores[qtde++] = v;
            return true;
        }

        public Vendedor searchVendedor(Vendedor v)
        {
            if (v == null)
                return null;

            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                    return osVendedores[i];
            }
            return null;
        }

        public bool delVendedor(Vendedor v)
        {
            Vendedor existente = searchVendedor(v);
            if (existente == null)
                return false;

            if (existente.qtdeDiasComVenda() > 0)
                return false;

            int indice = -1;
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Id == v.Id)
                {
                    indice = i;
                    break;
                }
            }

            if (indice == -1)
                return false;

            for (int i = indice; i < qtde - 1; i++)
            {
                osVendedores[i] = osVendedores[i + 1];
            }

            osVendedores[qtde - 1] = null;
            qtde--;
            return true;
        }

        public double valorVendas()
        {
            double total = 0.0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].valorVendas();
            }
            return total;
        }

        public double valorComissao()
        {
            double total = 0.0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].valorComissao();
            }
            return total;
        }
    }
}
