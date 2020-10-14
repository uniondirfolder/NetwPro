using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dn_002_ServerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 10_000);
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client: " + client.Client.RemoteEndPoint);
                using (NetworkStream stream = client.GetStream())
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("Hello client");
                    }
                }
                client.Close();
            }

        }
    }
}
