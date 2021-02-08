using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LogBuffer
{
    class Program
    {
        
        public class LogBuffer
        {
            private List<string> Logs = new List<string>();
            private List<string> Buffer = new List<string>();
            private static int msgCounter = 0;
            private const int msgMax = 10;
            public void AddString(string item)
            {
                Logs.Add(item);
                BufferMessage(item);
            }
            private void BufferMessage(string message)
            {
                Buffer.Add(message);
                if (msgCounter < msgMax - 1)
                {
                    msgCounter++;
                }
                else
                {                    
                    FileSave();                   
                    msgCounter = 0;
                }
            }
            private void FileSave()
            {
                File.AppendAllLines("Logs.txt", Buffer);
                Buffer.Clear();
            }
            public async void FileSaveAsync(object obj)
            {
                await Task.Run(FileSave);
            }
        }

        static void Main(string[] args)
        {
            string str;
            LogBuffer logbuffer = new LogBuffer();
            Timer timer = new Timer(new TimerCallback(logbuffer.FileSaveAsync), null, 0, 5000);
            do
            {
                str = Console.ReadLine();
                logbuffer.AddString(str);
            } while (true);
        }
    }
}
