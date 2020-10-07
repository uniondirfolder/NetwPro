using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ht_0201_ping
{
    class Program
    {
        static CountdownEvent _countdown;
        static int _upCount;
        static object _lockObj = new object();
        const bool _resolveNames = true;
        static void Main(string[] args)
        {
            _countdown = new CountdownEvent(1);
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string ipBase = "192.168.115.";
            for (int i = 100; i < 124; i++)
            {
                string ip = ipBase + i.ToString();

                Ping p = new Ping();
                p.PingCompleted += new PingCompletedEventHandler(p_PingCompleted);
                _countdown.AddCount();
                p.SendAsync(ip, 100, ip);
            }
            _countdown.Signal();
            _countdown.Wait();
            sw.Stop();
            TimeSpan span = new TimeSpan(sw.ElapsedTicks);
            Console.WriteLine($"Took {sw.ElapsedMilliseconds} millisec. {_upCount} host active");

            Console.ReadLine();
        }

        private static void p_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            string ip = (string)e.UserState;
            if (e.Reply != null && e.Reply.Status == IPStatus.Success)
            {
                if (_resolveNames)
                {
                    string name;
                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(ip);
                        name = hostEntry.HostName;
                    }
                    catch (SocketException ex)
                    {

                        name = "?";
                    }
                    Console.WriteLine($"{ip} ({name}) is up: ({e.Reply.RoundtripTime}) ms");
                }
                else
                {
                    Console.WriteLine($"{ip} is up: ({e.Reply.RoundtripTime}) ms");
                }
                lock (_lockObj)
                {
                    _upCount++;
                }
            }
            else if (e.Reply == null) 
            {
                Console.WriteLine($"Pinging {ip} failed. (Null Reply object?)");
            }
            _countdown.Signal();
        }
    }
}
