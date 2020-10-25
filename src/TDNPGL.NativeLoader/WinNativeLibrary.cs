using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace TDNPGL.NativeLoader
{
    public class WinNativeLibrary : NativeLibrary
    {
        public IntPtr Handle { get; private set; }
        private const string kernel = "kernel32.dll";

        [DllImport(kernel, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr LoadLibrary(string lib);
        [DllImport(kernel, SetLastError = true)]
        private static extern void FreeLibrary(IntPtr module);
        [DllImport(kernel, SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr module, string proc);
        private WinNativeLibrary(string file, IntPtr lib, int flags) : base(file,lib,flags)
        {
            if (Environment.Is64BitProcess)
                throw new Exception(String.Format("Can't load {0} because this is a 64 bit proccess", file));
            if (!File.Exists(file))
                throw new FileNotFoundException(file);
            IntPtr llib = LoadLibrary(file);

            if (llib==IntPtr.Zero)
                throw new Win32Exception();

            Handle = llib;
        }
        public override T GetDelegate<T>(string name)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) 
                throw new PlatformNotSupportedException();

            IntPtr method = GetProcAddress(Handle, name);

            if (method == IntPtr.Zero)
                throw new Win32Exception();

            T result = (T)Marshal.GetDelegateForFunctionPointer(method, typeof(T));
            return result;
        }
        public static WinNativeLibrary GetLibrary(byte[] bytes, string name, string libraryExtension=".dll")
        {
            string fileName = name + libraryExtension;
            File.WriteAllBytes(fileName, bytes);
            return GetLibrary(fileName);
        }
        public static WinNativeLibrary GetLibrary(string file)
        {
            WinNativeLibrary library = new WinNativeLibrary(file, IntPtr.Zero, 0);
            return library;
        }
        public override void Dispose()
        {
            FreeLibrary(Handle);
        }
    }
}
