using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace dp_002_AsyncServer
{
    class DateTimeServer:IDisposable
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        private IPEndPoint _ep = null;
        private int _backlog;
        byte[] buff = new byte[1024];
        public DateTimeServer(IPAddress ipAddress, int port, int backlog = 10)
        {
            _ep = new IPEndPoint(ipAddress, port);
            _backlog = backlog;
        }

        public void Start()
        {
            _socket.Bind(_ep);
            _socket.Listen(_backlog);
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), _socket);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            Socket client = s.EndAccept(ar);
            Console.WriteLine("Listen for {0} ", client.RemoteEndPoint);

            
            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);

            _socket.BeginAccept(new AsyncCallback(AcceptCallback), _socket);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            Socket c = ar.AsyncState as Socket;
            int count = c.EndReceive(ar);
            string msg = Encoding.ASCII.GetString(buff, 0, count);

            Console.WriteLine("Msg from client {0}: {1}", c.RemoteEndPoint,msg);

            switch (msg)
            {
                case "time":
                    break;
                case "date":
                    break;
                default:
                    break;
            }

        }

        public void Dispose()
        {
            if (_socket.Connected)
            {
                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }

            _socket?.Dispose();
        }
    }
}
