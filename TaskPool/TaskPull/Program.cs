using System;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;

namespace TaskPull
{
    class Program
    {
        private static int copiedFilesCounter = 0;
        private static object copiedFilesLock = new object();
        private static int threadEndedCounter = 0;
        private static int threadsCounter = 0;
        private static object threadEndedLock = new object();        
       
        public class TaskQueue
        {
            private ConcurrentQueue<TaskDelegate> pool = new ConcurrentQueue<TaskDelegate>();
            public delegate void TaskDelegate();
            private bool disposeFlag = false;
            public void EnqueueTask(TaskDelegate task) 
            {               
                pool.Enqueue(task);
            }
            public TaskQueue(int threadNum)
            {
                for (var i = 0; i < threadNum; i++)
                {
                    new Thread(CopyOperation).Start();
                }
            }            
            public void CopyOperation()
            {
                while (!disposeFlag)
                {
                    pool.TryDequeue(out var task);
                    if (task == null)
                    {
                        Thread.Sleep(300);
                    }
                    else
                    {
                        task();
                    }
                }
            }
            public void Dispose()
            {
                if (disposeFlag)
                    return;
                disposeFlag = true;
            }            
        }
        public static void CopyFile(string root, string target)
        {
            try
            {
                File.Copy(root, target, true);
                lock (copiedFilesLock)
                {
                    copiedFilesCounter++;
                }
            }
            catch
            {
                
            }
            lock (threadEndedLock)
            {
                threadEndedCounter++;
            }
        }
        public static void CopyDirectory(string root, string target, TaskQueue taskQueue)
        {
            var directories = Directory.GetDirectories(root);
            target = target + "\\" + Path.GetFileName(root);
            Directory.CreateDirectory(target);
            foreach (var filePath in Directory.GetFiles(root))
            {                
                taskQueue.EnqueueTask(() => CopyFile(filePath, target + "\\" + Path.GetFileName(filePath)));
                threadsCounter++;
            }
            foreach (var directory in directories)
            {
                CopyDirectory(directory, target, taskQueue);
            }
        }

        static void Main(string[] args)
        {
            int threadNum = 100;
            string targetStr, rootStr;
            Console.WriteLine("Type in the root folder");
            rootStr = Console.ReadLine();
            Console.WriteLine("Type in the target folder");
            targetStr = Console.ReadLine();            
            if(!Directory.Exists(rootStr) || !Directory.Exists(targetStr))
            {
                Console.WriteLine("Incorrect paths");
                return;
            }
            var taskQueue = new TaskQueue(threadNum);
            CopyDirectory(rootStr, targetStr, taskQueue);
            while (threadsCounter != threadEndedCounter)
            {
                Thread.Sleep(300);
            }
            Console.WriteLine("Files total: {0}", threadsCounter);
            Console.WriteLine("Sucsessfully copied: {0}", copiedFilesCounter);
            taskQueue.Dispose();          
        }
    }
}
