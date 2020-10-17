using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ht_0401_LibraryNet;

namespace pe_0401_Client
{
    public partial class MainF0401Client : Form
    {
        public TcpSocket ClientTcp = null;
        public MainF0401Client()
        {
            InitializeComponent();
            LockUnlockControls(false);

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {

                if (ClientTcp == null)
                {
                    ClientTcp = new TcpSocket(IPAddress.Parse(tb_ip.Text), Int32.Parse(tb_port.Text));
                    ClientTcp.Start();
                }

                if (ClientTcp.Request == null)
                {
                    ClientTcp.MakeNewConnect();
                }

                if (ClientTcp.Request.Connected)
                {
                    LockUnlockControls(true);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            try
            {
                LockUnlockControls(false);
                ClientTcp.Request.Close();
                ClientTcp.Request = null;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            try
            {
                NetwMsg nm = new NetwMsg(ClientTcp.LocalEndPoint,tb_Send.Text);
                byte[] buff = Encoding.UTF8.GetBytes(nm.ToString());

                ClientTcp.Request.Send(buff);
                lb_Monitor.Items.Add(@"send > " + tb_Send.Text + " " + DateTime.Now.ToLongTimeString());
                string answer = string.Empty;

                byte[] back = new byte [ClientTcp.Request.ReceiveBufferSize];
                ClientTcp.Request.Receive(back);
                answer = Encoding.UTF8.GetString(back);
                if (!string.IsNullOrEmpty(answer))
                {
                    lb_Monitor.Items.Add(@"answer > " + answer + " " + DateTime.Now.ToLongTimeString());
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void LockUnlockControls(bool connect)
        {
            if (connect)
            {
                btn_Start.Enabled = false;
                btn_Stop.Enabled = true;
                gb_Connect.Enabled = false;
                gb_Send.Enabled = true;
                gb_Monitor.Enabled = true;
            }
            else
            {
                btn_Start.Enabled = true;
                btn_Stop.Enabled = false;
                gb_Connect.Enabled = true;
                gb_Send.Enabled = false;
                lb_Monitor.Items.Clear();
                gb_Monitor.Enabled = false;
            }
        }

        Task<string> GetAnswer()
        {
            return Task.Run(() =>
            {
                Socket s = ClientTcp.Listener.Accept();
                
                byte[] buff=new byte[s.ReceiveBufferSize];
                int count = s.Receive(buff);
                string get = Encoding.UTF8.GetString(buff, 0, count);
                NetwMsg nm = new NetwMsg(get);
               
                s.Close();
                
                return nm.ToString();
            });
        }
       
    }
}
