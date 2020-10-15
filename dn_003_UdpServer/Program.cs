using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace dn_003_UdpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 10_000;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),port );

            while (true)
            {
                string time = DateTime.Now.ToLongTimeString();
                Console.WriteLine($"Sended: {time}");
                byte[] msg = Encoding.UTF8.GetBytes(time);
                socket.SendTo(msg, ep);
                Thread.Sleep(10_000);
            }
        }
    }
}
