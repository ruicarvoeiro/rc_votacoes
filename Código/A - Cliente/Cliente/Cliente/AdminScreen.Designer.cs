namespace Cliente
{
    partial class AdminScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFim = new System.Windows.Forms.Button();
            this.btnIniciarEditar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.btnSair = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFim
            // 
            this.btnFim.Location = new System.Drawing.Point(77, 57);
            this.btnFim.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFim.Name = "btnFim";
            this.btnFim.Size = new System.Drawing.Size(158, 23);
            this.btnFim.TabIndex = 9;
            this.btnFim.Text = "Finalizar Votação";
            this.btnFim.UseVisualStyleBackColor = true;
            this.btnFim.Click += new System.EventHandler(this.btnFim_Click);
            // 
            // btnIniciarEditar
            // 
            this.btnIniciarEditar.Location = new System.Drawing.Point(77, 11);
            this.btnIniciarEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnIniciarEditar.Name = "btnIniciarEditar";
            this.btnIniciarEditar.Size = new System.Drawing.Size(158, 23);
            this.btnIniciarEditar.TabIndex = 1;
            this.btnIniciarEditar.Text = "Iniciar/Editar Votação ";
            this.btnIniciarEditar.UseVisualStyleBackColor = true;
            this.btnIniciarEditar.Click += new System.EventHandler(this.btnIniciarEditar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Notas Informativas:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 120);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(280, 22);
            this.textBox1.TabIndex = 12;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Enabled = false;
            this.btnEnviar.Location = new System.Drawing.Point(77, 161);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(75, 23);
            this.btnEnviar.TabIndex = 13;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(166, 161);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(69, 23);
            this.btnSair.TabIndex = 14;
            this.btnSair.Text = "Exit";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.button1_Click);
            // 
            // AdminScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 201);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnIniciarEditar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFim);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AdminScreen";
            this.Text = "Admistrador";
            this.Load += new System.EventHandler(this.AdminScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFim;
        private System.Windows.Forms.Button btnIniciarEditar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnSair;
    }
}