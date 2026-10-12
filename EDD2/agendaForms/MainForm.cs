using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Agenda
{
    public partial class MainForm : Form
    {
        private Contatos contatos = new Contatos();

        public MainForm()
        {
            InitializeComponent();
            ListarContatos();
        }

        private void ExibirContatos(IEnumerable<Contato> lista)
        {
            dgvContatos.Rows.Clear();

            int exibidos = 0;
            foreach (Contato c in lista)
            {
                int linha = dgvContatos.Rows.Add(
                    c.Nome,
                    c.Email,
                    c.DtNasc.ToString(),
                    c.getIdade(),
                    c.getTelefonePrincipal());
                dgvContatos.Rows[linha].Tag = c;
                exibidos++;
            }

            lblTotal.Text = $"Exibindo {exibidos} de {contatos.Agenda.Count} contato(s)";
        }

        private void ListarContatos()
        {
            txtPesquisa.Clear();
            ExibirContatos(contatos.Agenda);
        }

        private Contato ObterSelecionado()
        {
            if (dgvContatos.SelectedRows.Count == 0)
                return null;

            return (Contato)dgvContatos.SelectedRows[0].Tag;
        }

        private void Aviso(string mensagem)
        {
            MessageBox.Show(this, mensagem, "Agenda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            using (ContatoForm form = new ContatoForm(contatos))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                if (contatos.adicionar(form.Contato))
                    ListarContatos();
                else
                    Aviso("Já existe um contato com este e-mail.");
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string email = txtPesquisa.Text.Trim();
            if (email.Length == 0)
            {
                Aviso("Informe o e-mail do contato para pesquisar.");
                return;
            }

            Contato encontrado = contatos.pesquisar(new Contato(email));
            if (encontrado == null)
            {
                Aviso("Contato não encontrado.");
                return;
            }

            ExibirContatos(new List<Contato> { encontrado });
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            Contato selecionado = ObterSelecionado();
            if (selecionado == null)
            {
                Aviso("Selecione um contato na lista para alterar.");
                return;
            }

            using (ContatoForm form = new ContatoForm(selecionado))
            {
                if (form.ShowDialog(this) != DialogResult.OK)
                    return;

                if (contatos.alterar(form.Contato))
                    ListarContatos();
                else
                    Aviso("Não foi possível alterar o contato.");
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            Contato selecionado = ObterSelecionado();
            if (selecionado == null)
            {
                Aviso("Selecione um contato na lista para remover.");
                return;
            }

            DialogResult resposta = MessageBox.Show(
                this,
                $"Deseja remover o contato \"{selecionado.Nome}\"?",
                "Agenda",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            if (contatos.remover(selecionado))
                ListarContatos();
            else
                Aviso("Não foi possível remover o contato.");
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            ListarContatos();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvContatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                btnAlterar_Click(sender, e);
        }
    }
}
