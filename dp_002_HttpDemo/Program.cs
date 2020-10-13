using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dp_002_HttpDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("" +
                           "itstep.dp.ua",80);
            using (NetworkStream stream = client.GetStream())
            {
                StreamReader reader  = new StreamReader(stream);
                StreamWriter writer  = new StreamWriter(stream);

                var request = "GET / HTTP/1.1\r\n" +
                              "Host: itstep.dp.ua\r\n" +
                              "Connection: close\r\n"+
                              "\r\n";

                writer.Write(request);
                writer.Flush();

                var response = reader.ReadToEnd();
                Console.WriteLine(response);

            }

            Console.ReadKey();
        }
    }
}
