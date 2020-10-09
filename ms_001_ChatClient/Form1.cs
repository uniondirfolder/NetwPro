using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ms_001_ChatClient
{
    public partial class Form1 : Form
    {
        TcpClient clienSocket = new TcpClient();
        private NetworkStream serverStream = default(NetworkStream);
        private string readData = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void txb_SendMsg_TextChanged(object sender, EventArgs e)
        {
            if (btn_SendMsg != null) btn_SendMsg.Enabled = txb_SendMsg.Text.Length > 0;
        }

        private void txb_ChatName_TextChanged(object sender, EventArgs e)
        {
            if (btn_ConnectToServer != null) btn_ConnectToServer.Enabled = txb_ChatName.Text.Length > 0;
        }

        private void btn_ConnectToServer_Click(object sender, EventArgs e)
        {
            readData = "Connect to Chat Server ...";
            Msg();
            clienSocket.Connect("127.0.0.1", 8888);
            serverStream = clienSocket.GetStream();

            byte[] outStream = Encoding.ASCII.GetBytes(txb_SendMsg.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            Thread cThread = new Thread(GetMessage);
            
        }

        private void Msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(Msg));
            }
            else
            {
                if (txb_ChatMonitor != null)
                    txb_ChatMonitor.Text = txb_ChatMonitor.Text + Environment.NewLine + " >> " + readData;
            }
        }
        private void btn_SendMsg_Click(object sender, EventArgs e)
        {
            txb_ChatMonitor.Text += txb_SendMsg.Text + "\r";
            byte[] outStream = Encoding.ASCII.GetBytes(txb_ChatName.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

        }

        private void GetMessage()
        {
            while (true)
            {
                serverStream = clienSocket.GetStream();
                int buffSize = clienSocket.ReceiveBufferSize;
                byte[] inStream = new byte[buffSize];
                serverStream.Read(inStream, 0, buffSize);
                string returnData = Encoding.ASCII.GetString(inStream);
                readData = "" + returnData;
                Msg();
            }
        }
    }
}
