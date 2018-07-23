using System;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Login : Form
    {
        
        public static String resposta = " ";
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            resposta = tbUser.Text + ";" + tbPass.Text;
                this.Close(); 
        }

        private void tbuser_TextChanged(object sender, EventArgs e)
        {
            
            btnOK.Enabled = tbUser.Text.Trim().Length > 0 && tbPass.Text.Trim().Length > 0;
        }

        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = tbUser.Text.Trim().Length > 0 && tbPass.Text.Trim().Length > 0;
            tbPass.PasswordChar = '*';
            tbPass.MaxLength = 3;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
