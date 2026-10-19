using System;
using System.Windows.Forms;

namespace ProjetoAtendimento
{
    public partial class Form1 : Form
    {
        private Senhas controleSenhas;
        private Guiches controleGuiches;

        public Form1()
        {
            InitializeComponent();
            controleSenhas = new Senhas();
            controleGuiches = new Guiches();
            lblQtdGuiches.Text = "Guichês ativos: 0";
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            controleSenhas.gerar();
            AtualizarListaSenhas();
        }

        private void btnAdicionarGuiche_Click(object sender, EventArgs e)
        {
            int novoId = controleGuiches.ListaGuiches.Count + 1;
            Guiche novoGuiche = new Guiche(novoId);
            controleGuiches.adicionar(novoGuiche);
            
            lblQtdGuiches.Text = $"Guichês ativos: {controleGuiches.ListaGuiches.Count}";
            MessageBox.Show($"Guichê {novoId} adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuicheId.Text, out int idGuiche))
            {
                Guiche guiche = controleGuiches.ListaGuiches.Find(g => g.Id == idGuiche);

                if (guiche != null)
                {
                    bool sucesso = guiche.chamar(controleSenhas.FilaSenhas);
                    if (sucesso)
                    {
                        AtualizarListaSenhas();
                        AtualizarListaAtendimentos(guiche);
                    }
                    else
                    {
                        MessageBox.Show("Não há senhas na fila de espera.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Informe um ID de guichê válido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnListarSenhas_Click(object sender, EventArgs e)
        {
            AtualizarListaSenhas();
        }

        private void btnListarAtendimentos_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuicheId.Text, out int idGuiche))
            {
                Guiche guiche = controleGuiches.ListaGuiches.Find(g => g.Id == idGuiche);
                if (guiche != null)
                {
                    AtualizarListaAtendimentos(guiche);
                }
                else
                {
                    MessageBox.Show("Guichê não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AtualizarListaSenhas()
        {
            lstSenhas.Items.Clear();
            foreach (Senha s in controleSenhas.FilaSenhas)
            {
                lstSenhas.Items.Add(s.dadosParciais());
            }
        }

        private void AtualizarListaAtendimentos(Guiche guiche)
        {
            lstAtendimentos.Items.Clear();
            foreach (Senha s in guiche.Atendimentos)
            {
                lstAtendimentos.Items.Add(s.dadosCompletos());
            }
        }
    }
}