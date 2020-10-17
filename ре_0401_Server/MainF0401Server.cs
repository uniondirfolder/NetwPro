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
 
        public bool run = true;

        public MainF0401Server()
        {
            InitializeComponent();
        }


        private void btn_Start_Click(object sender, EventArgs e)
        {
            lb_Monitor.Items.Add("Start...");
           
            Run();
        }
        private void btn_Stop_Click(object sender, EventArgs e)
        {
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


        #region AsyncListener
        public class ObjectState
        {
            public Socket WSocket = null;
            public const int buffSize = 1024;
            public byte[] buff = new byte[buffSize];
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
                        listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                        allCompleted.WaitOne();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }

            }


            private static void AcceptCallback(IAsyncResult ar)
            {
                allCompleted.Set();
                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);

                ObjectState state = new ObjectState();
                state.WSocket = handler;
                handler.BeginReceive(state.buff, 0, ObjectState.buffSize, 0, new AsyncCallback(ReadCallback), state);

            }

            private static void ReadCallback(IAsyncResult ar)
            {
                string content = string.Empty;
                ObjectState state = (ObjectState)ar.AsyncState;
                Socket handler = state.WSocket;
                int byteRead = handler.EndReceive(ar);
                if (byteRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buff, 0, byteRead));
                    content = state.sb.ToString();
                    if (content.Contains("<EOF>"))
                    {
                        //content.IndexOf(@"<EOF>", StringComparison.CurrentCulture) > -1
                        frm.UpdateMon($"Read: {content.Length} bytes from  socket Data: {content}");
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
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }

            private static void SendCallback(IAsyncResult ar)
            {
                try
                {
                    Socket handler = (Socket)ar.AsyncState;
                    int byteSent = handler.EndSend(ar);
                    frm.UpdateMon($"Sent:  {byteSent} to client");
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

    }



}
