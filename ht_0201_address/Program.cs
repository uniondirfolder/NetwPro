using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ht_0201_address
{
    class Program
    {
        #region Values
        static Socket _socket = null;
        static IPEndPoint _endPoint = null;
        static IPAddress _ipAddress = null;
        static string _userString = "";
        static bool runProg = true;
        #endregion

        

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "hw 2010";
            Console.Clear();
            PrintHelp();

            while (runProg)
            {
                if (_userString == "exit") 
                {
                    if (_socket != null) 
                    {
                        CloseCurCon();
                    }
                    runProg = false;
                }             
                if (_userString == "build") { Build(); }
                if (_userString == "restart") { if (_socket != null) { CloseCurCon(); }; Build(); }
                if (_userString == "help") { PrintHelp(); }

                Console.Write("Adm > "); _userString = Console.ReadLine();
            }

            if (_socket != null) { CloseCurCon(); }
        }

        static void PrintHelp() 
        {
            Console.Clear();
            Console.WriteLine("Help > commands:");
            Console.WriteLine("exit\nrestart (close current conn  and build new conn)\nhelp\nbuild ( new conn)\n");
        }
        static void Build() 
        {
            if (_socket != null) { Console.WriteLine("Current connection is active! You must restart connection!"); return; }

            Console.Write("Put IP (xxx.xxx.xxx.xxx) > "); _userString = Console.ReadLine();

            if (IPAddress.TryParse(_userString, out _ipAddress))
            {
                try
                {
                    _ipAddress = _ipAddress.MapToIPv4();
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);
                    _endPoint = new IPEndPoint(_ipAddress, 80);
                    _socket.Bind(_endPoint);
                    Console.WriteLine($"Created EndPoint: address {_endPoint.Address} port:{_endPoint.Port} ");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(new string ('-',25));
                    Console.WriteLine($"Error -> {ex} / Only IP address use A class");
                    Console.WriteLine(new string('-', 25));
                    if (_socket != null) { CloseCurCon(); }
                }
                
            }
            else
            {
                Console.WriteLine("Bad ip format address for use! Go to ITSTEP and learn 'CISCO' course! Or try Again...");
            }
        }
        static void CloseCurCon() 
        {
            _socket.Close();
            _socket = null;
            _endPoint = null;
            _userString = "";
        }
    }
}
