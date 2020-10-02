using System;
using System.Net;
using System.Net.Sockets;

namespace gl_001
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("207.46.197.32");
            IPEndPoint ep = new IPEndPoint(ip, 80);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }
    }
}
