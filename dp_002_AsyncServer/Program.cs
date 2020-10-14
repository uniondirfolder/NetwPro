using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dp_002_AsyncServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTimeServer server new DateTimeServer(IPAddress.Loopback, 10001);
        }
    }
}
