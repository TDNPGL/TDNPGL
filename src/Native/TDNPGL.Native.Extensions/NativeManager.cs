using TDNPGL.Core.Math;

namespace TDNPGL.Native.Extensions
{
    internal class NativeManager
    {
        public static bool IsPointOverNative<T>(AABB aabb, Vec2f point) where T : AbstractNative, new()=>
            new T().AABB_IsPointOverNative(
            aabb,point
            ) == 1;
    }
}