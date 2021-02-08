using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicList
{
    class Program
    {
        public class DynamicList<T> : IEnumerable<T>
        {
            private T[] array = new T[0];
            private int size = 0;
            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return ((IEnumerable<T>)array).GetEnumerator();
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return array.GetEnumerator();
            }
            public int Count { get { return size; } }
            public void Add(T item)
            {
                Array.Resize<T>(ref array, size + 1);
                array[size] = item;
                size++;
            }
            public void Remove(T item)
            {
                int i = Array.IndexOf<T>(array, item);
                if (i >= 0)
                {
                    RemoveAt(i + 1);
                }
            }
            public void RemoveAt(int i)
            {                
                Array.Copy(array, i, array, i - 1, size - i);
                size--;
                Array.Resize<T>(ref array, size);
            }
            public T this[int i] { get { return array[i - 1]; } set { array[i - 1] = value; }
            }
            public void Clear()
            {
                if( size > 0)
                {
                    Array.Clear(array, 0, size);
                    size = 0;
                }
            }            
        }
        static void Main(string[] args)
        {
            DynamicList<string> list = new DynamicList<string>();
            list.Add("trial");
            list.Add("test");
            list.Add("my");
            list.Add("dynamic");
            list.Add("list");
            foreach(var str in list)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine($"There are {list.Count} elements in my list");
            Console.WriteLine($"Element with an index {list.Count / 2} is: {list[list.Count / 2]}");
            list.RemoveAt(3);
            Console.WriteLine("Removed element with an index 3");
            list.Remove("dynamic");
            Console.WriteLine("Removed element with a name \"dynamic\"");
            foreach (var str in list)
            {
                Console.WriteLine(str);
            }
            list.Clear();
            Console.WriteLine("Cleared entire list");
            foreach (var str in list)
            {
                if (str!=null)
                    Console.WriteLine(str);
            }
        }
    }
}
