using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Core.Gameplay.LowLevel
{
    public struct AABB
    {
        public Vec2f max;
        public Vec2f min;

        public static bool AABBvsAABB(AABB a, AABB b)
        {
            if (a.max.X < b.min.X || a.min.X > b.max.X) return false;
            if (a.max.Y < b.min.Y || a.min.Y > b.max.Y) return false;

            return true;
        }
        public static bool IsPointOver(AABB a,SKPoint point){
            return (point.X>a.min.X&&point.X<a.max.X)&&(point.Y>a.min.Y&&point.Y<a.max.Y);
        }
        public bool IsPointOver(SKPoint point)
        {
            return AABB.IsPointOver(this,point);
        }
        public SKRect ToRect()
        {
            return new SKRect(min.X, min.Y, max.X, max.Y);
        }
        public AABB(float x,float y,float maxx,float maxy)
        {
            min.X = x;
            min.Y = y;
            max.X = x;
            max.Y = y;
        }
    }
}
