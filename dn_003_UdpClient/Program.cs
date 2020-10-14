using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dn_003_UdpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            const int port = 10_000;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


            IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);

            socket.Bind(ep);
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

            byte[]buff = new byte[255];

            while (true)
            {
                int count = socket.ReceiveFrom(buff, ref remoteEP);
                string msg = Encoding.UTF8.GetString(buff, 0, count);
                Console.WriteLine($"Received from {remoteEP} msg: {msg}");
            }
        }
    }
}
