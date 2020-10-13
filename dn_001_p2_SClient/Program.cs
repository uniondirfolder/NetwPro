using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dn_001_p2_SClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //string host = "itstep.dp.ua";
            //IPAddress ip = Dns.GetHostAddresses(host).First();
            //Console.WriteLine(ip);
            //IPEndPoint ep = new IPEndPoint(ip, 80);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),10000);
            

            Socket s = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.IP);

            try
            {
                s.Connect(ep);
                if (s.Connected)
                {
                    Console.WriteLine("Connected");

                    //Request page
                    //string msg = string.Format("GET / HTTP/1.1\r\nHost: {0}\r\n\r\n", host);
                    Console.WriteLine("Type message: ");
                    string msg = Console.ReadLine();
                    byte[] buff = Encoding.ASCII.GetBytes(msg);
                    s.Send(buff);

                    //Get answer
                    int count = 0;
                    buff = new byte[1024];
                    do
                    {
                        count = s.Receive(buff);
                        Console.WriteLine(Encoding.UTF8.GetString(buff, 0, count));

                    } while (count > 0);

                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                if (s.Connected)
                {
                    s.Shutdown(SocketShutdown.Both);
                }

                s.Close();
            }


            Console.ReadKey();
        }
    }
}
