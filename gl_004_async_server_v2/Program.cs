using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gl_004_async_server_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncServer server = new AsyncServer("127.0.0.1", 1024);

            server.StartServer();
            Console.Read();
        }

        class AsyncServer
        {
            IPEndPoint endPoint;
            Socket socket;
            public AsyncServer(string strAddr, int port)
            {
                endPoint = new IPEndPoint(IPAddress.Parse(strAddr), port);
            }

            void MyAcceptCallBackFunction(IAsyncResult ia) 
            {
                Socket socket = (Socket)ia.AsyncState;
                Socket ns = socket.EndAccept(ia);

                Console.WriteLine(ns.RemoteEndPoint.ToString());

                byte[] sendBuffer = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

                ns.BeginSend(
                    sendBuffer, 
                    0, 
                    sendBuffer.Length, 
                    SocketFlags.None, 
                    new AsyncCallback(MySendCallBackFunction), 
                    ns);
                socket.BeginAccept(
                    new AsyncCallback(MyAcceptCallBackFunction),
                    socket);
            }

            private void MySendCallBackFunction(IAsyncResult ia)
            {
                Socket ns = (Socket)ia.AsyncState;
                int n = ((Socket)ia.AsyncState).EndSend(ia);
                ns.Shutdown(SocketShutdown.Send);
                ns.Close();
            }

            public void StartServer() 
            {
                if (socket != null)
                    return;
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                socket.Bind(endPoint);
                socket.Listen(10);

                socket.BeginAccept(new AsyncCallback(MyAcceptCallBackFunction), socket);
            }
        }
    }
}
