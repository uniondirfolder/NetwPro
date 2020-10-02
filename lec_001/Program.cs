using System;
using System.Net;
using System.Net.NetworkInformation;

namespace lec_001
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ping");
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply pingReply = ping.Send("127.0.0.1", 100);
                    if (pingReply.Status==IPStatus.Success)
                    {
                        Console.WriteLine($"{pingReply.Address} {pingReply.RoundtripTime}");
                    }
                    else
                    {
                        Console.WriteLine(pingReply.Status);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }


            Console.ReadKey();
        }
    }
}
