using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TDNPGL.Cli
{
    internal class NativeMethods
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcessId();

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, ref IntPtr ProcessId);

        internal static bool IsOwnConsole()
        {
            if (!Os.IsWindows)
                return false;
            IntPtr hConsole = GetConsoleWindow();
            IntPtr hProcessId = IntPtr.Zero;
            GetWindowThreadProcessId(hConsole, ref hProcessId);
            return GetCurrentProcessId().Equals(hProcessId);
        }
    }
}
