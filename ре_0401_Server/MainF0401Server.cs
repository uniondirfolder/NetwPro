using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ht_0401_LibraryNet;

namespace ре_0401_Server
{
    public partial class MainF0401Server : Form
    {
        public LocalMsg localMsg;
        private ServerTcp server;
        Thread thread;
        private bool checkMsg = true;

        public MainF0401Server()
        {
            InitializeComponent();
            localMsg = new LocalMsg();
        }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            btn_Start.Enabled = false;
            btn_Stop.Enabled = true;
            thread = new Thread(new ThreadStart(ServerStart));
            thread.Start();
            CheckMessage();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            server.Stop();
            thread.Abort();
            btn_Start.Enabled = true;
            btn_Stop.Enabled = false;
        }

        void ServerStart()
        {
            server = new ServerTcp(IPAddress.Parse(tb_ip.Text), int.Parse(tb_port.Text), localMsg);
            server.Start(lb_Monitor);
        }

        void PrintMonitor(string str)
        {
            lb_Monitor.Items.Add(str);
        }

        void CheckMessage()
        {

            Task.Run(() =>
            {
                while (checkMsg)
                {
                    Thread.Sleep(5000);
                    if (localMsg.haveMsg)
                    {
                        Invoke(new Action(() =>

                                this.lb_Monitor.Text = localMsg.msg
                            )
                        );
                    }
                }
            });

        }
    }
}
