using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dn_001_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 10_000;
            String serverIp = "127.0.0.1";
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            IPAddress serverAddress = IPAddress.Parse(serverIp);
            IPEndPoint serverEndPoint = new IPEndPoint(serverAddress, PORT);

            clientSocket.Connect(serverEndPoint);
            byte[] buff = new byte[255];
            if (clientSocket.Connected)
            {

                int len = clientSocket.Receive(buff);
                String msg = Encoding.UTF8.GetString(buff,0, len);
                Console.WriteLine(msg);
            }
        }
    }
}
