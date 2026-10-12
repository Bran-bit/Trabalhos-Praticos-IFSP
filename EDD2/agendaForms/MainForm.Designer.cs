using System.Drawing;
using System.Windows.Forms;

namespace Agenda
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblPesquisa = new Label();
            this.txtPesquisa = new TextBox();
            this.btnPesquisar = new Button();
            this.btnListar = new Button();
            this.dgvContatos = new DataGridView();
            this.colNome = new DataGridViewTextBoxColumn();
            this.colEmail = new DataGridViewTextBoxColumn();
            this.colNascimento = new DataGridViewTextBoxColumn();
            this.colIdade = new DataGridViewTextBoxColumn();
            this.colTelefone = new DataGridViewTextBoxColumn();
            this.lblTotal = new Label();
            this.btnAdicionar = new Button();
            this.btnAlterar = new Button();
            this.btnRemover = new Button();
            this.btnSair = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContatos)).BeginInit();
            this.SuspendLayout();

            this.lblPesquisa.AutoSize = true;
            this.lblPesquisa.Location = new Point(12, 17);
            this.lblPesquisa.Name = "lblPesquisa";
            this.lblPesquisa.TabIndex = 0;
            this.lblPesquisa.Text = "E-mail do contato:";

            this.txtPesquisa.Location = new Point(135, 13);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new Size(260, 23);
            this.txtPesquisa.TabIndex = 1;

            this.btnPesquisar.Location = new Point(405, 11);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new Size(95, 27);
            this.btnPesquisar.TabIndex = 2;
            this.btnPesquisar.Text = "Pesquisar";
            this.btnPesquisar.UseVisualStyleBackColor = true;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);

            this.btnListar.Location = new Point(506, 11);
            this.btnListar.Name = "btnListar";
            this.btnListar.Size = new Size(110, 27);
            this.btnListar.TabIndex = 3;
            this.btnListar.Text = "Listar todos";
            this.btnListar.UseVisualStyleBackColor = true;
            this.btnListar.Click += new System.EventHandler(this.btnListar_Click);

            this.dgvContatos.AllowUserToAddRows = false;
            this.dgvContatos.AllowUserToDeleteRows = false;
            this.dgvContatos.AllowUserToResizeRows = false;
            this.dgvContatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvContatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvContatos.Columns.AddRange(new DataGridViewColumn[] {
                this.colNome,
                this.colEmail,
                this.colNascimento,
                this.colIdade,
                this.colTelefone});
            this.dgvContatos.Location = new Point(12, 50);
            this.dgvContatos.MultiSelect = false;
            this.dgvContatos.Name = "dgvContatos";
            this.dgvContatos.ReadOnly = true;
            this.dgvContatos.RowHeadersVisible = false;
            this.dgvContatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvContatos.Size = new Size(776, 350);
            this.dgvContatos.TabIndex = 4;
            this.dgvContatos.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvContatos_CellDoubleClick);

            this.colNome.FillWeight = 120F;
            this.colNome.HeaderText = "Nome";
            this.colNome.Name = "colNome";
            this.colNome.ReadOnly = true;

            this.colEmail.FillWeight = 130F;
            this.colEmail.HeaderText = "E-mail";
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;

            this.colNascimento.FillWeight = 70F;
            this.colNascimento.HeaderText = "Nascimento";
            this.colNascimento.Name = "colNascimento";
            this.colNascimento.ReadOnly = true;

            this.colIdade.FillWeight = 40F;
            this.colIdade.HeaderText = "Idade";
            this.colIdade.Name = "colIdade";
            this.colIdade.ReadOnly = true;

            this.colTelefone.FillWeight = 90F;
            this.colTelefone.HeaderText = "Telefone principal";
            this.colTelefone.Name = "colTelefone";
            this.colTelefone.ReadOnly = true;

            this.lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new Point(12, 410);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Exibindo 0 de 0 contato(s)";

            this.btnAdicionar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAdicionar.Location = new Point(12, 438);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new Size(120, 32);
            this.btnAdicionar.TabIndex = 6;
            this.btnAdicionar.Text = "Adicionar";
            this.btnAdicionar.UseVisualStyleBackColor = true;
            this.btnAdicionar.Click += new System.EventHandler(this.btnAdicionar_Click);

            this.btnAlterar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnAlterar.Location = new Point(138, 438);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new Size(120, 32);
            this.btnAlterar.TabIndex = 7;
            this.btnAlterar.Text = "Alterar";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);

            this.btnRemover.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.btnRemover.Location = new Point(264, 438);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new Size(120, 32);
            this.btnRemover.TabIndex = 8;
            this.btnRemover.Text = "Remover";
            this.btnRemover.UseVisualStyleBackColor = true;
            this.btnRemover.Click += new System.EventHandler(this.btnRemover_Click);

            this.btnSair.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnSair.Location = new Point(668, 438);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new Size(120, 32);
            this.btnSair.TabIndex = 9;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);

            this.AcceptButton = this.btnPesquisar;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 482);
            this.Controls.Add(this.lblPesquisa);
            this.Controls.Add(this.txtPesquisa);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.btnListar);
            this.Controls.Add(this.dgvContatos);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnAdicionar);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.btnRemover);
            this.Controls.Add(this.btnSair);
            this.MinimumSize = new Size(700, 420);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Agenda de Contatos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblPesquisa;
        private TextBox txtPesquisa;
        private Button btnPesquisar;
        private Button btnListar;
        private DataGridView dgvContatos;
        private DataGridViewTextBoxColumn colNome;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colNascimento;
        private DataGridViewTextBoxColumn colIdade;
        private DataGridViewTextBoxColumn colTelefone;
        private Label lblTotal;
        private Button btnAdicionar;
        private Button btnAlterar;
        private Button btnRemover;
        private Button btnSair;
    }
}
