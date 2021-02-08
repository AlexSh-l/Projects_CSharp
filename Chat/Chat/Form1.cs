using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Chat
{
    public partial class Chatrr : Form
    {
        static string Nickname;
        static string Message;
        static int Port = 8883;
        static int BrPort = 8881;
        static IPAddress IPAddr;
        static int PacketNum = 5;
        static List<Client> clients = new List<Client>();
        private bool connected = false;
        public NetworkStream stream;
        public TcpClient tcpClient;

        public const byte TMessage = 1;
        //public const byte THistoryRequest = 2;
        public const byte TUserConnected = 3;
        public const byte TUserDisconnected = 4;

        public Chatrr()
        {
            InitializeComponent();
            DiscBtn.Enabled = false;
            EntrBtn.Enabled = false;
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress tempIp in host.AddressList)
            {
                if (tempIp.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPAddr = tempIp;
                }
            }
            Task.Factory.StartNew(() => ListenerUDP());
        }

        private void RegBtn_Click(object sender, EventArgs e)
        {
            Nickname = NickBox.Text;
            if (Nickname == "")
            {

            }
            else
            {
                connected = true;
                UdpClient Uclient = new UdpClient("255.255.255.255", BrPort);
                byte[] Nickmsg = Encoding.Unicode.GetBytes(Nickname);
                Uclient.EnableBroadcast = true;
                Task.Factory.StartNew(()=> ConnectionCatcher());
                for (int i = 0; i < PacketNum; i++)
                {
                    Uclient.Send(Nickmsg, Nickmsg.Length);
                }
                Uclient.Dispose();
                RegBtn.Enabled = false;
                NickBox.Enabled = false;
                DiscBtn.Enabled = true;
                EntrBtn.Enabled = true;
            }
        }

        private void EntrBtn_Click(object sender, EventArgs e)
        {
            Nickname = NickBox.Text;
            Message = MessageBox.Text;
            if ((Message == "") || (Nickname == ""))
            {

            }
            else
            {
                TxtBox.Text += Nickname + ": " + Message + Environment.NewLine;
                foreach (Client client in clients)
                { 
                    var MessageBytes = Encoding.Unicode.GetBytes(Message);
                    var SendMessage = new byte[5 + MessageBytes.Length];
                    SendMessage[0] = TMessage;
                    var ByteLengthNick = BitConverter.GetBytes(MessageBytes.Length);
                    Buffer.BlockCopy(ByteLengthNick, 0, SendMessage, 1, 4);
                    Buffer.BlockCopy(MessageBytes, 0, SendMessage, 5, MessageBytes.Length);
                    client.stream.Write(SendMessage, 0, SendMessage.Length);
                }
                MessageBox.Text = "";
            }
        }
        public void ListenerUDP()
        {
            var udpCatcher = new UdpClient(BrPort);
            udpCatcher.EnableBroadcast = true;
            while (true)
            {
                IPEndPoint hostremote = null;
                var data = udpCatcher.Receive(ref hostremote);
                if (connected)
                {
                    var client = new Client();
                    client.IPV4Addr = hostremote.Address;
                    client.Nickname = Encoding.Unicode.GetString(data);
                    if ((!hostremote.Address.Equals(IPAddr.ToString())) && (client.Nickname != Nickname))
                    {
                        bool presence = false;
                        foreach (var temp in clients)
                        {
                            if (temp.IPV4Addr.Equals(client.IPV4Addr))
                            {
                                presence = true;
                                break;
                            }
                        }
                        if (presence == false)
                        {
                            tcpClient = new TcpClient();                            
                            tcpClient.Connect(new IPEndPoint(client.IPV4Addr, Port));
                            stream = tcpClient.GetStream();                      
                            client.stream = tcpClient.GetStream();
                            var NicknameBytes = Encoding.Unicode.GetBytes(Nickname);
                            var ConnectMessage = new byte[5 + NicknameBytes.Length];
                            ConnectMessage[0] = TUserConnected;
                            var ByteLengthNick = BitConverter.GetBytes(NicknameBytes.Length);
                            Buffer.BlockCopy(ByteLengthNick, 0, ConnectMessage, 1, 4);
                            Buffer.BlockCopy(NicknameBytes, 0, ConnectMessage, 5, NicknameBytes.Length);
                            stream.Write(ConnectMessage, 0, ConnectMessage.Length);
                            client.tcpclient = tcpClient;
                            clients.Add(client);
                            this.Invoke(new MethodInvoker(() =>
                            {
                                TxtBox.Text += client.Nickname + " Connected." + Environment.NewLine;
                            }));
                            Task.Factory.StartNew(() => ListenerTCP(clients[clients.IndexOf(client)]));
                        }
                    }
                }
            }
        }
       
        public void ConnectionCatcher()
        {     
            TcpListener tcpListener = new TcpListener(IPAddr, Port);
            tcpListener.Start();
            while (connected)
            {
                if (tcpListener.Pending())
                {
                    var client = new Client();
                    client.tcpclient = tcpListener.AcceptTcpClient();
                    client.IPV4Addr = ((IPEndPoint)client.tcpclient.Client.RemoteEndPoint).Address;
                    client.stream = client.tcpclient.GetStream();                 
                    byte[] data = new byte[5];
                    client.stream.Read(data, 0, 5);
                    int length = BitConverter.ToInt32(data, 1);
                    byte[] message = new byte[length];
                    client.stream.Read(message, 0, length);
                    string messagetxt = Encoding.Unicode.GetString(message);
                    client.Nickname = messagetxt;                    
                    clients.Add(client);                    
                    if (data[0] == TUserConnected)
                    {  
                            this.Invoke(new MethodInvoker(() =>
                            {
                                TxtBox.Text += "You've connected to the chat." + Environment.NewLine;
                            }));
                        Task.Factory.StartNew(() => ListenerTCP(clients[clients.IndexOf(client)]));
                    }
                }
            }
            tcpListener.Stop();            
        }
        public void ListenerTCP(Client client)
        {
            while (client.connected)
            {
                if (client.stream.DataAvailable)
                {
                    byte[] data = new byte[5];
                    client.stream.Read(data, 0, 5);
                    byte Type = data[0];
                    int Length = BitConverter.ToInt32(data, 1);
                    switch (Type)
                    { 
                        case TMessage:
                            byte[] msg = new byte[Length];
                            client.stream.Read(msg, 0, Length);
                            string msgtxt = Encoding.Unicode.GetString(msg);
                            this.Invoke(new MethodInvoker(() =>
                            {
                                TxtBox.Text += client.Nickname + ": " + msgtxt + Environment.NewLine;
                            }));
                            break;
                        case TUserDisconnected:
                            client.connected = false;
                            this.Invoke(new MethodInvoker(() =>
                            {
                                TxtBox.Text += client.Nickname + " Disconnected." + Environment.NewLine;
                            }));
                            
                            client.stream.Close();
                            client.stream.Dispose();
                            client.tcpclient.Close();
                            client.tcpclient.Dispose();
                            clients.Remove(client);
                            break;
                    }
                }
            }
        }

        private void DiscBtn_Click(object sender, EventArgs e)
        {
            RegBtn.Enabled = true;
            NickBox.Enabled = true;
            DiscBtn.Enabled = false;
            connected = false;
            EntrBtn.Enabled = false;
            TxtBox.Text += "You've disconnected from the chat." + Environment.NewLine;
            foreach (Client client in clients)
            {
                var data = new byte[5];
                data[0] = TUserDisconnected;
                int length = 0;
                var lengthBytes = BitConverter.GetBytes(length);
                Buffer.BlockCopy(lengthBytes, 0, data, 1, 4);
                client.stream.Write(data, 0, data.Length);
                client.connected = false;
                client.stream.Close();
                client.stream.Dispose();
                client.tcpclient.Close();
                client.tcpclient.Dispose();
            }
            clients.Clear();
        }

        private void Chatrr_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connected==true)
            {
                connected = false;
                TxtBox.Text += "You've disconnected from the chat." + Environment.NewLine;
                foreach (Client client in clients)
                {
                    var data = new byte[5];
                    data[0] = TUserDisconnected;
                    int length = 0;
                    var lengthBytes = BitConverter.GetBytes(length);
                    Buffer.BlockCopy(lengthBytes, 0, data, 1, 4);
                    client.stream.Write(data, 0, data.Length);
                    client.connected = false;
                    client.stream.Close();
                    client.stream.Dispose();
                    client.tcpclient.Close();
                    client.tcpclient.Dispose();
                }
                clients.Clear();
            }
        }
    }
}
