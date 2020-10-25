using System;
using System.Data.Common;

namespace TDNPGL.Core{
    public struct Vec2f{
        public float X;
        public float Y;
        public SkiaSharp.SKPoint ToSKPoint(){
            return new SkiaSharp.SKPoint(this.X,this.Y);
        }
        public System.Numerics.Vector2 ToVector2()
        {
            return new System.Numerics.Vector2(this.X, this.Y);
        }
        public static implicit operator SkiaSharp.SKPoint(Vec2f point){
            return point.ToSKPoint();
        }
        public static implicit operator System.Numerics.Vector2(Vec2f point)
        {
            return point.ToVector2();
        }
        public Vec2f(float x,float y){
            this.X=x;
            this.Y=y;
        }
        public static Vec2f operator +(Vec2f a, Vec2f b)
        {
            return new Vec2f(a.X + b.X, a.Y + b.Y);
        }
        public static Vec2f operator -(Vec2f a, Vec2f b)
        {
            return new Vec2f(a.X - b.X, a.Y - b.Y);
        }
    }
}