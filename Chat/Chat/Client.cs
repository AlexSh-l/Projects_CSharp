using System.Net.Sockets;
using System.Net;

namespace Chat
{
    public class Client
    {
        public IPAddress IPV4Addr { get; set; }         
        public TcpClient tcpclient;
        public string Nickname { get; set; }
        public NetworkStream stream;
        public bool connected = true;             
    }
}
