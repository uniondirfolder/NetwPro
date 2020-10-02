using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gl_002_n
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip,25);

            socket.Bind(ep);
            socket.Listen(10);

            try
            {
                while (true)
                {
                    Socket ns = socket.Accept();
                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    ns.Send(Encoding.ASCII.GetBytes(DateTime.Now.ToString()));
                    ns.Shutdown(SocketShutdown.Both);
                    ns.Close();
                }
            }
            catch (SocketException ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
