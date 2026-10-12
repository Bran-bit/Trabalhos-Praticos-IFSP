using System.Drawing;
using System.Windows.Forms;

namespace Agenda
{
    partial class ContatoForm
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
            this.lblNome = new Label();
            this.txtNome = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblNascimento = new Label();
            this.dtpNascimento = new DateTimePicker();
            this.grpTelefones = new GroupBox();
            this.lblTipo = new Label();
            this.cboTipo = new ComboBox();
            this.lblNumero = new Label();
            this.txtNumero = new TextBox();
            this.chkPrincipal = new CheckBox();
            this.btnAdicionarTelefone = new Button();
            this.btnRemoverTelefone = new Button();
            this.dgvTelefones = new DataGridView();
            this.colTipo = new DataGridViewTextBoxColumn();
            this.colNumero = new DataGridViewTextBoxColumn();
            this.colPrincipal = new DataGridViewTextBoxColumn();
            this.btnSalvar = new Button();
            this.btnCancelar = new Button();
            this.grpTelefones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefones)).BeginInit();
            this.SuspendLayout();

            this.lblNome.AutoSize = true;
            this.lblNome.Location = new Point(15, 18);
            this.lblNome.Name = "lblNome";
            this.lblNome.TabIndex = 0;
            this.lblNome.Text = "Nome:";

            this.txtNome.Location = new Point(15, 38);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new Size(390, 23);
            this.txtNome.TabIndex = 1;

            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(15, 68);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "E-mail:";

            this.txtEmail.Location = new Point(15, 88);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(390, 23);
            this.txtEmail.TabIndex = 3;

            this.lblNascimento.AutoSize = true;
            this.lblNascimento.Location = new Point(15, 118);
            this.lblNascimento.Name = "lblNascimento";
            this.lblNascimento.TabIndex = 4;
            this.lblNascimento.Text = "Data de nascimento:";

            this.dtpNascimento.Format = DateTimePickerFormat.Custom;
            this.dtpNascimento.CustomFormat = "dd/MM/yyyy";
            this.dtpNascimento.Location = new Point(15, 138);
            this.dtpNascimento.Name = "dtpNascimento";
            this.dtpNascimento.Size = new Size(140, 23);
            this.dtpNascimento.TabIndex = 5;

            this.grpTelefones.Controls.Add(this.lblTipo);
            this.grpTelefones.Controls.Add(this.cboTipo);
            this.grpTelefones.Controls.Add(this.lblNumero);
            this.grpTelefones.Controls.Add(this.txtNumero);
            this.grpTelefones.Controls.Add(this.chkPrincipal);
            this.grpTelefones.Controls.Add(this.btnAdicionarTelefone);
            this.grpTelefones.Controls.Add(this.btnRemoverTelefone);
            this.grpTelefones.Controls.Add(this.dgvTelefones);
            this.grpTelefones.Location = new Point(15, 175);
            this.grpTelefones.Name = "grpTelefones";
            this.grpTelefones.Size = new Size(390, 225);
            this.grpTelefones.TabIndex = 6;
            this.grpTelefones.TabStop = false;
            this.grpTelefones.Text = "Telefones";

            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new Point(10, 22);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo:";

            this.cboTipo.Items.AddRange(new object[] {
                "Celular",
                "Residencial",
                "Comercial",
                "Outro"});
            this.cboTipo.Location = new Point(10, 42);
            this.cboTipo.Name = "cboTipo";
            this.cboTipo.Size = new Size(115, 23);
            this.cboTipo.TabIndex = 1;

            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new Point(135, 22);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.TabIndex = 2;
            this.lblNumero.Text = "Número:";

            this.txtNumero.Location = new Point(135, 42);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new Size(135, 23);
            this.txtNumero.TabIndex = 3;

            this.chkPrincipal.AutoSize = true;
            this.chkPrincipal.Location = new Point(285, 44);
            this.chkPrincipal.Name = "chkPrincipal";
            this.chkPrincipal.TabIndex = 4;
            this.chkPrincipal.Text = "Principal";
            this.chkPrincipal.UseVisualStyleBackColor = true;

            this.btnAdicionarTelefone.Location = new Point(10, 75);
            this.btnAdicionarTelefone.Name = "btnAdicionarTelefone";
            this.btnAdicionarTelefone.Size = new Size(170, 27);
            this.btnAdicionarTelefone.TabIndex = 5;
            this.btnAdicionarTelefone.Text = "Adicionar telefone";
            this.btnAdicionarTelefone.UseVisualStyleBackColor = true;
            this.btnAdicionarTelefone.Click += new System.EventHandler(this.btnAdicionarTelefone_Click);

            this.btnRemoverTelefone.Location = new Point(200, 75);
            this.btnRemoverTelefone.Name = "btnRemoverTelefone";
            this.btnRemoverTelefone.Size = new Size(170, 27);
            this.btnRemoverTelefone.TabIndex = 6;
            this.btnRemoverTelefone.Text = "Remover selecionado";
            this.btnRemoverTelefone.UseVisualStyleBackColor = true;
            this.btnRemoverTelefone.Click += new System.EventHandler(this.btnRemoverTelefone_Click);

            this.dgvTelefones.AllowUserToAddRows = false;
            this.dgvTelefones.AllowUserToDeleteRows = false;
            this.dgvTelefones.AllowUserToResizeRows = false;
            this.dgvTelefones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTelefones.Columns.AddRange(new DataGridViewColumn[] {
                this.colTipo,
                this.colNumero,
                this.colPrincipal});
            this.dgvTelefones.Location = new Point(10, 112);
            this.dgvTelefones.MultiSelect = false;
            this.dgvTelefones.Name = "dgvTelefones";
            this.dgvTelefones.ReadOnly = true;
            this.dgvTelefones.RowHeadersVisible = false;
            this.dgvTelefones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTelefones.Size = new Size(370, 105);
            this.dgvTelefones.TabIndex = 7;

            this.colTipo.FillWeight = 40F;
            this.colTipo.HeaderText = "Tipo";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;

            this.colNumero.FillWeight = 40F;
            this.colNumero.HeaderText = "Número";
            this.colNumero.Name = "colNumero";
            this.colNumero.ReadOnly = true;

            this.colPrincipal.FillWeight = 20F;
            this.colPrincipal.HeaderText = "Principal";
            this.colPrincipal.Name = "colPrincipal";
            this.colPrincipal.ReadOnly = true;

            this.btnSalvar.Location = new Point(225, 410);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new Size(85, 30);
            this.btnSalvar.TabIndex = 7;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);

            this.btnCancelar.DialogResult = DialogResult.Cancel;
            this.btnCancelar.Location = new Point(320, 410);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new Size(85, 30);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new Size(420, 455);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblNascimento);
            this.Controls.Add(this.dtpNascimento);
            this.Controls.Add(this.grpTelefones);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContatoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Contato";
            this.grpTelefones.ResumeLayout(false);
            this.grpTelefones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelefones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblNome;
        private TextBox txtNome;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblNascimento;
        private DateTimePicker dtpNascimento;
        private GroupBox grpTelefones;
        private Label lblTipo;
        private ComboBox cboTipo;
        private Label lblNumero;
        private TextBox txtNumero;
        private CheckBox chkPrincipal;
        private Button btnAdicionarTelefone;
        private Button btnRemoverTelefone;
        private DataGridView dgvTelefones;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colNumero;
        private DataGridViewTextBoxColumn colPrincipal;
        private Button btnSalvar;
        private Button btnCancelar;
    }
}
