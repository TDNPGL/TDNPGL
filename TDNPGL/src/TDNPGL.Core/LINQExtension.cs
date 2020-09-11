using Newtonsoft.Json;
using TDNPGL.Core;

namespace System
{
    public static class LINQExtension
    {
        public static object FromJSON(this object source,string json)
        {
            return JsonConvert.DeserializeObject<System.Object>(json);
        }
        public static string ToJSON(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
        public static Vec2f ToVec2f(this SkiaSharp.SKPoint point){
            return new Vec2f(point.X,point.Y);
        }
    }
}
