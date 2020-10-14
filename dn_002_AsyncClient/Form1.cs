using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dn_002_AsyncClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(textBox1.Text, 10_000);
            Text = "Connected to " + textBox1.Text;
            using (Stream stream = client.GetStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string html = await reader.ReadToEndAsync();
                    textBox2.Text = html;
                }
                
            }
        }
    }

   
}
