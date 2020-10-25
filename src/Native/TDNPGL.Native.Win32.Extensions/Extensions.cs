using SkiaSharp;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using TDNPGL.Core.Gameplay.LowLevel;
using TDNPGL.Native.Win32.Extensions.Delegates;
using TDNPGL.Native.Win32.Extensions.Properties;
using TDNPGL.NativeLoader;

namespace TDNPGL.Native.Win32.Extensions
{
    public static class Extensions
    {
        public static WinNativeLibrary TDNPGLNative;
        public static bool IsPointOverNative(this AABB aabb, SKPoint point)
        {
            try
            {
                return TDNPGLNative.
                    GetDelegate<IsPointOver>("AABB_IsPointOver").
                    Invoke(
                    point.X,
                    point.Y,
                    aabb.min.X,
                    aabb.min.Y,
                    aabb.max.X,
                    aabb.max.Y);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        static Extensions(){
            byte[] buffer = Resources.TDNPGLNativeWin32;
            TDNPGLNative = WinNativeLibrary.GetLibrary(buffer,"TDNPGL.Native.Win32.dll","");
        }
    }
}
