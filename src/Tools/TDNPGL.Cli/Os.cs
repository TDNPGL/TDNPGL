using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TDNPGL.Cli
{
    public class Os
    {
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        public static bool IsFreeBSD => RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD);
        public static bool IsOSX => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        public static string FileSystemSlash => IsWindows ? "\\" : "/";
    }
}
