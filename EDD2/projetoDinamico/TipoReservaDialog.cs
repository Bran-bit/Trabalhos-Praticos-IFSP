using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoDinamico
{
    public partial class TipoReservaDialog : Form
    {
            public enum OpcaoReserva
        {
            Nenhuma,
            Inteira,
            Meia
        }

        public OpcaoReserva OpcaoEscolhida { get; private set; } = OpcaoReserva.Nenhuma;

        public TipoReservaDialog(int fileira, int coluna)
        {
            // Configura a janelinha
            this.Text = "Tipo de Reserva";
            this.Size = new Size(280, 150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Mensagem informativa
            Label lblMensagem = new Label();
            lblMensagem.Text = $"Reservar poltrona:\nFileira {fileira}, Assento {coluna}";
            lblMensagem.Location = new Point(20, 10);
            lblMensagem.Size = new Size(240, 30);

            // Botão Inteira
            Button btnInteira = new Button();
            btnInteira.Text = "Inteira";
            btnInteira.Location = new Point(30, 55);
            btnInteira.Size = new Size(90, 30);
            btnInteira.Click += (sender, e) =>
            {
                OpcaoEscolhida = OpcaoReserva.Inteira;
                this.Close();
            };

            // Botão Meia Entrada
            Button btnMeia = new Button();
            btnMeia.Text = "Meia Entrada";
            btnMeia.Location = new Point(140, 55);
            btnMeia.Size = new Size(90, 30);
            btnMeia.Click += (sender, e) =>
            {
                OpcaoEscolhida = OpcaoReserva.Meia;
                this.Close();
            };

            // Adiciona os controles ao formulário
            this.Controls.Add(lblMensagem);
            this.Controls.Add(btnInteira);
            this.Controls.Add(btnMeia);
        }
    }
}
