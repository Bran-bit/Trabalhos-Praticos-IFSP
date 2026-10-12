using System;
using System.Windows.Forms;

namespace Agenda
{
    public partial class ContatoForm : Form
    {
        private Contato contato;
        private Contatos agenda;
        private bool modoEdicao;

        public Contato Contato => contato;

        public ContatoForm(Contatos agenda)
        {
            InitializeComponent();

            this.agenda = agenda;
            modoEdicao = false;
            contato = new Contato(string.Empty);

            Text = "Novo contato";
            dtpNascimento.MaxDate = DateTime.Today;
            dtpNascimento.Value = new DateTime(2000, 1, 1);
            cboTipo.SelectedIndex = 0;
        }

        public ContatoForm(Contato existente)
        {
            InitializeComponent();

            modoEdicao = true;
            contato = new Contato(existente.Email, existente.Nome, existente.DtNasc);
            foreach (Telefone t in existente.Telefones)
                contato.adicionarTelefone(new Telefone(t.Tipo, t.Numero, t.Principal));

            Text = "Alterar contato";
            txtNome.Text = existente.Nome;
            txtEmail.Text = existente.Email;
            txtEmail.ReadOnly = true;
            dtpNascimento.MaxDate = DateTime.Today;
            dtpNascimento.Value = existente.DtNasc.ParaDateTime();
            cboTipo.SelectedIndex = 0;

            AtualizarTelefones();
        }

        private void AtualizarTelefones()
        {
            dgvTelefones.Rows.Clear();

            foreach (Telefone t in contato.Telefones)
            {
                int linha = dgvTelefones.Rows.Add(t.Tipo, t.Numero, t.Principal ? "Sim" : "Não");
                dgvTelefones.Rows[linha].Tag = t;
            }
        }

        private void Aviso(string mensagem)
        {
            MessageBox.Show(this, mensagem, "Agenda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAdicionarTelefone_Click(object sender, EventArgs e)
        {
            string tipo = cboTipo.Text.Trim();
            string numero = txtNumero.Text.Trim();

            if (tipo.Length == 0)
            {
                Aviso("Informe o tipo do telefone.");
                cboTipo.Focus();
                return;
            }

            if (numero.Length == 0)
            {
                Aviso("Informe o número do telefone.");
                txtNumero.Focus();
                return;
            }

            contato.adicionarTelefone(new Telefone(tipo, numero, chkPrincipal.Checked));
            AtualizarTelefones();

            txtNumero.Clear();
            chkPrincipal.Checked = false;
            txtNumero.Focus();
        }

        private void btnRemoverTelefone_Click(object sender, EventArgs e)
        {
            if (dgvTelefones.SelectedRows.Count == 0)
            {
                Aviso("Selecione um telefone na lista para remover.");
                return;
            }

            Telefone selecionado = (Telefone)dgvTelefones.SelectedRows[0].Tag;
            contato.removerTelefone(selecionado);
            AtualizarTelefones();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (nome.Length == 0)
            {
                Aviso("Informe o nome do contato.");
                txtNome.Focus();
                return;
            }

            if (email.Length == 0 || !email.Contains("@"))
            {
                Aviso("Informe um e-mail válido.");
                txtEmail.Focus();
                return;
            }

            if (!modoEdicao && agenda.pesquisar(new Contato(email)) != null)
            {
                Aviso("Já existe um contato com este e-mail.");
                txtEmail.Focus();
                return;
            }

            DateTime nascimento = dtpNascimento.Value;

            contato.Nome = nome;
            if (!modoEdicao)
                contato.Email = email;
            contato.DtNasc = new Data(nascimento.Day, nascimento.Month, nascimento.Year);

            DialogResult = DialogResult.OK;
        }
    }
}
