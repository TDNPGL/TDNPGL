using System.Runtime.InteropServices;
using TDNPGL.Core.Math;

namespace TDNPGL.Native.Extensions
{
    internal class NativeLinuxX86 : AbstractNative
    {
        static NativeLinuxX86()
        {
        }
        [DllImport("x86\\libtdnpgl.so", EntryPoint = "AABB_IsPointOver", CallingConvention = CallingConvention.Cdecl)]
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
