using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;

namespace TDNPGL.Core
{
    public class I18n
    {
        private string locales;
        public IReadOnlyDictionary<string, Dictionary<string, string>> Locales;
        private string lang;
        public Dictionary<string, string> CurrentLocale => Locales[lang];
        public I18n(Assembly assembly,string resourceName="locales.json")
        {
            string res = assembly.GetManifestResourceNames()[0];
            Stream stream = assembly.GetManifestResourceStream(res);
            ResourceSet set = new ResourceSet(stream);
            locales = (string)set.GetObject(resourceName); 
            Locales = JsonConvert.DeserializeObject<
                Dictionary<string, Dictionary<string, string>>>(locales);
        }
        public void SetLanguage(string lang)
        {
            this.lang = lang;
        }
        public string Translate(string key, params object[] args)
        {
            if(Locales.ContainsKey(lang))
                if(CurrentLocale.ContainsKey(key))
                    return String
                        .Format(CurrentLocale[key], args);
            return key;
        }
    }
}
