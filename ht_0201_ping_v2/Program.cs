using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;

namespace ht_0201_ping_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "0201 ping 192.168.43.0/24";
            int count = 25;
            string ipBase = "192.168.43.";
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\ping.txt")) { File.CreateText(Directory.GetCurrentDirectory() + @"\ping.txt"); }
            else { File.WriteAllText(Directory.GetCurrentDirectory() + @"\ping.txt", ""); }
            while (count!=0)
            {
                count--;
                string ip = ipBase + count.ToString();
                Ping ping = new Ping();
                PingReply reply = ping.Send(ip);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine(ip+" is active");
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\ping.txt",$"{ip} is active\r\n");
                    
                }
                else
                {
                    Console.WriteLine(ip + " no information");
                    File.WriteAllText(Directory.GetCurrentDirectory() + @"\ping.txt", $"{ip}  no information\r\n");
                    
                }
            }

            Console.ReadKey();
        }
    }
}
