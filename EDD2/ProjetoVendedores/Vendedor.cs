using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVendedores
{
    internal class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "ID deve ser maior que zero.");
                id = value;
            }
        }

        public string Nome
        {
            get => nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome não pode ser vazio.", nameof(Nome));
                nome = value;
            }
        }

        public double PercComissao
        {
            get => percComissao;
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(PercComissao), "Percentual de comissão deve estar entre 0 e 100.");
                percComissao = value;
            }
        }

        public Vendedor(int id, string nome, double percComissao)
        {
            Id = id;
            Nome = nome;
            PercComissao = percComissao;
            asVendas = new Venda[31];
        }

        public void registrarVenda(int dia, Venda venda)
        {
            if (dia < 1 || dia > 31)
                throw new ArgumentOutOfRangeException(nameof(dia), "Dia deve estar entre 1 e 31.");
            if (venda == null)
                throw new ArgumentNullException(nameof(venda), "Venda não pode ser nula.");

            asVendas[dia - 1] = venda;
        }

        public double valorVendas()
        {
            double total = 0.0;
            foreach (Venda v in asVendas)
            {
                if (v != null)
                    total += v.Valor;
            }
            return total;
        }

        public double valorComissao()
        {
            return valorVendas() * PercComissao / 100.0;
        }

        public int qtdeDiasComVenda()
        {
            int cont = 0;
            foreach (Venda v in asVendas)
            {
                if (v != null)
                    cont++;
            }
            return cont;
        }
    }
}
