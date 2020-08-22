using Newtonsoft.Json;

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
    }
}
