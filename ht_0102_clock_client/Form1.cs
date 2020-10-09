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

namespace ht_0102_clock_client
{
    public partial class Form1 : Form
    {
        Socket client = null;
        
        public Form1()
        {
            InitializeComponent();

            btn_GetDate.Enabled = btn_GetTime.Enabled = false;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (btn_Connect.Text == @"Connect" && client == null)
            {

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                try
                {
                    IPEndPoint ep;
                    int port = Convert.ToInt32(txb_Port.Text);
                    ep = txb_IP.Text==@"default" ? new IPEndPoint(IPAddress.Loopback, port) : new IPEndPoint(IPAddress.Parse(txb_IP.Text), port);
                    client.Connect(ep);
                    if (client.Connected)
                    {
                        btn_Connect.Text = @"Disconnect";
                        txb_Port.Enabled = txb_IP.Enabled = false;
                        btn_GetDate.Enabled = btn_GetTime.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show(@"Неможливо створити за таких параметрів з'язок!");
                    }

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Disconnect();
                }
            }
            else if (btn_Connect.Text == @"Disconnect")
            {
                Disconnect();
            }
        }

        private void btn_GetTime_Click(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                client.Send(Encoding.ASCII.GetBytes("time"));

                int size = client.ReceiveBufferSize;
                byte[] get = new byte[size];
                EndPoint ep = client.RemoteEndPoint;

                client.ReceiveFrom(get, SocketFlags.None, ref ep);

                txb_Time.Text = Encoding.UTF8.GetString(get);

                Disconnect();
            }
            
            
        }

        private void btn_GetDate_Click(object sender, EventArgs e)
        {
            client.Send(Encoding.ASCII.GetBytes("date"));

            int size = client.ReceiveBufferSize;
            byte[] get = new byte[size];
            EndPoint ep = client.RemoteEndPoint;

            client.ReceiveFrom(get, SocketFlags.None, ref ep);

            txb_Date.Text = Encoding.UTF8.GetString(get);

            Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           Disconnect();
        }

        private void Disconnect()
        {
            if (client != null && client.Connected)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }

            client = null;
            btn_GetDate.Enabled = btn_GetTime.Enabled = false;
            btn_Connect.Text = @"Connect";
            txb_Port.Enabled = txb_IP.Enabled = true;
        }
    }
}
