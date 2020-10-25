using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Core.Math
{
    public struct AABB
    {
        public Vec2f max;
        public Vec2f min;
        public float Height => max.Y - min.Y;
        public float Width => max.X - min.X;
        public float X => min.X;
        public float Y => min.Y;

        public static bool AABBvsAABB(AABB a, AABB b)
        {
            if (a.max.X < b.min.X || a.min.X > b.max.X) return false;
            if (a.max.Y < b.min.Y || a.min.Y > b.max.Y) return false;

            return true;
        }
        public static bool IsPointOver(AABB a, Vec2f point)
        {
            return (point.X > a.min.X && point.X < a.max.X) && (point.Y > a.min.Y && point.Y < a.max.Y);
        }
        public bool IsPointOver(Vec2f point)
        {
            return IsPointOver(this, point);
        }
        public SKRect ToRect()
        {
            return new SKRect(min.X, min.Y, max.X, max.Y);
        }
        public AABB(float x, float y, float maxx, float maxy)
        {
            min.X = x;
            min.Y = y;
            max.X = maxx;
            max.Y = maxy;
        }
    }
}
