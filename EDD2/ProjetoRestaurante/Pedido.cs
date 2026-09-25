using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante
{
    public class Pedido
    {
        private const int MAX_ITENS = 10;

        private int id;
        private string cliente;
        private Item[] itens = new Item[MAX_ITENS];
        private int quantidadeItens = 0;

        public Pedido(int id, string cliente)
        {
            this.id = id;
            this.cliente = cliente;
        }

        public int Id => id;
        public string Cliente => cliente;
        public int QuantidadeItens => quantidadeItens;

        public IReadOnlyList<Item> Itens
        {
            get
            {
                Item[] copia = new Item[quantidadeItens];
                Array.Copy(itens, copia, quantidadeItens);
                return copia;
            }
        }

        public bool adicionarItem(Item item)
        {
            if (item == null) return false;
            if (quantidadeItens >= MAX_ITENS) return false;

            itens[quantidadeItens] = item;
            quantidadeItens++;
            return true;
        }

        public bool removerItem(Item item)
        {
            if (item == null) return false;

            for (int i = 0; i < quantidadeItens; i++)
            {
                if (itens[i] != null && itens[i].Id == item.Id)
                {
                    for (int j = i; j < quantidadeItens - 1; j++)
                        itens[j] = itens[j + 1];

                    itens[quantidadeItens - 1] = null;
                    quantidadeItens--;
                    return true;
                }
            }
            return false;
        }

        public string dadosDoPedido()
        {
            //usar essa classe que já lida com a criação de strings
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pedido #{id} - Cliente: {cliente}");
            sb.AppendLine("Itens:");

            if (quantidadeItens == 0)
            {
                sb.AppendLine("  (nenhum item cadastrado)");
            }
            else
            {
                for (int i = 0; i < quantidadeItens; i++)
                {
                    sb.AppendLine($"  [{itens[i].Id}] {itens[i].Descricao} - R$ {itens[i].Preco.ToString("F2", CultureInfo.InvariantCulture)}");
                }
            }

            sb.AppendLine($"Total: R$ {calcularTotal().ToString("F2", CultureInfo.InvariantCulture)}");
            return sb.ToString();
        }

        public double calcularTotal()
        {
            double total = 0;
            for (int i = 0; i < quantidadeItens; i++)
                total += itens[i].Preco;
            return total;
        }
    }

}
