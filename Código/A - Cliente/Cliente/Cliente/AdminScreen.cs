using System;
using System.Windows.Forms;

namespace Cliente
{
    public partial class AdminScreen : Form
    {
        public static String opcaoEscolhida;
        public static String notaFormativa;
        public AdminScreen()
        {
            InitializeComponent();
        }

        private void btnIniciarEditar_Click(object sender, EventArgs e)
        {
            opcaoEscolhida = "1";
            this.Close();
        }

        private void btnFim_Click(object sender, EventArgs e)
        {
            opcaoEscolhida = "2";
            this.Close();
        }

        private void AdminScreen_Load(object sender, EventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            opcaoEscolhida = "3";
            notaFormativa = textBox1.Text;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            opcaoEscolhida = "4";
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnEnviar.Enabled = textBox1.Text.Trim().Length > 0;
        }
    }
}
