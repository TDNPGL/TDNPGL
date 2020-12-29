using TDNPGL.Core.Math;

namespace TDNPGL.Native.Extensions
{
    public abstract class AbstractNative
    {
        public abstract int AABB_IsPointOverNative(AABB aabb, Vec2f point);
    }
}