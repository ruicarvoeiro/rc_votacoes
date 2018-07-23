using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
   
    public partial class ResultadoPorQuestao : Form
    {
        public static string inputDoServidor;
        public ResultadoPorQuestao()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminScreen admistrador = new AdminScreen();
            admistrador.ShowDialog();
        }

        private void ResultadoPorQuestao_Load(object sender, EventArgs e)
        {
            texto.Text = inputDoServidor;
        }
    }
}
