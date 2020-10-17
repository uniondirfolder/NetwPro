using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pe_0401_Client
{

    public class ObjectState
    {
        public const int BuffSize = 256;
        public Socket WSocket = null;
        public byte[] Buffer = new byte[BuffSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class AsyncSocketClient
    {
        private const int Port = 10_000;
        private static ManualResetEvent connectCompleted = new ManualResetEvent(false);
        private static ManualResetEvent sendCompleted = new ManualResetEvent(false);
        private static ManualResetEvent receivedCompleted = new ManualResetEvent(false);
        private static string response = string.Empty;
        private static MainF0401Client frm;
        public static void StartClient(MainF0401Client _frm)
        {
            frm = _frm;
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ip = ipHost.AddressList[5];
                IPEndPoint removeEndPoint = new IPEndPoint(ip, Port);

                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                client.BeginConnect(removeEndPoint,new AsyncCallback(ConnectionCallback), client);
                Send(client, $"This is socket message {DateTime.UtcNow:D} <EOF>");
                sendCompleted.WaitOne();

                Receive(client);
                receivedCompleted.WaitOne();
                frm.UpdateMon($"Response received {response}");
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                ObjectState state = new ObjectState();
                state.WSocket = client;
                client.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReceivedCallback),
                    state);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }

        private static void ReceivedCallback(IAsyncResult ar)
        {
            try
            {
                ObjectState state = (ObjectState) ar.AsyncState;
                var client = state.WSocket;
                int byteRead = client.EndReceive(ar);
                if (byteRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, byteRead));
                    client.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReceivedCallback),
                        state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }

                    receivedCompleted.Set();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }

        private static void Send(Socket client, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;
                int byteSent = client.EndSend(ar);
                frm.UpdateMon($"Sent: {byteSent} bytes to server");
                sendCompleted.Set();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }

        private static void ConnectionCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                frm.UpdateMon($"Socket connection : {client.RemoteEndPoint}");
                connectCompleted.Set();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }
    }


    public partial class MainF0401Client : Form
    {
        //private TcpClient tcpClient;
        public MainF0401Client()
        {
            InitializeComponent();
            //tcpClient = new TcpClient();
            LockUnlockControls(false);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Run();
            {
                //try
                //{
                //    tcpClient.Connect(IPAddress.Parse(tb_ip.Text), int.Parse(tb_port.Text));
                //    lb_Monitor.Items.Add(tcpClient.Connected ? "Connected!" : "Connected - False!");
                //    LockUnlockControls(true);
                //}
                //catch (Exception exception)
                //{
                //    MessageBox.Show(exception.Message);
                //}
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //tcpClient?.Close();
            LockUnlockControls(true);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            {
            //    //code not for WinForms-> :(
            //    //https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=netcore-3.1
            //    try
            //    {
            //        // Create a TcpClient.
            //        // Note, for this client to work you need to have a TcpServer
            //        // connected to the same address as specified by the server, port
            //        // combination.
            //        Int32 port = Int32.Parse(tb_port.Text);
            //        TcpClient client = new TcpClient(tb_ip.Text, port);

            //        // Translate the passed message into ASCII and store it as a Byte array.
            //        Byte[] data = System.Text.Encoding.UTF8.GetBytes(tb_Send.Text);

            //        // Get a client stream for reading and writing.
            //        //  Stream stream = client.GetStream();

            //        NetworkStream stream = client.GetStream();

            //        // Send the message to the connected TcpServer.
            //        stream.Write(data, 0, data.Length);

            //        lb_Monitor.Items.Add("Sent: " + tb_Send.Text);
            //        //Console.WriteLine("Sent: {0}", message);

            //        // Receive the TcpServer.response.

            //        // Buffer to store the response bytes.
            //        data = new Byte[256];

            //        // String to store the response ASCII representation.
            //        String responseData = String.Empty;

            //        // Read the first batch of the TcpServer response bytes.
            //        Int32 bytes = stream.Read(data, 0, data.Length); // <- this operation deadlock- trubs with reading...???

            //        responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            //        lb_Monitor.Items.Add("Received: " + responseData);
            //        //Console.WriteLine("Received: {0}", responseData);

            //        // Close everything.
            //        stream.Close();
            //        //client.Close();
            //    }
            //    catch (ArgumentNullException ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        //Console.WriteLine("ArgumentNullException: {0}", e);
            //    }
            //    catch (SocketException ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        //Console.WriteLine("SocketException: {0}", e);
            //    }
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
        Task RunClient()
        {
            return Task.Run(() =>
            {
                AsyncSocketClient.StartClient(this);

            });
        }

        private async void Run()
        {
            await RunClient();
        }
        public void UpdateMon(string str)
        {
            new Thread(
                () => { Invoke(new Action(() => { this.lb_Monitor.Items.Add(str); })); }).Start();
        }
    }
}
