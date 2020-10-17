using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;
//using pe_0401_Client;
//using ре_0401_Server;


//namespace MyLibrary
//{
//    public class ObjectState
//    {
//        public const int BuffSize = 1024;
//        public Socket WSocket = null;
//        public byte[] Buffer = new byte[BuffSize];
//        public StringBuilder sb = new StringBuilder();
//    }

//    public class AsyncSocketListener
//    {
//        public static ManualResetEvent allCompleted = new ManualResetEvent(false);
//        private static MainF0401Server frm;

//        public static void StartListener(MainF0401Server _frm)
//        {
//            frm = _frm;

//            byte[] bytes = new byte[1024];

//            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
//            IPAddress ip = ipHost.AddressList[5];
//            IPEndPoint localEndPoint = new IPEndPoint(ip, 10000);
//            for (int i = 0; i < ipHost.AddressList.Length; i++)
//            {
//                frm.UpdateMon($"{ipHost.AddressList[i].MapToIPv4()}-{i} ");
//            }

//            Socket listener = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
//            frm.UpdateLblIp(localEndPoint.Address.MapToIPv4().ToString());
//            frm.UpdateLblPort(localEndPoint.Port.ToString());
//            try
//            {
//                listener.Bind(localEndPoint);
//                listener.Listen(100);

//                while (frm.run)
//                {
//                    allCompleted.Reset();
//                    frm.UpdateMon("Waiting for incoming connection...");
//                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
//                    allCompleted.WaitOne();
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }

//        }

//        private static void AcceptCallback(IAsyncResult ar)
//        {
//            allCompleted.Set();
//            Socket listener = (Socket) ar.AsyncState;
//            Socket handler = listener.EndAccept(ar);

//            ObjectState state = new ObjectState();
//            state.WSocket = handler;
//            handler.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReadCallback), state);

//        }

//        private static void ReadCallback(IAsyncResult ar)
//        {
//            string content = string.Empty;
//            ObjectState state = (ObjectState) ar.AsyncState;
//            Socket handler = state.WSocket;
//            int byteRead = handler.EndReceive(ar);
//            if (byteRead > 0)
//            {
//                state.sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, byteRead));
//                if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
//                {
//                    frm.UpdateMon($"Read: {content.Length} bytes from  socket Data: {content}");
//                    Send(handler, content);
//                }
//                else
//                {
//                    handler.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReadCallback),
//                        state);
//                }
//            }
//        }

//        private static void Send(Socket handler, string content)
//        {
//            byte[] byteData = Encoding.ASCII.GetBytes(content);
//            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
//        }

//        private static void SendCallback(IAsyncResult ar)
//        {
//            try
//            {
//                Socket handler = (Socket) ar.AsyncState;
//                int byteSent = handler.EndSend(ar);
//                frm.UpdateMon($"Sent:  {byteSent} to client");
//                handler.Shutdown(SocketShutdown.Both);
//                handler.Close();
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }
//    }


//    public class AsyncSocketClient
//    {
//        private const int Port = 10_000;
//        private static ManualResetEvent connectCompleted = new ManualResetEvent(false);
//        private static ManualResetEvent sendCompleted = new ManualResetEvent(false);
//        private static ManualResetEvent receivedCompleted = new ManualResetEvent(false);
//        private static string response = string.Empty;
//        private static MainF0401Client frm;

//        public static void StartClient(MainF0401Client _frm)
//        {
//            frm = _frm;
//            try
//            {
//                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
//                IPAddress ip = ipHost.AddressList[5];
//                IPEndPoint removeEndPoint = new IPEndPoint(ip, Port);

//                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
//                client.BeginConnect(removeEndPoint, new AsyncCallback(ConnectionCallback), client);
//                Send(client, $"This is socket message {DateTime.UtcNow:D} <EOF>");
//                sendCompleted.WaitOne();

//                Receive(client);
//                receivedCompleted.WaitOne();
//                frm.UpdateMon($"Response received {response}");
//                client.Shutdown(SocketShutdown.Both);
//                client.Close();
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }
//        }

//        private static void Receive(Socket client)
//        {
//            try
//            {
//                ObjectState state = new ObjectState();
//                state.WSocket = client;
//                client.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReceivedCallback),
//                    state);
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }
//        }

//        private static void ReceivedCallback(IAsyncResult ar)
//        {
//            try
//            {
//                ObjectState state = (ObjectState) ar.AsyncState;
//                var client = state.WSocket;
//                int byteRead = client.EndReceive(ar);
//                if (byteRead > 0)
//                {
//                    state.sb.Append(Encoding.ASCII.GetString(state.Buffer, 0, byteRead));
//                    client.BeginReceive(state.Buffer, 0, ObjectState.BuffSize, 0, new AsyncCallback(ReceivedCallback),
//                        state);
//                }
//                else
//                {
//                    if (state.sb.Length > 1)
//                    {
//                        response = state.sb.ToString();
//                    }

//                    receivedCompleted.Set();
//                }
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }
//        }

//        private static void Send(Socket client, string data)
//        {
//            byte[] byteData = Encoding.ASCII.GetBytes(data);
//            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
//        }

//        private static void SendCallback(IAsyncResult ar)
//        {
//            try
//            {
//                Socket client = (Socket) ar.AsyncState;
//                int byteSent = client.EndSend(ar);
//                frm.UpdateMon($"Sent: {byteSent} bytes to server");
//                sendCompleted.Set();
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }
//        }

//        private static void ConnectionCallback(IAsyncResult ar)
//        {
//            try
//            {
//                Socket client = (Socket) ar.AsyncState;
//                client.EndConnect(ar);
//                frm.UpdateMon($"Socket connection : {client.RemoteEndPoint}");
//                connectCompleted.Set();
//            }
//            catch (Exception e)
//            {
//                MessageBox.Show(e.Message);
//            }
//        }
//    }

//}
