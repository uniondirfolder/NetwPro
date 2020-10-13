using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using dn_002_BlackjackLib;

namespace dp_002_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 10_000);
            Message request = new Message {MessageType = MessageType.TakeCard};
            using (NetworkStream stream = client.GetStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream,request);
                Message answer = (Message)formatter.Deserialize(stream);
                Console.Write(answer);
            }

            Console.ReadKey();
        }
    }
}
