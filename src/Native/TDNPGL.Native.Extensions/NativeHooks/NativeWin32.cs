using System.Runtime.InteropServices;
using TDNPGL.Core.Math;

namespace TDNPGL.Native.Extensions
{
    internal class NativeWin32 : AbstractNative
    {
        static NativeWin32()
        {
        }
        [DllImport("x86\\tdnpgl.dll", EntryPoint = "AABB_IsPointOver", CallingConvention = CallingConvention.Cdecl)]
        private static extern int AABB_IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy);

        public override int AABB_IsPointOverNative(AABB aabb, Vec2f point)
        {
            return AABB_IsPointOver(point.X,
            point.Y,
            aabb.min.X,
            aabb.min.Y,
            aabb.max.X,
            aabb.max.Y);
        }
    }
}