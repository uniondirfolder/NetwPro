using dn_002_BlackjackLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace dn_002_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 10_000);
            tcpListener.Start();
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Task.Run(() =>
                {
                    using (NetworkStream stream = tcpClient.GetStream())
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Message request = (Message)formatter.Deserialize(stream);
                        Console.Write(request);
                        if (request.MessageType == MessageType.TakeCard )
                        {
                            var answer = new Message
                            {
                                MessageType = MessageType.GiveCard,
                                Cards = new[] { new Card { Suite = Suite.Clubs, Value = "A" } }
                            };
                            formatter.Serialize(stream, answer);
                        }
                    }
                });
            }
        }
    }
}
