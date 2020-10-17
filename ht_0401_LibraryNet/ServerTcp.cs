using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ht_0401_LibraryNet
{
    public class NetwMsg
    {
        public IPEndPoint IpEndPoint { get; set; }
        public string Body { get; set; }
        public override string ToString()
        {
            return $"{IpEndPoint.Address}+{IpEndPoint.Port}+{Body}";
        }

        public NetwMsg(IPEndPoint ipEndPoint, string body)
        {
            this.Body = body;
            this.IpEndPoint = new IPEndPoint(ipEndPoint.Address,ipEndPoint.Port);
        }
        public NetwMsg(string netwMsg)
        {
            string[] buff = netwMsg.Split('+');
            if (buff.Length == 3)
            {
                this.IpEndPoint = new IPEndPoint(IPAddress.Parse(buff[0]), int.Parse(buff[1]));
                this.Body = buff[2];
            }
        }
    }
    public class TcpSocket : IDisposable
    {
        public Socket Listener { get; set; }
        public Socket Request { get; set; }
        public IPEndPoint RemoteIpEndPoint { get; set; }
        public IPEndPoint LocalEndPoint { get; set; }

        byte[] buff = new byte[1024];
        public TcpSocket(IPAddress remoteIpEndPoint, int remotePort)
        {
            try
            {
                Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                Request = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                RemoteIpEndPoint = new IPEndPoint(remoteIpEndPoint, remotePort+1);
                LocalEndPoint = new IPEndPoint(IPAddress.Loopback, RemoteIpEndPoint.Port);
                //Listener.Bind(LocalEndPoint);
                //Listener.Listen(10);
               // Request.Connect(RemoteIpEndPoint);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw new SocketException(0000);
            }

        }

        public void MakeNewConnect()
        {
            Request = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Request.Connect(RemoteIpEndPoint);
        }

        public void Start()
        {
            Listener.Bind(RemoteIpEndPoint);
            Listener.Listen(10);
            Listener.BeginAccept(new AsyncCallback(AcceptCallback), Listener);
            Request.Connect(RemoteIpEndPoint);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket s = ar.AsyncState as Socket;
            Socket client = s.EndAccept(ar);
            //Console.WriteLine("Listen for {0} ", client.RemoteEndPoint);

            buff = Encoding.UTF8.GetBytes("555");

            client.BeginReceive(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);

            Listener.BeginAccept(new AsyncCallback(AcceptCallback), Listener);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            Socket c = ar.AsyncState as Socket;
            int count = c.EndReceive(ar);
            string msg = Encoding.ASCII.GetString(buff, 0, count);

            //Console.WriteLine("Msg from client {0}: {1}", c.RemoteEndPoint, msg);

        }

        public void Dispose()
        {
            Listener?.Dispose();
            Request?.Dispose();
        }
    }
}
