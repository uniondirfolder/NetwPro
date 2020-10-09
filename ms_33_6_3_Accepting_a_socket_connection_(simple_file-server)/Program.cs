using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ms_33_6_3_Accepting_a_socket_connection__simple_file_server_
{
    public class MainClass
    {
        private static void HandleRequest(object state)
        {
            using (Socket client = (Socket)state)
            using (NetworkStream stream = new NetworkStream(client))
            using (StreamReader reader = new StreamReader(stream))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                string fileName = reader.ReadLine();
                writer.Write(File.ReadAllText(fileName));
            }
        }
        static void Main(string[] args)
        {
            using (Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                s.Bind(new IPEndPoint(IPAddress.Loopback, 55555));
                s.Listen(3);

                Socket client = s.Accept();
                ThreadPool.QueueUserWorkItem(HandleRequest, client);
            }
        }
    }
}

//http://www.java2s.com/Tutorial/CSharp/0580__Network/SimpleTcpserverreceivedatafromaclient.htm