using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace ре_0401_Server
{
    public partial class MainF0401Server : Form
    {
        public class ObjectState
        {
            public Socket WSocket = null;
            public const int buffSize = 1024;
            public byte[]buff = new byte[buffSize];
            public StringBuilder sb = new StringBuilder();
        }
        public class AsyncSocketListener
        {
            public static ManualResetEvent allCompleted = new ManualResetEvent(false);
            private static MainF0401Server frm;
            public static void StartListener(MainF0401Server _frm)
            {
                frm = _frm;

                byte[] bytes = new byte[1024];

                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ip = ipHost.AddressList[5];
                IPEndPoint localEndPoint = new IPEndPoint(ip, 10000);
                for (int i = 0; i < ipHost.AddressList.Length; i++)
                {
                    frm.UpdateMon($"{ipHost.AddressList[i].MapToIPv4()}-{i} ");
                }
                Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                frm.UpdateLblIp(localEndPoint.Address.MapToIPv4().ToString());
                frm.UpdateLblPort(localEndPoint.Port.ToString());
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(100);

                    while (frm.run)
                    {
                        allCompleted.Reset();
                        frm.UpdateMon("Waiting for incoming connection...");
                        //frm.lb_Monitor.Items.Add("Waiting for incoming connection...");
                        //Debug.WriteLine($"Waiting for incoming connection...");
                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        allCompleted.WaitOne();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                }
                
            }
            

            private static void AcceptCallback(IAsyncResult ar)
            {
                allCompleted.Set();
                Socket listener = (Socket) ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                ObjectState state = new ObjectState();
                state.WSocket = handler;
                handler.BeginReceive(state.buff, 0, ObjectState.buffSize,0, new AsyncCallback(ReadCallback), state);

            }

            private static void ReadCallback(IAsyncResult ar)
            {
                string content = string.Empty;
                ObjectState state = (ObjectState) ar.AsyncState;
                Socket handler = state.WSocket;
                int byteRead = handler.EndReceive(ar);
                if (byteRead>0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buff, 0, byteRead));
                    if (content.IndexOf("<EOF>",StringComparison.Ordinal)>-1)
                    {
                        frm.UpdateMon($"Read: {content.Length} bytes from  socket Data: {content}");

                        //frm.lb_Monitor.Items.Add($"Read: {content.Length} bytes from \n socket Data: {content}");
                        //Debug.WriteLine($"Read: {content.Length} bytes from \n socket Data: {content}");
                        Send(handler, content);
                    }
                    else
                    {
                        handler.BeginReceive(state.buff, 0, ObjectState.buffSize, 0, new AsyncCallback(ReadCallback), state);
                    }
                }
            }

            private static void Send(Socket handler, string content)
            {
                byte[] byteData = Encoding.ASCII.GetBytes(content);
                handler.BeginSend(byteData,0,byteData.Length,0, new AsyncCallback(SendCallback),handler);
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket handler = (Socket) ar.AsyncState;
                    int byteSent = handler.EndSend(ar);
                    frm.lb_Monitor.Items.Add($"Sent:  {byteSent} to client");
                    //Debug.WriteLine($"Sent:  {byteSent} to client");

                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        //TcpListener _server = null;
        public bool run = true;
        public MainF0401Server()
        {
            InitializeComponent();
        }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            lb_Monitor.Items.Add("Start...");
            {
                //try
                //{
                //    // Set the TcpListener on port 13000.
                //    Int32 port = int.Parse(tb_port.Text);
                //    IPAddress localAddr = IPAddress.Parse(tb_ip.Text);

                //    // TcpListener server = new TcpListener(port);
                //    _server = new TcpListener(localAddr, port);

                //    // Start listening for client requests.
                //    _server.Start();

                //    // Buffer for reading data
                //    Byte[] bytes = new Byte[256];
                //    String data = null;

                //    // Enter the listening loop.
                //    while (true)
                //    {
                //        lb_Monitor.Items.Add("Waiting for a connection... ");
                //        //Console.Write("Waiting for a connection... ");

                //        // Perform a blocking call to accept requests.
                //        // You could also use server.AcceptSocket() here.
                //        TcpClient client = _server.AcceptTcpClient();
                //        lb_Monitor.Items.Add("Connected!");

                //        //Console.WriteLine("Connected!");

                //        data = null;

                //        // Get a stream object for reading and writing
                //        NetworkStream stream = client.GetStream();

                //        if (stream.CanRead)
                //        {
                //            int i = stream.Read(bytes, 0, bytes.Length);

                //            // Loop to receive all the data sent by the client.
                //            while (i != 0)
                //            {
                //                // Translate data bytes to a ASCII string.
                //                data = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                //                lb_Monitor.Items.Add("Received: " + data);
                //                //Console.WriteLine("Received: {0}", data);

                //                // Process the data sent by the client.
                //                //data = data.ToUpper();

                //                byte[] msg = System.Text.Encoding.UTF8.GetBytes(data);

                //                // Send back a response.
                //                stream.Write(msg, 0, msg.Length);
                //                lb_Monitor.Items.Add("Sent: " + data);
                //                //Console.WriteLine("Sent: {0}", data);
                //                i = stream.Read(bytes, 0, bytes.Length);
                //            }
                //        }

                //        // Shutdown and end connection
                //        client.Close();
                //    }
                //}
                //catch (SocketException ex)
                //{
                //    MessageBox.Show(ex.Message);
                //    //Console.WriteLine("SocketException: {0}", e);
                //}
                //finally
                //{
                //    // Stop listening for new clients.
                //    _server.Stop();
                //}
            }
            //AsyncSocketListener.StartListener(this);
            Run();
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //_server.Stop();
            try
            {
               run = false;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        Task RunServer()
        {
            return Task.Run(() =>
            {
                AsyncSocketListener.StartListener(this);
                
            });
        }

        private async void Run()
        {
            await RunServer();
        }

        private void UpdateMon(string str)
        {
            new Thread(
                () => { Invoke(new Action(() => { this.lb_Monitor.Items.Add(str); })); }).Start();
        }
        private void UpdateLblIp(string str)
        {
            new Thread(
                () => { Invoke(new Action(() => { this.lbl_ip_cond.Text=str; })); }).Start();
        }
        private void UpdateLblPort(string str)
        {
            new Thread(
                () => { Invoke(new Action(() => { this.lbl_port_cond.Text = str; })); }).Start();
        }
    }



}
