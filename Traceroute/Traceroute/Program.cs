using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace traceroute
{
    class Program
    {        
        const int RequestNum = 3;        
        const int MessageSize = 50000;
        const int TTLExceded = 11;
        const int TTLMax = 30;     
        const int WaitingTime = 2500;
        const int MaxErrCntr = 30;
        static int ExtendeMode;
        static int LastReqNum = RequestNum - 1;
        const int ICMPHdrSz = 4;
        const int ICMPTOM = 2;
        const int ICMPC = 2;
        const int EchoRequest = 8;
        const int EchoReply = 0;
        static void Main()
        {
            string mode, adress;
            Console.Write("traceroute ");
            adress = Console.ReadLine();
            Console.Write("Выберите режим работы('n' с разрешениим имён, '' без):");
            mode = Console.ReadLine();
            if (adress != "")
            {
                if (mode == "n")
                {
                    ExtendeMode = 1;
                }
                else
                {
                    if (mode == "")
                    {
                        ExtendeMode = 0;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод данных");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод данных");
                return;
            }
            IPAddress targetIP;
            IPEndPoint targetIPEndPoint;            
            if (IPAddress.TryParse(adress, out targetIP)) // IP
            {
                if (targetIP.AddressFamily != AddressFamily.InterNetwork)
                {
                    Console.WriteLine("Целевой узел должен быть либо в виде URL, либо IPv4");
                    return;
                }
                else
                {
                    targetIPEndPoint = new IPEndPoint(targetIP, 0);
                    try
                    {
                        string hostName = Dns.GetHostEntry(targetIP).HostName;
                        Console.WriteLine("\nТрассировка маршрута к {0} [{1}]\n максимальное число прыжков {2}:\n", hostName, targetIP, TTLMax);
                    }
                    catch
                    {
                        Console.WriteLine("\nТрассировка маршрута к {0} максимальное число прыжков {1}:\n", targetIP, TTLMax);
                    }
                }
            }
            else // Домен
            {
                try
                {
                    IPHostEntry ipHostEntry = Dns.GetHostEntry(adress);
                    targetIPEndPoint = new IPEndPoint(ipHostEntry.AddressList[0], 0);
                    Console.WriteLine("\nТрассировка маршрута к {0} [{1}]\n максимальное число прыжков {2}:\n", adress, targetIPEndPoint.Address, TTLMax);
                }
                catch (SocketException)
                {
                    Console.WriteLine("Не удается разрешить системное имя узла {0}.", adress);
                    return;
                }
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            Traceroute(socket, targetIPEndPoint, ExtendeMode);
            return;
        }
        static byte[] PacketGen(/*ref int PacketSize*/)
        {
            byte[] message = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwabcdefghi");
            byte[] data = new byte[message.Length + ICMPTOM + ICMPC + ICMPHdrSz];
            data[0] = EchoRequest;
            data[1] = 0;
            Buffer.BlockCopy(message, 0, data, ICMPHdrSz, message.Length);
            //PacketSize = message.Length + ICMPTOM + ICMPC + ICMPHdrSz;
            UInt16 checksum = CheckSum(data, data.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(checksum), 0, data, 2, 2);
            return data;
        }
        static UInt16 CheckSum(byte[] data, int PacketSize)
        {
            UInt32 checksum = 0;
            for (int i = 0; i < PacketSize; i += 2)
            {
                checksum += Convert.ToUInt32(BitConverter.ToUInt16(data, i));
            }
            checksum = (checksum >> 16) + (checksum & 0xffff);
            checksum += (checksum >> 16);
            return (UInt16)(~checksum);
        }
        static int PingCalc(DateTime startmomnt, DateTime endmomnt) //datetime не точен, stopwatch
        {
            var result = endmomnt.Millisecond - startmomnt.Millisecond;
            return result;
        }        
        static void Traceroute(Socket socket, IPEndPoint targetIPEndPoint, int ExtMode)
        {
            int respSize, errCounter = 0;
            byte[] respBytes;
            DateTime beginMomnt, endMomnt;
            EndPoint targetEndPoint = targetIPEndPoint;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, WaitingTime);
            for (int TTL = 1; TTL < TTLMax; TTL++)
            {
                
                byte[] myPacket = PacketGen(/*ref PacketSize*/);
                int PacketSize = myPacket.Length;
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.IpTimeToLive, TTL);
                Console.Write("{0})\t", TTL);
                for (int NumOFReq = 0; NumOFReq < RequestNum; NumOFReq++)
                {
                    respBytes = new byte[MessageSize];
                    beginMomnt = DateTime.Now;
                    socket.SendTo(myPacket, PacketSize, SocketFlags.None, targetIPEndPoint);
                    try
                    {
                        respSize = socket.ReceiveFrom(respBytes, ref targetEndPoint);
                        endMomnt = DateTime.Now;

                        if ((respBytes[20] == EchoReply) || (respBytes[20] == TTLExceded))
                        {
                            if (PingCalc(beginMomnt, endMomnt) <= 0)
                            {
                                Console.Write("<1 мс\t");
                            }
                            else
                            {
                                Console.Write("{0} мс\t", PingCalc(beginMomnt, endMomnt));
                            }
                            if (NumOFReq == LastReqNum)
                            {
                                String currIP = targetEndPoint.ToString();
                                currIP = currIP.Replace(":0", "");
                                if (ExtMode == 0)
                                {
                                    Console.WriteLine("{0}", currIP);
                                }
                                else
                                {
                                    try
                                    {
                                        var hostName = Dns.GetHostEntry(IPAddress.Parse(currIP)).HostName;
                                        Console.WriteLine("{0} [{1}]", hostName, currIP);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("{0}", currIP);
                                    }
                                }
                            }
                        }
                        if ((respBytes[20] == EchoReply) && (NumOFReq == LastReqNum))
                        {
                            Console.WriteLine("\nТрассировка завершена.");
                            return;
                        }
                        errCounter = 0;
                    }
                    catch (SocketException)
                    {
                        Console.Write("*\t");
                        if (NumOFReq == LastReqNum)
                        {
                            Console.WriteLine("Превышен интервал ожидания для запроса.");
                        }
                        errCounter++;
                        if (errCounter == MaxErrCntr)
                        {
                            Console.WriteLine("Невозможно связаться с удаленным хостом.");
                            return;
                        }
                    }
                }
            }
        }               
    }
}
