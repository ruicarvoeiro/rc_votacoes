using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class SynchronousSocketListener
{
    static string pergunta;

    static Dictionary<string, int> votacao = new Dictionary<string, int>();

    static string nr_escolhido;

    public static void opcoesPorDefeito()
    {
        votacao.Add("EquipaA", 0);
        votacao.Add("EquipaB", 0);
        pergunta = "Qual a melhor equipa?";

    }

    public static void StartListening()
    {
        byte[] bytes = new Byte[1024]; //buffer

        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        String nomeDeUtilizador = "";
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

            while (true)
            {
                nr_escolhido = "";
                Console.WriteLine("Aguardando votos...");
                Socket handler = listener.Accept();
                byte[] msg;
                int bytesRec;

                Boolean loginOK = true;
                do
                {
                    bytes = new byte[1024];
                    bytesRec = handler.Receive(bytes); //Receção de dados
                    nomeDeUtilizador += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    bytes = new byte[1024];
                    bytesRec = handler.Receive(bytes); //Receção de dados
                    String passe = "";
                    passe += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    loginOK = LoginComSucesso(nomeDeUtilizador, passe);
                }
                while (!loginOK);

                if(nomeDeUtilizador == "user")
                {
                    msg = Encoding.ASCII.GetBytes(mostrarVotacao());
                    handler.Send(msg);
                }
                else
                {

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
        credenciaisLogin.Add("user", "maçoas");
        credenciaisLogin.Add("admin", "rossana");
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
        string estadoVotacao = "";
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
}
