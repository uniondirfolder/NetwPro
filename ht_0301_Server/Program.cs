using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ht_0301_Server
{
    class Program
    {
        static List<Socket> clients = new List<Socket>();
        static Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Loopback;
            int port = 10_000;
            socketServer.Bind(new IPEndPoint(address, port));
            socketServer.Listen(1);

            Console.WriteLine("Server start work >");

            while (true)
            {
                Socket client = socketServer.Accept();
                if (!clients.Contains(client))
                {
                    clients.Add(client);
                    Task.Run(() =>
                    {
                        while (true)
                        {
                            try
                            {
                                byte[] buff = ReceiveAll(client);
                                string message = Encoding.UTF8.GetString(buff);
                                if (!String.IsNullOrWhiteSpace(message))
                                {
                                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()} {message}");
                                    SendMessageAllClient($"{message}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                if (!client.Connected && clients.Contains(client))
                                    clients.Remove(client);
                                break;
                            }
                        }
                    });
                }
            }
            Console.ReadKey();
        }

        private static void SendMessageAllClient(string msg)
        {

            if (socketServer != null)
            {
                try
                {
                    var listRemove = new List<Socket>();
                    foreach (var client in clients)
                    {
                        try
                        {
                            client.Send(Encoding.UTF8.GetBytes(msg));
                        }
                        catch
                        {
                            listRemove.Add(client);
                        }
                    }
                    foreach (var client in listRemove)
                    {
                        clients.Remove(client);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static byte[] ReceiveAll(Socket client)
        {
            var buffer = new List<byte>();

            while (client.Available > 0)
            {
                var currByte = new Byte[1];
                var byteCounter = client.Receive(currByte, currByte.Length, SocketFlags.None);

                if (byteCounter.Equals(1))
                {
                    buffer.Add(currByte[0]);
                }
            }

            return buffer.ToArray();
        }
    }
}
