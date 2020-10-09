using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace gl_001_n
{
    class Program
    {
        static void Main(string[] args)
        {

            //IPAddress ip = IPAddress.Parse("207.46.197.32"); -bad
            IPAddress ip = Dns.GetHostAddresses("www.microcoft.com")[0];
            IPEndPoint ep = new IPEndPoint(ip, 80);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            StringBuilder stringBuilder = new StringBuilder();

            try
            {

                Console.WriteLine(new string('-',30)+">connect");
                socket.Connect(ep);

                if (socket.Connected)
                {
                    Console.WriteLine(new string('-', 30) + "> GET");
                    string strSend = "GET\r\n\r\n";
                    socket.Send(Encoding.ASCII.GetBytes(strSend));

                    byte[] buffer = new byte[1024];
                    int l;
                    do
                    {
                        l = socket.Receive(buffer);
                        stringBuilder.Append(Encoding.ASCII.GetString(buffer, 0, l));

                    } while (l > 0);

                    Console.WriteLine(stringBuilder);
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            finally 
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            Console.WriteLine(new string('-', 30) + "> END");
            Console.ReadKey();
        }
    }
}
