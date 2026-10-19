namespace ProjetoAtendimento
{
    partial class Form1
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
            this.btnGerar = new System.Windows.Forms.Button();
            this.btnAdicionarGuiche = new System.Windows.Forms.Button();
            this.btnChamar = new System.Windows.Forms.Button();
            this.btnListarSenhas = new System.Windows.Forms.Button();
            this.btnListarAtendimentos = new System.Windows.Forms.Button();
            this.lstSenhas = new System.Windows.Forms.ListBox();
            this.lstAtendimentos = new System.Windows.Forms.ListBox();
            this.txtGuicheId = new System.Windows.Forms.TextBox();
            this.lblQtdGuiches = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            // btnGerar
            this.btnGerar.Location = new System.Drawing.Point(12, 12);
            this.btnGerar.Name = "btnGerar";
            this.btnGerar.Size = new System.Drawing.Size(120, 30);
            this.btnGerar.TabIndex = 0;
            this.btnGerar.Text = "Gerar Senha";
            this.btnGerar.UseVisualStyleBackColor = true;
            this.btnGerar.Click += new System.EventHandler(this.btnGerar_Click);
            
            // btnAdicionarGuiche
            this.btnAdicionarGuiche.Location = new System.Drawing.Point(12, 48);
            this.btnAdicionarGuiche.Name = "btnAdicionarGuiche";
            this.btnAdicionarGuiche.Size = new System.Drawing.Size(120, 30);
            this.btnAdicionarGuiche.TabIndex = 1;
            this.btnAdicionarGuiche.Text = "Adicionar Guichê";
            this.btnAdicionarGuiche.UseVisualStyleBackColor = true;
            this.btnAdicionarGuiche.Click += new System.EventHandler(this.btnAdicionarGuiche_Click);
            
            // lblQtdGuiches
            this.lblQtdGuiches.AutoSize = true;
            this.lblQtdGuiches.Location = new System.Drawing.Point(138, 56);
            this.lblQtdGuiches.Name = "lblQtdGuiches";
            this.lblQtdGuiches.Size = new System.Drawing.Size(95, 15);
            this.lblQtdGuiches.TabIndex = 2;
            this.lblQtdGuiches.Text = "Guichês ativos: 0";
            
            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID do Guichê:";
            
            // txtGuicheId
            this.txtGuicheId.Location = new System.Drawing.Point(94, 92);
            this.txtGuicheId.Name = "txtGuicheId";
            this.txtGuicheId.Size = new System.Drawing.Size(50, 23);
            this.txtGuicheId.TabIndex = 4;
            
            // btnChamar
            this.btnChamar.Location = new System.Drawing.Point(150, 87);
            this.btnChamar.Name = "btnChamar";
            this.btnChamar.Size = new System.Drawing.Size(120, 30);
            this.btnChamar.TabIndex = 5;
            this.btnChamar.Text = "Chamar Senha";
            this.btnChamar.UseVisualStyleBackColor = true;
            this.btnChamar.Click += new System.EventHandler(this.btnChamar_Click);
            
            // btnListarSenhas
            this.btnListarSenhas.Location = new System.Drawing.Point(12, 130);
            this.btnListarSenhas.Name = "btnListarSenhas";
            this.btnListarSenhas.Size = new System.Drawing.Size(258, 30);
            this.btnListarSenhas.TabIndex = 6;
            this.btnListarSenhas.Text = "Listar Senhas (Fila)";
            this.btnListarSenhas.UseVisualStyleBackColor = true;
            this.btnListarSenhas.Click += new System.EventHandler(this.btnListarSenhas_Click);
            
            // lstSenhas
            this.lstSenhas.FormattingEnabled = true;
            this.lstSenhas.ItemHeight = 15;
            this.lstSenhas.Location = new System.Drawing.Point(12, 166);
            this.lstSenhas.Name = "lstSenhas";
            this.lstSenhas.Size = new System.Drawing.Size(258, 244);
            this.lstSenhas.TabIndex = 7;
            
            // btnListarAtendimentos
            this.btnListarAtendimentos.Location = new System.Drawing.Point(286, 130);
            this.btnListarAtendimentos.Name = "btnListarAtendimentos";
            this.btnListarAtendimentos.Size = new System.Drawing.Size(350, 30);
            this.btnListarAtendimentos.TabIndex = 8;
            this.btnListarAtendimentos.Text = "Listar Atendimentos do Guichê";
            this.btnListarAtendimentos.UseVisualStyleBackColor = true;
            this.btnListarAtendimentos.Click += new System.EventHandler(this.btnListarAtendimentos_Click);
            
            // lstAtendimentos
            this.lstAtendimentos.FormattingEnabled = true;
            this.lstAtendimentos.ItemHeight = 15;
            this.lstAtendimentos.Location = new System.Drawing.Point(286, 166);
            this.lstAtendimentos.Name = "lstAtendimentos";
            this.lstAtendimentos.Size = new System.Drawing.Size(350, 244);
            this.lstAtendimentos.TabIndex = 9;
            
            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 422);
            this.Controls.Add(this.lstAtendimentos);
            this.Controls.Add(this.btnListarAtendimentos);
            this.Controls.Add(this.lstSenhas);
            this.Controls.Add(this.btnListarSenhas);
            this.Controls.Add(this.btnChamar);
            this.Controls.Add(this.txtGuicheId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblQtdGuiches);
            this.Controls.Add(this.btnAdicionarGuiche);
            this.Controls.Add(this.btnGerar);
            this.Name = "Form1";
            this.Text = "Projeto Atendimento - Filas";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnGerar;
        private System.Windows.Forms.Button btnAdicionarGuiche;
        private System.Windows.Forms.Button btnChamar;
        private System.Windows.Forms.Button btnListarSenhas;
        private System.Windows.Forms.Button btnListarAtendimentos;
        private System.Windows.Forms.ListBox lstSenhas;
        private System.Windows.Forms.ListBox lstAtendimentos;
        private System.Windows.Forms.TextBox txtGuicheId;
        private System.Windows.Forms.Label lblQtdGuiches;
        private System.Windows.Forms.Label label1;
    }
}