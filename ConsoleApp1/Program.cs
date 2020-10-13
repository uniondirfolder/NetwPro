using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 10_000;

            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork, // ipv4
                SocketType.Stream,          // tcp
                ProtocolType.IP);           // IP

            IPAddress addr = IPAddress.Parse("127.0.0.1"); //localhost
            IPEndPoint endPoint = new IPEndPoint(addr, PORT);

            serverSocket.Bind(endPoint);    // привязываем к конечной точке
            serverSocket.Listen(10);  // очередь клиентов

            while (true)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] Start lithening");
                Socket remoteSocket = serverSocket.Accept();                        //Слушаем входящие соединения
                Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] Connectied to: {remoteSocket.RemoteEndPoint}");
                byte[] msg = Encoding.UTF8.GetBytes("Hello world");
                remoteSocket.Send(msg);
                Console.WriteLine("Close connection");
                remoteSocket.Shutdown(SocketShutdown.Both);
                remoteSocket.Close();
            }
        }
    }
}
