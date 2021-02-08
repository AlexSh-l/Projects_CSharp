using System;
using System.Net.Sockets;
using System.IO;
using System.Threading.Tasks;

namespace ProxyServer
{
    class Program
    {
        static void Main(string[] args)        
        {           
            string blackList = "";
            using (StreamReader data = new StreamReader("Banned.txt", System.Text.Encoding.Default))
            {
                blackList = data.ReadToEnd();
            }
            Server proxyServer = new Server("127.0.0.1", 8888, blackList);
            proxyServer.Start();
            while (true)
            {
                Socket socket = proxyServer.AcceptConnection();                
                Task.Factory.StartNew(() => proxyServer.DataFlow(socket));
            }
        }
    }
}
