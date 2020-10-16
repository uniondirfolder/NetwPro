using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ht_LibraryNetPro
{
    public enum FormatEncoding
    {
        ASCII, UTF8   
    }
    
    public class Msg
    {
        private Socket _socket = null;
        public int SizeBuff { get; set; } = 1_024;
        public void ChangeSizeBuff(int size)
        {
            if (size < 1 || size > 65000)
            {
                SizeBuff = 1_024;
            }

            SizeBuff = size;
            _buff = new byte[SizeBuff];
        }
        private byte[] _buff;

        public string MsgStrUdp(ref EndPoint remoteEndPoint, Socket socket, FormatEncoding formatEncoding)
        {
            string output = "";
            if (socket != null && remoteEndPoint != null)
            {
                this._socket = socket;
                int count = _socket.ReceiveFrom(_buff, ref remoteEndPoint);
                switch (formatEncoding)
                {
                    case FormatEncoding.UTF8:
                        output = Encoding.UTF8.GetString(_buff, 0, count);
                        break;

                    default:
                        break;
                }
            }

            return output;
        }

        public byte[] ByteStrUdp;
        public Msg()
        {
            _buff= new byte[SizeBuff];
        }
    }
    public class UdpServer
    {
        public int Port { get; set; }
        public int Sleep { get; set; }
        public IPEndPoint EndPoint { get; set; }
        public Socket Socket { get; set; }
        public byte[] PkgBytes { get; set; }
        public FormatEncoding FormatEncoding { get; set; }
        public bool _work = false;
        public bool _wait = true;
        private int _seconds = -1;

        public UdpServer()
        {
            this.Port = 10_000;
            this.Sleep = 1_000;
            this.EndPoint = new IPEndPoint(IPAddress.Loopback, Port);
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.PkgBytes = new byte[1024];
            this.FormatEncoding = FormatEncoding.UTF8;
        }
        public UdpServer(IPAddress ipAddress, int port, int sleep, int sizeBuff, FormatEncoding formatEncoding)
        {
            this.Port = port;
            this.Sleep = sleep;
            this.EndPoint = new IPEndPoint(ipAddress, Port);
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.PkgBytes = new byte[sizeBuff];
            this.FormatEncoding = formatEncoding;
        }

        public bool IsWorking()
        {
            return _work;
        }

        public bool IsWaiting()
        {
            return _wait;
        }

        public void Stop()
        {
            _work = false;
        }

        public void Wait(int seconds)
        {
            this._seconds = seconds;
            _wait = true;

        }

        public void Start()
        {
            while (_work)
            {
                while (_wait)
                {
                }
            
                Thread.Sleep(Sleep);

            }

            if (Socket != null)
            {
                if (Socket.Connected)
                {
                    Socket.Shutdown(SocketShutdown.Both);
                }
                Socket.Close();
                Socket = null;
            }

        }
    }
}
