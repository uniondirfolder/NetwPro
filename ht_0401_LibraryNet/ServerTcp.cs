using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ht_0401_LibraryNet
{
    public class LocalMsg
    {
        public bool haveMsg { get; set; } = false;
        public string msg { get; set; } = "NaN";
    }

    public class MadText
    {
        public List<string> Text { get; set; }

        public MadText(string textFile)
        {
            try
            {
               Text=new List<string>();
               using (var file = File.OpenText(textFile))
               {
                   while (!file.EndOfStream)
                   {
                       var str = file.ReadLine();
                       if (!string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str))
                       {
                           Text.Add(str);
                       }
                   }
               }
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
        }
    }

    public enum MsgType
    {
        CmdRun,
        CmdStop,
        CmdOther
    }

    public class Client
    {
        public int ClientGuid { get; }= new Guid().GetHashCode();
        public string Name { get; set; } = "anonymous";

        public Client()
        {
            
        }

        public Client(string name)
        {
            this.Name = name;
        }
    }

    [Serializable]
    public class Msg
    {
        public string Name { get; set; }
        public string Guid { get; set; }
        public MsgType MsgType { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $"ID: {Guid} | Client: {Name} | MT: {MsgType} | MSG: {Body}";
        }
    }


    public class ServerTcp
    {
        private Client Id = new Client("Server");
        private TcpListener server = null;
        private IPAddress ipAddress;
        private int port;
        

        private ListBox lbMonitor;
        private MadText madText;
        private bool console = true;
        private bool run = true;
        public LocalMsg localMsg;

        public ServerTcp(IPAddress ipAddress, int port, LocalMsg localMsg)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.localMsg = localMsg;
            madText = new MadText(Directory.GetCurrentDirectory() + @"\exceptionLog.txt");
        }

        public void Start(ListBox monitor=null)
        {
            try
            {
                this.server = new TcpListener(ipAddress,port);
                this.server.Start();
                localMsg.msg = "Start" + DateTime.Now.ToString();
                localMsg.haveMsg = true;
                while (run)
                {
                    TcpClient client = server.AcceptTcpClient();

                    NetworkStream networkStream = client.GetStream();

                    BinaryFormatter formatter = new BinaryFormatter();
                    Msg request = (Msg)formatter.Deserialize(networkStream);

                    Msg answer = new Msg();

                    switch (request.MsgType)
                    {
                        case MsgType.CmdRun:
                            break;
                        case MsgType.CmdStop:
                            Stop();
                            break;
                        case MsgType.CmdOther:
                            Random rnd = new Random();
                            var indx = rnd.Next(madText.Text.Count);
                            answer.Name = Id.Name;
                            answer.Guid = Id.ClientGuid.ToString();
                            answer.MsgType = MsgType.CmdOther;
                            answer.Body = madText.Text[indx];
                            //Print(answer.ToString());
                            localMsg.msg = answer.ToString();
                            localMsg.haveMsg = true;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    formatter.Serialize(networkStream, answer);
                }
                
            }
            catch (Exception e)
            {
                File.AppendAllText(Directory.GetCurrentDirectory() + @"\temptext.txt", DateTime.Now.ToString(CultureInfo.InvariantCulture) + e.Message);
            }

            
        }

        public void Stop()
        {
            run = false;
        }

      
        public void Print(string str)
        {
            if (console)
            {
                Console.WriteLine(str);
            }
            else
            {
                lbMonitor.Items.Add(str);

            }
        }
    }


}
