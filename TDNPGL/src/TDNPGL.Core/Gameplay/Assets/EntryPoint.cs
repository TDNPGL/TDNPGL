﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TDNPGL.Core.Debug.Exceptions;

namespace TDNPGL.Core.Gameplay.Assets
{
    public class EntryPoint : ContentFile
    {
        [JsonProperty("scripts")]
        public Dictionary<string, string> Scripts = new Dictionary<string, string>();
        [JsonProperty("entry_class_name")]
        public string Name;
        [JsonProperty("namespace")] 
        public string Namespace;
        [JsonIgnore]
        public Assembly currentAssembly => Assembly.GetAssembly(GetType());
        [JsonProperty("auto_load_level")]
        public string AutoLoadLevel;
        [JsonProperty("content_type")]
        public override string ContentType { get; set; }

        public void RunMainLevel()
        {
            Console.WriteLine("Running level: " + AutoLoadLevel);
            Game.SetLevel((Level)AssetLoader.GetAsset<Level>(AutoLoadLevel,Game.AssetsAssembly));
        }
        public EntryPoint(string name) : base("entry")
        {
            Name = name;
        }
        public static T FromJSON<T>(string json) where T : EntryPoint
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public Type GetScript(string name)
        {
            Type type = Game.AssetsAssembly.GetType(Scripts[name]);
            if (type == null)
                throw new AssetsException("Script not found!");
            return type;
        }
    }
}
