using Newtonsoft.Json;
using System.Numerics;
using TDNPGL.Core;
using TDNPGL.Core.Math;

namespace TDNPGL.Core
{
    public static class Extension
    {
        public static T FromJSON<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }
        public static string ToJSON(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
        public static Vec2f ToVec2f(this SkiaSharp.SKPoint point){
            return new Vec2f(point.X,point.Y);
        }
        public static Vector2 ToVec2f(this Vector2 point)
        {
            return new Vec2f(point.X, point.Y);
        }
    }
}
