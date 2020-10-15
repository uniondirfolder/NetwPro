using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace dn_003_WinClient
{
    public static class UdpClinettExtention
    {
        public static void SendBroadcast(this UdpClient udpClient, string msg)
        {
            var data = Encoding.Unicode.GetBytes(msg);
            udpClient.Send(data, data.Length, "192.168.115.255", 10_000);
        }
    }
}
