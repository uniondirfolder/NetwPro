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

namespace ht_0102_TimeClient
{
    public partial class MainF : Form
    {
        const int port = 10_000;
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
        EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        byte[] buff = new byte[255];
        private int seconds = 2;
        
        public MainF()
        {
            InitializeComponent();
            socket.Bind(ep);
            timer.Enabled = true;
            timer.Start();
        }

        private void MainF_Load(object sender, EventArgs e)
        {
            
        }

        private void MainF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (socket != null)
            {
                if (socket.Connected) { socket.Shutdown(SocketShutdown.Both);}
                socket.Close();
            }
        }

        void RunClient()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    int count = socket.ReceiveFrom(buff, ref remoteEP);
                    string msg = Encoding.UTF8.GetString(buff, 0, count);
                    Invoke(new Action(() => { lbl_time.Text = msg; }));
                }
            });
            
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            while (seconds!=0)
            {
                seconds--;
            }
            timer.Stop();
            RunClient();
        }

    }
}
