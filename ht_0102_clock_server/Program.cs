using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ht_0102_clock_server
{
    internal class ClockServer
    {
        private static readonly Socket _socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.IP);
        private static IPEndPoint _endPoint = default(IPEndPoint);
        private static byte[] _buff;
        private static void InitServer(string ipAddress = "default", int port = 55555)
        {
            if (ipAddress == "default" && port >= 0 && port <= 65535)
            {
                _endPoint = new IPEndPoint(IPAddress.Loopback, port);
            }
            else if (port >= 0 && port <= 65535)
            {
                _endPoint = IPAddress.TryParse(ipAddress, out var temp) ? new IPEndPoint(temp, port) : new IPEndPoint(IPAddress.Loopback, port);
            }
            else
            {
                _endPoint = new IPEndPoint(IPAddress.Loopback, 55555);
            }

            _buff = new byte[1024];
        }
        private static Task<Socket> ServerAcceptAsync()
        {
            return Task<Socket>.Factory.FromAsync(_socket.BeginAccept,_socket.EndAccept, null);
        }
        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            Socket client = s.EndAccept(ar);
            Console.WriteLine($"Listen for {client.RemoteEndPoint}");

            client.BeginReceive(_buff, 0, _buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback),client);

            _socket.BeginAccept(new AsyncCallback(AcceptCallback), s);
        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            if (ar.AsyncState is Socket socket)
            {
                int count = socket.EndReceive(ar);
                string msg = Encoding.ASCII.GetString(_buff, 0, count);

                Console.WriteLine($"Message from client {socket.RemoteEndPoint} -> give me {msg}");
                DateTime temp = DateTime.Now;

                if (msg == "time")
                {
                    socket.Send(Encoding.UTF8.GetBytes(temp.ToLongTimeString()));

                    // return time
                }
                else if (msg == "date")
                {
                    socket.Send(Encoding.UTF8.GetBytes(temp.ToLongDateString()));
                }
                else
                {
                    socket.Send(Encoding.ASCII.GetBytes("NaN"));
                }
            }
        }
        private static void StartServer()
        {
            try
            {
                _socket.Bind(_endPoint);
                _socket.Listen(10);

                Console.WriteLine("Server Clock Start");

                _socket.BeginAccept(new AsyncCallback(AcceptCallback), _socket);

                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        static void Main(string[] args)
        {
            Console.Title = "SERVER TIME-DATE GET";
            if (args.Length > 1)
            {
                int.TryParse(args[1], out var temp);
                InitServer(args[0],temp);
            }
            else
            {
                InitServer();
            }

            StartServer();

            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
        }
    }
}
