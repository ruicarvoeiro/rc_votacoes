using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace Cliente
{
    static class Program
    {
        static String opcaoEscolhida;
        static Boolean ok_foi_clicked = false;
        static Form verVotacao;
        public static void StartClient()
        {
            byte[] bytes = new byte[1024]; //buffer
            try
            {
                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);
                
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);
                    int bytesRec;
                    byte[] msg;
                    int bytesSent;
                    String loginCredentials = "";
                    Boolean loginOK = true;
                    String utilizador = "";
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    do
                    {
                        Application.Run(new Login());
                        loginCredentials = Login.resposta;
                        if (loginCredentials == " ") break;
                        msg = Encoding.ASCII.GetBytes(loginCredentials); //Transforma o texto em Bytes
                        bytesSent = sender.Send(msg);
                        bytesRec = sender.Receive(bytes);
                        String ans = "";
                        ans += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        loginOK = ans == "Login efetuado com sucesso";
                        MessageBox.Show(ans);
                        utilizador = loginCredentials.Split(';')[0];
                    } while (!loginOK);

                    if (utilizador == "user")
                    {
                        bytesRec = sender.Receive(bytes);
                        String recv = "";
                        recv += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        String[] opcoes = recv.Split(';');

                        Label pergunta = new Label();
                        verVotacao = new Form();
                        pergunta.Width = verVotacao.Width - 20;
                        pergunta.Text = opcoes[0];
                        pergunta.Top = 10;
                        pergunta.Left = 20;
                        verVotacao.Controls.Add(pergunta);
                        
                        RadioButton opcao;                        
                        for (int i = 2; i < opcoes.Length; i++)
                        {
                            opcao = new RadioButton();
                            opcao.Left = 20;
                            opcao.Text = opcoes[i];
                            opcao.Top += 30 * i;
                            opcao.CheckedChanged += new EventHandler(opcao_checked);
                            verVotacao.Controls.Add(opcao);
                            if (i == 2)
                            {
                                opcaoEscolhida = opcao.Text;
                                opcao.Checked = true;
                            }
                        }
                       
                        Button ok = new Button();
                        ok.Text = "Ok";
                        ok.Left = 50;
                        ok.Top = 30 * (opcoes.Length);
                        
                        ok.Click += new EventHandler(ok_Click);
                        verVotacao.Controls.Add(ok);
                        if (opcoes[1] != "") MessageBox.Show(opcoes[1]);
                        Application.Run(verVotacao);
                        
                        while (!ok_foi_clicked)
                        {

                        };
                        msg = Encoding.ASCII.GetBytes(opcaoEscolhida); //Transforma o texto em Bytes
                        bytesSent = sender.Send(msg);

                    }
                    else if(utilizador == "admin")
                    {
                        bytesRec = sender.Receive(bytes);
                        String ans = "";
                        ans += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        //Votacao

                        String opcao;
                        do
                        {
                            Application.Run(new AdminScreen());
                            opcao = AdminScreen.opcaoEscolhida;

                            msg = Encoding.ASCII.GetBytes(opcao); //Transforma o texto em Bytes
                            bytesSent = sender.Send(msg);
                            if (opcao == "1")
                            {
                                bytesRec = sender.Receive(bytes);
                                ans = "";
                                ans += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                CriaVotacao.votacaoAtual = ans.Split(';');

                                Application.Run(new CriaVotacao());
                                msg = Encoding.ASCII.GetBytes(CriaVotacao.answer); //Transforma o texto em Bytes
                                bytesSent = sender.Send(msg);
                            }

                            else if(opcao == "2")
                            {
                                bytesRec = sender.Receive(bytes);
                                ans = "";
                                ans += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                                String[] obj = ans.Split(';');
                                ans = "";
                                for(int i = 0; i < obj.Length; i++)
                                {
                                    ans += obj[i] + '\n';
                                }
                                ResultadoPorQuestao.inputDoServidor = ans;
                                Application.Run(new ResultadoPorQuestao());
                            }

                            else if(opcao == "3")
                            {
                                msg = Encoding.ASCII.GetBytes(AdminScreen.notaFormativa); //Transforma o texto em Bytes
                                bytesSent = sender.Send(msg);
                            }
                        }
                        while (opcao != "4");
                        
                    }
                    sender.Shutdown(SocketShutdown.Both); //Desliga o socket
                    sender.Close(); //Fecha o socket
                    
                }
            
                catch (ArgumentNullException ane) //Excepções
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        static void ok_Click(object sender, System.EventArgs e)
        {
    
            ok_foi_clicked = true;
            MessageBox.Show("Obrigado pelo seu voto!");
            verVotacao.Close();

        }
        
        static void opcao_checked(object sender, System.EventArgs e)
        {
            opcaoEscolhida = ((RadioButton) sender).Text;
        }

        static void Main()
        {
            StartClient();
        }
    }
}
