using System;
using System.Threading;
using System.Collections.Concurrent;
using System.IO;

namespace ParallelWaitAll
{
    class Program
    {
        public delegate void TaskDelegate();
        public class TaskQueue
        {
            public ConcurrentQueue<TaskDelegate> pool = new ConcurrentQueue<TaskDelegate>();
            
            private bool disposeFlag = false;
            public void EnqueueTask(TaskDelegate task)
            {
                pool.Enqueue(task);
            }
            public TaskQueue(int threadNum)
            {
                for (var i = 0; i < threadNum; i++)
                {
                    new Thread(Operation).Start();
                }
            }
            public void Operation()
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
        public class Parallel
        {
            public static void WaitAll(TaskDelegate[] delegateArray)
            {
                TaskQueue taskQueue = new TaskQueue(10);
                int counter = 0;
                for (int i = 0; i < delegateArray.Length; i++)
                {
                    delegateArray[i] = delegateArray[i] + (() => Interlocked.Increment(ref counter));
                    taskQueue.EnqueueTask(delegateArray[i]);
                }
                while (counter != delegateArray.Length)
                {
                    Thread.Yield();
                }
                taskQueue.Dispose();
            }
        }
        
        static void Main(string[] args)
        {
            TaskDelegate[] delegateArray = new TaskDelegate[20];
            for (int i = 0; i < 20; i++)
            {
                int j = i;
                delegateArray[i] = () => Console.WriteLine($"Task {j + 1}");
            }
            Parallel.WaitAll(delegateArray);
            Console.WriteLine("All tasks have been completed");
        }
    }
}
