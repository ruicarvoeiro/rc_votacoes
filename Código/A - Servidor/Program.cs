using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class SynchronousSocketListener
{
    static string pergunta;

    static String notaInformativa;

    static Dictionary<string, int> votacao = new Dictionary<string, int>();

    public static void opcoesPorDefeito()
    {
        votacao.Add("Equipa A", 0);
        votacao.Add("Equipa B", 0);
        votacao.Add("Equipa C", 0);
        pergunta = "Qual a melhor equipa?";

    }

    public static void StartListening()
    {
        if (pergunta == null) opcoesPorDefeito();
        byte[] bytes = new Byte[1024]; //buffer
        Console.WriteLine("Iniciando o servidor...");
        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            while (true)
            {
                String nomeDeUtilizador = "";
                Console.WriteLine("Há espera de utilizadores...");
                Socket handler = listener.Accept();
                byte[] msg;
                int bytesRec;

                Boolean loginOK = true;
                do
                {
                    Console.WriteLine("Iniciando sessão...");
                    String input = "";
                    try
                    {
                        bytes = new byte[1024];
                        bytesRec = handler.Receive(bytes);
                        input += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        nomeDeUtilizador = input.Split(';')[0];
                        String passe = input.Split(';')[1];
                        loginOK = LoginComSucesso(nomeDeUtilizador, passe);
                        String mensagem = "";
                        if (loginOK) mensagem = "Login efetuado com sucesso";
                        else mensagem = "Username/Password incorretos";
                        if (mensagem == " ") break;
                        msg = Encoding.ASCII.GetBytes(mensagem);
                        Console.WriteLine(mensagem);
                        handler.Send(msg);
                    }
                    catch
                    {
                        break;
                    }
                }
                while (!loginOK);

                if (nomeDeUtilizador == "user")
                {
                    Console.WriteLine(mostrarVotacao());
                    Console.WriteLine("Sessão iniciada como: {0}", nomeDeUtilizador);
                    msg = Encoding.ASCII.GetBytes(enviaVotacao());
                    handler.Send(msg);

                    try {
                        bytes = new byte[1024];
                        bytesRec = handler.Receive(bytes);
                        string input = "";
                        input += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        votar(input);
                        Console.WriteLine("Voto para: " + input);
                        Console.WriteLine("Estado votacao: " + mostrarVotacao());
                    }
                    catch { }

                }
                else if(nomeDeUtilizador == "admin")
                {
                    Console.WriteLine("Sessão iniciada como: {0}", nomeDeUtilizador);
                    msg = Encoding.ASCII.GetBytes(mostrarVotacao());
                    handler.Send(msg);
                    string input;
                    do
                    {
                        bytes = new byte[1024];
                        bytesRec = handler.Receive(bytes);
                        input = "";
                        input += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                        if (input == "1")
                        {
                            msg = Encoding.ASCII.GetBytes(enviaVotacao());
                            handler.Send(msg);

                            bytes = new byte[1024];
                            bytesRec = handler.Receive(bytes);
                            input = "";
                            input += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            criarVotacao(input);
                        }
                        
                        else if(input == "2")
                        {
                           msg = Encoding.ASCII.GetBytes(mostrarPercentagens());
                           handler.Send(msg);
                           apagarTudo();
                        }

                        else if (input == "3")
                        {
                            bytes = new byte[1024];
                            bytesRec = handler.Receive(bytes);
                            input = "";
                            input += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                            notaInformativa = input;
                        }
                        
                    }
                    while (input != "4");
                    
                }

                handler.Shutdown(SocketShutdown.Both); //Desligar o socket
                handler.Close(); //Fecho do socket
            }

        }
        catch (Exception e) //Excepções
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static Boolean LoginComSucesso(String userName, String password)
    {
        Dictionary<string, string> credenciaisLogin = new Dictionary<string, string>();
        credenciaisLogin.Add("user", "123");
        credenciaisLogin.Add("admin", "abc");
        return credenciaisLogin.ContainsKey(userName) && credenciaisLogin[userName] == password;
    }

    public static void votar(String voto)
    {
        try
        {
            votacao[voto] += 1;
        }
        catch
        {
            votacao[voto] = 1;
        }
     }

    public static String mostrarVotacao() {
        string estadoVotacao = pergunta + "\n";
        foreach (KeyValuePair<string, int> valor in votacao)
        {
            estadoVotacao += string.Format("{0}: {1} votos\n", valor.Key, valor.Value);
        }
        return estadoVotacao;
    }

    public static int Main(String[] args)
    {
        StartListening();
        Console.ReadLine();
        return 0;
    }

    public static String enviaVotacao()
    {
        string opcoes = pergunta + ";" + notaInformativa + ";";
        foreach (KeyValuePair<string, int> valor in votacao)
        {
            opcoes += string.Format("{0};", valor.Key);
        }
        opcoes = opcoes.Substring(0, opcoes.Length-1);
        return opcoes;
    }

    public static void criarVotacao(String inputDoCliente)
    {
        String[] cenas = inputDoCliente.Split(';');
        pergunta = cenas[0];
        votacao = new Dictionary<string, int>();
        for (int i = 2; i <= Convert.ToInt32(cenas[1]) + 1; i++)
        {
            votacao.Add(cenas[i], 0);
        }
    }

    public static void apagarTudo()
    {
        votacao = new Dictionary<string, int>();
        pergunta = null;
    }

    public static String mostrarPercentagens()
    {
        string opcoes = pergunta + ";";
        int total = 0;
        String[] max = { "a", "a" };
        
        foreach (KeyValuePair<string, int> valor in votacao)
        {
            max[0] = valor.Key; //opcao
            max[1] = Convert.ToString(valor.Value); //nr Votos
            total += Convert.ToInt32(valor.Value);
        }
        foreach (KeyValuePair<string, int> valor in votacao)
        {
            if(Convert.ToInt32(max[1]) < Convert.ToInt32(valor.Value))
            {
                max[1] = Convert.ToString(valor.Value);
                max[0] = valor.Key;
            }
            opcoes += string.Format("{0} : {1} votos ({2}%);", valor.Key, Convert.ToInt32(valor.Value), Convert.ToInt32(valor.Value)/total*100);
        }
        opcoes = opcoes.Substring(0, opcoes.Length - 1);
        opcoes += String.Format("\n\nOpcao mais escolhida: opcao {0}, com {1} votos ({2}%)", max[0], max[1], Convert.ToInt32(max[1])/total*100);
        return opcoes;
    }
}
