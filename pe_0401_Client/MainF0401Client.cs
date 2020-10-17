using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace pe_0401_Client
{

    public partial class MainF0401Client : Form
    {
        private bool sendMsg = false;
        public MainF0401Client()
        {
            InitializeComponent();

            LockUnlockControls(false);
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            Run();
            LockUnlockControls(true);
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {

            LockUnlockControls(true);
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            sendMsg = true;
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
        private void UpdateMon(string str)
        {
            new Thread(
                () => { Invoke(new Action(() => { this.lb_Monitor.Items.Add(str); })); }).Start();
        }

        public Task<bool> MustSend()
        {
            return Task.Run(() =>
            {
                bool must=this.sendMsg;
                return must;

            });
        }

        public async Task<bool> IsSendMsg()
        {
            return await MustSend();
        } 
        //----------------------

        #region AsyncClient
        public class ObjectState
        {
            public const int BuffSize = 1024;
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

                    Socket client = null;


                    while (true)
                    {
                        if (frm.sendMsg==true)
                        {
                            client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                            client.BeginConnect(removeEndPoint, new AsyncCallback(ConnectionCallback), client);
                            Send(client, $"This is socket message {DateTime.UtcNow:D} <EOF> ");
                            sendCompleted.WaitOne();

                            Receive(client);
                            receivedCompleted.WaitOne();
                            frm.UpdateMon($"Response received {response}");
                            frm.sendMsg = false;
                            
                            
                        }
                    }
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();

                }
                catch (Exception e)
                {
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
                    MessageBox.Show(e.Message);
                }
            }

            private static void ReceivedCallback(IAsyncResult ar)
            {
                try
                {
                    ObjectState state = (ObjectState)ar.AsyncState;
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
                    Socket client = (Socket)ar.AsyncState;
                    int byteSent = client.EndSend(ar);
                    frm.UpdateMon($"Sent: {byteSent} bytes to server");
                    sendCompleted.Set();
                }
                catch (Exception e)
                {
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
                    MessageBox.Show(e.Message);
                }
            }
        }

        #endregion


    }
}
