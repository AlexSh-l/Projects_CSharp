using System;
using System.Threading;

namespace Mutex
{
    class Program
    {
        public class Mutex
        {
            private Thread thread = null;
            public void Lock()
            {
                while(Interlocked.CompareExchange(ref thread, Thread.CurrentThread, null) != null)
                {
                    Thread.Yield();
                }
            }
            public void Unlock()
            {
                if(Interlocked.CompareExchange(ref thread, Thread.CurrentThread, null) != Thread.CurrentThread)
                {
                    throw new ApplicationException();
                }
            }
        }
        private static void Action(object mutexObj)
        {
            var mutex = mutexObj as Mutex;
            mutex.Lock();
            Console.WriteLine($"{Thread.CurrentThread.Name} Performed some action");
            mutex.Unlock();
        }

        static void Main(string[] args)
        {
            var mutex = new Mutex();
            for (var i = 1; i <= 7; i++)
            {
                var thread = new Thread(Action) { Name = $"Thread № {i}" };
                thread.Start(mutex);
            }          
        }
    }
}
