using System;
using System.Collections;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace ms_001_ChatServer
{
    class Program
    {
        public static Hashtable clientList = new Hashtable();
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("Chat Server started .....");
            counter = 0;

            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();

                //byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;

                NetworkStream networkStream = clientSocket.GetStream();

                int test = clientSocket.ReceiveBufferSize;
                byte[] bytesFrom = new byte[test];

                networkStream.Read(bytesFrom, 0, (int) clientSocket.ReceiveBufferSize);
                dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                clientList.Add(dataFromClient, clientSocket);

                Broadcast(dataFromClient+" Joined ", dataFromClient,false);

                Console.WriteLine(dataFromClient + " Joined chat room ");
                HandleClient client = new HandleClient();
                client.StartClient(clientSocket,dataFromClient,clientList);
            }
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine("exit");
            Console.ReadKey();

        }

        public static void Broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry item in clientList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient) item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes,0,broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }

        public class HandleClient
        {
            private TcpClient clientSocet;
            private string clNo;
            private Hashtable clientsList;

            public void StartClient(TcpClient inClientSocket, string clieneNo, Hashtable cList)
            {
                this.clientSocet = inClientSocket;
                this.clNo = clieneNo;
                this.clientsList = cList;
                Thread ctThread = new Thread(DoChat);
                ctThread.Start();
            }

            private void DoChat()
            {
                int requestCount = 0;
                byte[] bytesFrom = new byte[10025];
                string dataFromClient = null;
                Byte[] sendBytes = null;
                string serverResponse = null;
                requestCount = 0;

                while ((true))
                {
                    try
                    {
                        requestCount = requestCount + 1;
                        NetworkStream networkStream = clientSocet.GetStream();

                        int test = clientSocet.ReceiveBufferSize;
                        bytesFrom = new byte[test];

                        networkStream.Read(bytesFrom, 0, (int) clientSocet.ReceiveBufferSize);
                        dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                        Program.Broadcast(dataFromClient,clNo,true);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        
                    }
                }
            }
        }
    }
}
