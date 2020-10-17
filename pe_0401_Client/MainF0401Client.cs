using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private Client Id = new Client("client");
        private TcpClient client;
        private NetworkStream stream;
        public MainF0401Client()
        {
            InitializeComponent();

        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient();
                client.Connect(tb_ip.Text, int.Parse(tb_port.Text));
                stream = client.GetStream();

                btn_Start.Enabled = false;
                btn_Stop.Enabled = true;

                BinaryFormatter formatter = new BinaryFormatter();
                Msg request = new Msg
                {
                    Name = Id.Name,
                    Guid = Id.ClientGuid.ToString(),
                    MsgType = MsgType.CmdOther,
                    Body = "start connect"
                };
                formatter.Serialize(stream, request);
                Msg answer = (Msg)formatter.Deserialize(stream);
                lb_Monitor.Items.Add(answer.ToString());

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
                stream.Close();
                client.Close();
                btn_Start.Enabled = true;
                btn_Stop.Enabled = false;

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
               

                Msg request = new Msg
                {
                    Name = Id.Name,
                    Guid = Id.ClientGuid.ToString(),
                    MsgType = MsgType.CmdOther,
                    Body = tb_Send.Text
                };

                string sendData = string.Empty;
                stream = client.GetStream();
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(request.ToString());
                stream.Write(data, 0, data.Length);



                //BinaryFormatter formatter = new BinaryFormatter();

                //formatter.Serialize(stream, request);
                //Msg answer = (Msg) formatter.Deserialize(stream);

                data = new Byte[1024];

                // String to store the response ASCII representation.
                String responseData = String.Empty;
                if (stream.DataAvailable)
                {
                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);

                    lb_Monitor.Items.Add(responseData);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
