using System;
using System.Windows.Forms;

namespace Cliente
{
    public partial class CriaVotacao : Form
    {
        public static string [] votacaoAtual;
        public static String answer;
        public CriaVotacao()
        {
            InitializeComponent();
        }

        private void CriaVotacao_Load(object sender, EventArgs e)
        {
            tbTituloVotacao.Text = votacaoAtual[0];
            int conta = 0;
            try
            {
                tbOpcao1.Text = votacaoAtual[2];
                conta++;
            }
            catch { }
            try
            {
                tbOpcao2.Text = votacaoAtual[3];
                conta++;
            }
            catch { }
            try
            {
                tbOpcao3.Text = votacaoAtual[4];
                conta++;
            }
            catch { }
            try
            {
                tbOpcao4.Text = votacaoAtual[5];
                conta++;
            }
            catch { }
            try
            {
                tbOpcao5.Text = votacaoAtual[6];
                conta++;
            }
            catch { }
            try
            {
                tbOpcao6.Text = votacaoAtual[7];
                conta++;
            }
            catch { }

            cbNrOpcoes.SelectedIndex = conta - 2;
        }

        private void cbNrOpcoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            if(cb.SelectedIndex > -1)
            {
                label5.Visible = false;
                tbOpcao3.Visible = false;

                label6.Visible = false;
                tbOpcao4.Visible = false;

                label7.Visible = false;
                tbOpcao5.Visible = false;

                label8.Visible = false;
                tbOpcao6.Visible = false;
            }
            if(cb.SelectedIndex >= 1)
            {
                label5.Visible = true;
                tbOpcao3.Visible = true;
            }
            if (cb.SelectedIndex >= 2)
            {
                label6.Visible = true;
                tbOpcao4.Visible = true;
            }
            if (cb.SelectedIndex >= 3)
            {
                label7.Visible = true;
                tbOpcao5.Visible = true;
            }
            if (cb.SelectedIndex >= 4)
            {
                label8.Visible = true;
                tbOpcao6.Visible = true;
            }
        }

        private void btnSubmeter_Click(object sender, EventArgs e)
        {
            //mandar para o servidor a pergunta escolhida e as opções escolhidas
            answer = tbTituloVotacao.Text + ";" + cbNrOpcoes.SelectedItem + ";" + tbOpcao1.Text + ";" + tbOpcao2.Text + ";" + tbOpcao3.Text + ";" +
            tbOpcao4.Text + ";" + tbOpcao5.Text + ";" + tbOpcao6.Text;
            //servidor vai mandar para o user a lista
            this.Close();

            //o servidor vai mandar para o cliente user a lista a votar
        }
    }
}
