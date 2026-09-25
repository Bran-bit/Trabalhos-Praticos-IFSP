using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante
{
    public class Restaurante
    {
        private const int MAX_PEDIDOS = 50;

        private int proxPedido = 1;
        private Pedido[] pedidos = new Pedido[MAX_PEDIDOS];
        private int quantidadePedidos = 0;

        public int QuantidadePedidos => quantidadePedidos;

        //usei esse tipo pra não quebrar o encapsulamento. Se eu retorno o array, podem modificar ele
        public IReadOnlyList<Pedido> Pedidos
        {
            get
            {
                Pedido[] copia = new Pedido[quantidadePedidos];
                Array.Copy(pedidos, copia, quantidadePedidos);
                return copia;
            }
        }

        public int gerarProximoIdPedido() => proxPedido++;

        public bool novoPedido(Pedido pedido)
        {
            if (pedido == null) return false;
            if (quantidadePedidos >= MAX_PEDIDOS) return false;

            pedidos[quantidadePedidos] = pedido;
            quantidadePedidos++;
            return true;
        }

        public Pedido buscarPedido(Pedido pedido)
        {
            if (pedido == null) return null;
            return buscarPedido(pedido.Id);
        }

        public bool cancelarPedido(Pedido pedido)
        {
            if (pedido == null) return false;
            return cancelarPedido(pedido.Id);
        }

        // Sobrecargas auxiliares

        public Pedido buscarPedido(int id)
        {
            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i] != null && pedidos[i].Id == id)
                    return pedidos[i];
            }
            return null;
        }

        public bool cancelarPedido(int id)
        {
            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i] != null && pedidos[i].Id == id)
                {
                    for (int j = i; j < quantidadePedidos - 1; j++)
                        pedidos[j] = pedidos[j + 1];

                    pedidos[quantidadePedidos - 1] = null;
                    quantidadePedidos--;
                    return true;
                }
            }
            return false;
        }
    }

}
