using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dn_003_WinClient
{
    public partial class Form1 : Form
    {
        UdpClient client = new UdpClient();
        private const string msg = "{0} : {1}";

        public Form1()
        {
            InitializeComponent();
            Listener();
        }

        private void Listener()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.SendBroadcast(String.Format(msg,"max,", textBox1.Text));
        }
    }
}
