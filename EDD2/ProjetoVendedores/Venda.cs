using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores
{
    internal class Venda
    {
        private int qtde;
        private double valor;

        public int Qtde
        {
            get => qtde;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Qtde), "Quantidade não pode ser negativa.");
                qtde = value;
            }
        }

        public double Valor
        {
            get => valor;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Valor), "Valor não pode ser negativo.");
                valor = value;
            }
        }

        public Venda(int qtde, double valor)
        {
            Qtde = qtde;
            Valor = valor;
        }

        public Venda() : this(0, 0) { }

        public double valorMedio()
        {
            if (Qtde <= 0)
                return 0;
            return Valor / Qtde;
        }

        public override string ToString()
        {
            return $"Qtde: {Qtde}, Valor: {Valor.ToString("F2")}";
        }
    }
}
