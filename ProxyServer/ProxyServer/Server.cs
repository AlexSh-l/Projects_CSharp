using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace ProxyServer
{
    public class Server
    {
        public byte[] buffer;
        public int port;
        public string host;
        public string[] blackList;
        public TcpListener listener;                
        public Server(string host, int port, string blacklist)
        {
            this.host = host;
            this.port = port;
            this.listener = new TcpListener(IPAddress.Parse(this.host), this.port);
            this.blackList = blacklist.Trim().Split(new char[] { '\r', '\n' });
        }                
        public void DataFlow(Socket socket)
        {
            NetworkStream browserData = new NetworkStream(socket);
            buffer = new byte[20000];
            while (true)                                                        
            {
                if (browserData.CanRead)
                {
                    try
                    {
                        browserData.Read(buffer, 0, buffer.Length);
                    }
                    catch (IOException)
                    {
                        return;
                    }
                    ResponseCreator(buffer, browserData);
                    socket.Dispose();
                }
                else
                {
                    return;
                }
            }
        }
        public Socket AcceptConnection()
        {
            return listener.AcceptSocket();
        }
        public void Start()
        {
            listener.Start();
        }       
        public void ResponseCreator(byte[] buffer, NetworkStream browserStream)
        {
            TcpClient server;
            string message;
            string Code;
            try
            {
                string tmpBuff = Encoding.UTF8.GetString(buffer);
                Regex regexpr = new Regex(@"http:\/\/[a-zа-яё0-9\:\.]*");
                MatchCollection matches = regexpr.Matches(tmpBuff);
                string host = matches[0].Value;
                tmpBuff = tmpBuff.Replace(host, "");
                buffer = Encoding.UTF8.GetBytes(tmpBuff);
                string[] temp = tmpBuff.Trim().Split(new char[] { '\r', '\n' });
                string request = temp.FirstOrDefault(x => x.Contains("Host"));
                request = request.Substring(request.IndexOf(":") + 2);
                string[] IPAndP = request.Trim().Split(new char[] { ':' });                                  
                if (IPAndP.Length == 2)                                                                  
                {                                                                                             
                    server = new TcpClient(IPAndP[0], int.Parse(IPAndP[1]));
                }
                else
                {
                    server = new TcpClient(IPAndP[0], 80);
                }

                NetworkStream serverStream = server.GetStream();                                           
                if (blackList != null && Array.IndexOf(blackList, request.ToLower()) != -1)
                {
                    byte[] bannedResponse = Encoding.UTF8.GetBytes("HTTP/1.1 403 Forbidden\r\nContent-Type: text/html\r\nContent-Length: 31\r\n\r\nThis page has been blacklisted.");
                    browserStream.Write(bannedResponse, 0, bannedResponse.Length);
                    Code = "403";
                    message = request + " " + Code;
                    Console.WriteLine(message);
                    return;
                }

                serverStream.Write(buffer, 0, buffer.Length);                                               
                var bufResponse = new byte[32];                                                       
                serverStream.Read(bufResponse, 0, bufResponse.Length);                                       
                browserStream.Write(bufResponse, 0, bufResponse.Length);                                       
                string[] browserResponse = Encoding.UTF8.GetString(bufResponse).Split(new char[] { '\r', '\n' });     
                Code = browserResponse[0].Substring(browserResponse[0].IndexOf(" ") + 1);
                message = request + " " + Code;
                Console.WriteLine(message);
                serverStream.CopyTo(browserStream);                                                                            
            }
            catch 
            { 
                return; 
            }
        }                   
    }
}
