using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _33_6_2_Use_background_thread_to_deal_with_the_Server_socket
{
    class Program
    {
        private const int CONNECT_QUEUE_LENGTH = 4;

        static void ListenForRequests()
        {
            Socket listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSock.Bind(new IPEndPoint(IPAddress.Any, 55555));
            listenSock.Listen(CONNECT_QUEUE_LENGTH);

            while (true)
            {
                Socket newConnection = listenSock.Accept();
                byte[] msg = Encoding.UTF8.GetBytes("Hello!");

                newConnection.BeginSend(msg, 0, msg.Length, SocketFlags.None, null, null);
            }
        }

        static void Main(string[] args)
        {
            Thread listener = new Thread(new ThreadStart(ListenForRequests));
            listener.IsBackground = true;
            listener.Start();

            Console.WriteLine("Press <enter> to quit");
            Console.ReadLine();
        }
    }
}
