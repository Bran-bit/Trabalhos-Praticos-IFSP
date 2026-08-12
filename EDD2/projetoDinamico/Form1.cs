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
    public partial class Form1 : Form
    {
        private const int NUM_FILEIRAS = 15;
        private const int NUM_COLUNAS = 40;

        // achei mais interessante usar o tipo enum do que um char por legibilidade
        private enum EstadoPoltrona
        {
            Vaga,
            OcupadaInteira,
            OcupadaMeia
        }

        private EstadoPoltrona[,] estados;
        private Button[,] botoesPoltronas;

        private Button btnFaturamento;
        private Label lblResultado;

        // Cores usadas para representar cada estado visualmente
        private readonly Color corVaga = Color.LightGreen;
        private readonly Color corInteira = Color.IndianRed;
        private readonly Color corMeia = Color.Khaki;
        public Form1()
        {
            InitializeComponent();
            estados = new EstadoPoltrona[NUM_FILEIRAS, NUM_COLUNAS];
            botoesPoltronas = new Button[NUM_FILEIRAS, NUM_COLUNAS];

            // Inicializar o estado das poltronas (vagas)
            for (int f = 0; f < NUM_FILEIRAS; f++)
                for (int c = 0; c < NUM_COLUNAS; c++)
                    estados[f, c] = EstadoPoltrona.Vaga;

            MontarInterface();
        }
        private void MontarInterface()
        {
            int tamanhoBotao = 20;
            int espacamento = 2;
            int margemEsquerda = 15;
            int margemTopo = 15;

            for (int f = 0; f < NUM_FILEIRAS; f++)
            {
                for (int c = 0; c < NUM_COLUNAS; c++)
                {
                    Button btn = new Button();
                    btn.Parent = this;
                    btn.Width = tamanhoBotao;
                    btn.Height = tamanhoBotao;
                    btn.Left = margemEsquerda + c * (tamanhoBotao + espacamento);
                    btn.Top = margemTopo + f * (tamanhoBotao + espacamento);
                    btn.Margin = new Padding(0);
                    btn.BackColor = corVaga;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.Font = new Font("Microsoft Sans Serif", 6.5F);
                    btn.Text = "";

                    // Guarda a fileira/coluna desta poltrona nas Tag (poderia criar mais um enum, mas quis aproveitar o recurso)
                    btn.Tag = new Point(f, c);

                    btn.Click += Poltrona_Click;

                    botoesPoltronas[f, c] = btn;
                }
            }

            // Posição abaixo do mapa de poltronas (última fileira + margem)
            int topoAreaInferior = margemTopo + NUM_FILEIRAS * (tamanhoBotao + espacamento) + 20;

            btnFaturamento = new Button();
            btnFaturamento.Parent = this;
            btnFaturamento.Left = margemEsquerda;
            btnFaturamento.Top = topoAreaInferior;
            btnFaturamento.Width = 180;
            btnFaturamento.Height = 45;
            btnFaturamento.Text = "Faturamento";
            btnFaturamento.BackColor = Color.FromArgb(52, 152, 219);
            btnFaturamento.ForeColor = Color.White;
            btnFaturamento.FlatStyle = FlatStyle.Flat;
            btnFaturamento.FlatAppearance.BorderSize = 0;
            btnFaturamento.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnFaturamento.FlatAppearance.MouseDownBackColor = Color.FromArgb(31, 97, 141);
            btnFaturamento.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnFaturamento.Cursor = Cursors.Hand;
            btnFaturamento.Click += BtnFaturamento_Click;

            lblResultado = new Label();
            lblResultado.Parent = this;
            lblResultado.Left = margemEsquerda + btnFaturamento.Width + 20;
            lblResultado.Top = topoAreaInferior;
            lblResultado.Width = 500;
            lblResultado.Height = 60;
            lblResultado.Font = new Font("Microsoft Sans Serif", 10F);
            lblResultado.Text = "";

            this.ClientSize = new Size(
                margemEsquerda * 2 + NUM_COLUNAS * (tamanhoBotao + espacamento) + 20,
                topoAreaInferior + 100);
        }

        private void Poltrona_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Point posicao = (Point)btn.Tag;
            int fileira = posicao.X;
            int coluna = posicao.Y;

            if (!CoordenadaValida(fileira, coluna))
                return;

            EstadoPoltrona estadoAtual = estados[fileira, coluna];

            if (estadoAtual == EstadoPoltrona.Vaga)
            {
                ReservarPoltrona(fileira, coluna, btn);
            }
            else
            {
                string tipo = estadoAtual == EstadoPoltrona.OcupadaInteira ? "Inteira" : "Meia entrada";
                MessageBox.Show(
                    string.Format("A poltrona (Fileira {0}, Assento {1}) já está ocupada.\nTipo de reserva: {2}",
                        fileira + 1, coluna + 1, tipo),
                    "Poltrona ocupada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private bool CoordenadaValida(int fileira, int coluna)
        {
            if (fileira < 0 || fileira >= NUM_FILEIRAS)
            {
                MessageBox.Show(
                    $"Fileira inválida: {fileira + 1}. Deve estar entre 1 e {NUM_FILEIRAS}.",
                    "Erro de Consistência",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            if (coluna < 0 || coluna >= NUM_COLUNAS)
            {
                MessageBox.Show(
                    $"Poltrona inválida: {coluna + 1}. Deve estar entre 1 e {NUM_COLUNAS}.",
                    "Erro de Consistência",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ReservarPoltrona(int fileira, int coluna, Button btn)
        {
            if (!CoordenadaValida(fileira, coluna))
                return;

            // Abre outra classe de formulário que criei pra exibir a mensagem de meia ou inteira como queria
            TipoReservaDialog dialogo = new TipoReservaDialog(fileira + 1, coluna + 1);
            dialogo.ShowDialog(this);

            if (dialogo.OpcaoEscolhida == TipoReservaDialog.OpcaoReserva.Inteira)
            {
                estados[fileira, coluna] = EstadoPoltrona.OcupadaInteira;
                btn.BackColor = corInteira;
                btn.Text = "I";
            }
            else if (dialogo.OpcaoEscolhida == TipoReservaDialog.OpcaoReserva.Meia)
            {
                estados[fileira, coluna] = EstadoPoltrona.OcupadaMeia;
                btn.BackColor = corMeia;
                btn.Text = "M";
            }
        }
        private decimal ValorCheioFileira(int fileira)
        {
            if (fileira < 0 || fileira >= NUM_FILEIRAS)
                return 0m;

            int numeroFileira = fileira + 1;

            if (numeroFileira >= 1 && numeroFileira <= 5)
                return 50.00m;
            else if (numeroFileira >= 6 && numeroFileira <= 10)
                return 30.00m;
            else
                return 15.00m;
        }

        private void BtnFaturamento_Click(object sender, EventArgs e)
        {
            int qtdeOcupados = 0;
            decimal valorTotal = 0m;

            for (int f = 0; f < NUM_FILEIRAS; f++)
            {
                // Garante que a fileira é válida
                if (!CoordenadaValida(f, 0))
                    continue;

                decimal valorCheio = ValorCheioFileira(f);

                for (int c = 0; c < NUM_COLUNAS; c++)
                {
                    // Garante que a coluna é válida
                    if (!CoordenadaValida(f, c))
                        continue;

                    EstadoPoltrona estado = estados[f, c];

                    if (estado == EstadoPoltrona.OcupadaInteira)
                    {
                        qtdeOcupados++;
                        valorTotal += valorCheio;
                    }
                    else if (estado == EstadoPoltrona.OcupadaMeia)
                    {
                        qtdeOcupados++;
                        valorTotal += valorCheio * 0.5m;
                    }
                }
            }

            lblResultado.Text = string.Format(
                "Qtde de lugares ocupados: {0}\nValor da bilheteria: R$ {1:0.00}",
                qtdeOcupados, valorTotal);
        }

    }
}
