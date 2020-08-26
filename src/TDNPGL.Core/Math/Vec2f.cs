using System;
namespace TDNPGL.Core{
    public struct Vec2f{
        public float X;
        public float Y;
        public SkiaSharp.SKPoint ToSKPoint(){
            return new SkiaSharp.SKPoint(this.X,this.Y);
        }
        public static implicit operator SkiaSharp.SKPoint(Vec2f point){
            return point.ToSKPoint();
        }
        public Vec2f(float x,float y){
            this.X=x;
            this.Y=y;
        }
    }
}