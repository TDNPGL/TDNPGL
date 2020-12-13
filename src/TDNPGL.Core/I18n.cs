using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;

namespace TDNPGL.Core
{
    public static class I18n
    {
        private static string locales;
        public static IReadOnlyDictionary<string, Dictionary<string, string>> Locales;
        private static string lang;
        public static Dictionary<string, string> CurrentLocale => Locales[lang];
        public static void InitI18n(Assembly assembly,string resourceName="locales.json")
        {
            string res = assembly.GetManifestResourceNames()[0];
            Stream stream = assembly.GetManifestResourceStream(res);
            ResourceSet set = new ResourceSet(stream);
            locales = (string)set.GetObject(resourceName); 
            Locales = JsonConvert.DeserializeObject<
                Dictionary<string, Dictionary<string, string>>>(locales);
        }
        public static void SetLanguage(string lang)
        {
            I18n.lang = lang;
        }
        public static string Translate(string key, params object[] args)
        {
            if(Locales.ContainsKey(lang))
                if(CurrentLocale.ContainsKey(key))
                    return String
                        .Format(CurrentLocale[key], args);
            return key;
        }
    }
}
