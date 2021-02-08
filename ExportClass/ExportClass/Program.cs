using System;
using System.Reflection;

namespace ExportClass
{
    class Program
    {
        [ExportClass]
        private class PrivateClass
        {

        }
        [ExportClass]
        public class PublicClass
        {

        }
        public class AnotherPublicClass
        {

        }
        [ExportClass]
        public class Test
        {

        }
        [AttributeUsage(AttributeTargets.Class)]
        public class ExportClass : System.Attribute 
        {

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Type in path to a dll file:");
            string path = Console.ReadLine();
            Assembly assembly = Assembly.LoadFrom(path);
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if((type.GetCustomAttribute<ExportClass>(true) != null) && (type.IsNestedPublic || type.IsPublic))
                    Console.WriteLine(type.FullName);
            }
        }
    }
}
