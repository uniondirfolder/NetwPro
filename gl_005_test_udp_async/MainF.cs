using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gl_005_test_udp_async
{
    public partial class MainF : Form
    {
        delegate void AddTextDelegate(string text);
        private class StateObject 
        {
            public Socket workSocket = null;
            public const int BufferSize = 1024;
            public byte[] buffer = new byte[BufferSize];
        }
        StateObject state = new StateObject();
        Socket socket;
        IAsyncResult RcptRes, SendRes;

        EndPoint ClientEP = new IPEndPoint(IPAddress.Any, 53);
        public MainF()
        {
            InitializeComponent();
        }

        void AddText(string text) 
        {
            tb_main.Text += text;
        }
        private void btn_start_Click(object sender, EventArgs e)
        {
            if (socket != null) return;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            socket.Bind(new IPEndPoint(IPAddress.Parse("188.191.237.208"), 53));

            state.workSocket = socket;
            RcptRes = socket.BeginReceiveFrom(
                state.buffer,
                0,
                StateObject.BufferSize,
                SocketFlags.None,
                ref ClientEP,
                new AsyncCallback(Receive_Completed), state);
        }

        private void Receive_Completed(IAsyncResult ar)
        {
            try
            {
                StateObject so = (StateObject)ar.AsyncState;
                Socket client = so.workSocket;
                if (socket == null) return;
                int readed = client.EndReceiveFrom(RcptRes, ref ClientEP);
                string strClientIP = ((IPEndPoint)ClientEP).Address.ToString();
                string str = string.Format($"\nReceived from {strClientIP}\r\n{Encoding.Unicode.GetString(so.buffer, 0, readed)}");

                tb_main.BeginInvoke(new AddTextDelegate(AddText), str);

                RcptRes = socket.BeginReceiveFrom(
                    state.buffer,
                    0,
                    StateObject.BufferSize,
                    SocketFlags.None,
                    ref ClientEP,
                    new AsyncCallback(Receive_Completed),
                    state);
            }
            catch (SocketException ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            socket.Shutdown(SocketShutdown.Receive);
            socket.Close();
            socket = null;
            tb_main.Text = "";

            
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_main.Text = "";
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            byte[] buffer = Encoding.Unicode.GetBytes(tb_send.Text);
            SendRes = socket.BeginSendTo(
                buffer,
                0, 
                buffer.Count(),
                SocketFlags.None,
                (EndPoint)new IPEndPoint(IPAddress.Parse("188.191.237.208"), 53),
                new AsyncCallback(Send_Completed),
                socket);
        }

        private void Send_Completed(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(SendRes);
            socket.Shutdown(SocketShutdown.Send);
            socket.Close();
        }
    }
}
