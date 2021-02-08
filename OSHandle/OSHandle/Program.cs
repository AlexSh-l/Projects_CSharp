using System;
using System.Runtime.InteropServices;
using System.Globalization;

namespace OSHandle
{
    class Program
    {
        public class OSHandle : IDisposable
        {
            [DllImport("Kernel32.dll")]
            private static extern bool CloseHandle(IntPtr handle);
            public IntPtr Handle { get; private set; }
            public OSHandle(IntPtr handle)
            {
                Handle = handle;
            }
            private bool disposeFlag = false;
            public void DisposeWithFinalize()
            {
                if (disposeFlag)
                {
                    CloseHandle(Handle);
                    Handle = IntPtr.Zero;
                    disposeFlag = true;
                }
            }
            public void Dispose()
            {
                DisposeWithFinalize();
                GC.SuppressFinalize(this);
            }
            public bool Close()
            {
                bool hResult = CloseHandle(Handle);
                Dispose();
                return hResult;
            }
            ~OSHandle()
            {
                DisposeWithFinalize();                
            }
        }
        static void Main(string[] args)
        {
            int userHandle;
            Console.WriteLine("Descriptor: ");           
            if (int.TryParse(Console.ReadLine(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out userHandle))
            {
                var osHandle = new OSHandle(new IntPtr(userHandle));
                if (osHandle.Close())
                    Console.WriteLine("Descriptor successfully closed");
                else
                    Console.WriteLine("Unsuccessful operation");
                osHandle.Dispose();
            }
        }
    }
}
