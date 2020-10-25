using SkiaSharp;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using TDNPGL.Core.Math;
using TDNPGL.Native.Win32.Extensions.Properties;

namespace TDNPGL.Native.Win32.Extensions
{
    public static class Extensions
    {
        public static bool IsPointOverNative(this AABB aabb, Vec2f point) =>
            AABB_IsPointOver(
            point.X,
            point.Y,
            aabb.min.X,
            aabb.min.Y,
            aabb.max.X,
            aabb.max.Y) == 1;

        static Extensions()
        {
            byte[] buffer = Resources.TDNPGLNativeWin32;
            File.WriteAllBytes("TDNPGL.Native.Win32.dll", buffer);
        }
        [DllImport("TDNPGL.Native.Win32.dll", EntryPoint = "AABB_IsPointOver", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy);
    }
}
