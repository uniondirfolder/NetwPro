using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gl_005_test_udp
{
    public partial class MainF : Form
    {
        delegate void AddTextDelegate(string text);
        Thread thread;
        Socket socket;
        public MainF()
        {
            InitializeComponent();
        }

        void AddText(string text) 
        {
            tb_main.Text += text;
        }

        void RecivFunction(object obj) 
        {
            Socket rs = (Socket)obj;
            byte[] buffer = new byte[1024];
            do
            {
                EndPoint ep = new IPEndPoint(0x7F000000, 100);

                int l = rs.ReceiveFrom(buffer, ref ep);

                string strClientIP = ((IPEndPoint)ep).Address.ToString();
                //string strGetSend = Encoding.ASCII.GetString(buffer, 0, l);
                string str = string.Format("\nReceived from {0}\r\n{1}\r\n",
                    strClientIP, Encoding.Unicode.GetString(buffer, 0, l));

                tb_main.BeginInvoke(new AddTextDelegate(AddText), str);

            } while (true);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (socket != null && thread != null) 
            {
                return;
            }
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100));

            thread = new Thread(RecivFunction);
            thread.Start(socket);
            tb_main.Text += "start thread";
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (socket != null) 
            {
                thread.Abort();
                thread = null;
                socket.Shutdown(SocketShutdown.Receive);
                socket.Close();
                socket = null;
                tb_main.Text = "";
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            tb_main.Text = "";
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
            byte[] send = Encoding.Unicode.GetBytes(tb_send.Text);
            socket.SendTo(send, new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100));
            socket.Shutdown(SocketShutdown.Send);
            socket.Close();
        }
    }
}
