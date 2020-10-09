using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms_33_6_1_Listen_for_Socket_Request_in_Thread
{
    class Program
    {
        static void ListenForRequests()
        {
            int CONNECT_QUEUE_LENGTH = 4;

            Socket listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSock.Bind(new IPEndPoint(IPAddress.Any, 55555));
            listenSock.Listen(CONNECT_QUEUE_LENGTH);

            while (true)
            {
                using Socket newConnection = listenSock.Accept();
                // Send the data.
                byte[] msg = Encoding.UTF8.GetBytes("Hello World!");

                newConnection.Send(msg, SocketFlags.None);
            }
        }

        static void Main(string[] args)
        {
            // Start the listening thread.
            Thread listener = new Thread(new ThreadStart(ListenForRequests));

            listener.IsBackground = true;
            
            listener.Start();

            Console.WriteLine("Press <enter> to quit");
            Console.ReadLine();
            
            listener.Abort();
        }
    }
}
