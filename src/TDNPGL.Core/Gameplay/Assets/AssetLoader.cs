using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay;
using SkiaSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;

using Newtonsoft.Json;
using System.Net;
using System.Linq;
using TDNPGL.Core.Graphics;
using TDNPGL.Core.Debug.Exceptions;

namespace TDNPGL.Core.Gameplay.Assets
{
    public static class AssetLoader
    {
        public static Type[] SupportedAssetType { get; private set; } = {
            typeof(SKBitmap), 
            typeof(string), 
            typeof(byte[]), 
            typeof(Level),
            typeof(EntryPoint),
            typeof(ContentFile),
            typeof(SpriteManifest)
        };
        public static EntryPoint GetEntry(Assembly assembly)
        {
            EntryPoint entry;
            try
            {
                string[] names = assembly.GetManifestResourceNames();
                ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(names[0]));
                string json;
                json = (string)set.GetObject("assets_entry");

                entry = EntryPoint.FromJSON<EntryPoint>(json);
            }
            catch(Exception ex)
            {
                entry = null;
                Logging.WriteError(ex);
            }
            return entry;
        }
        public static Dictionary<string,T> LoadAssetsFrom<T>(Assembly assembly)
        {
            Dictionary<string, T> assets = new Dictionary<string, T>();;
            string[] names = assembly.GetManifestResourceNames();
            ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(names[0]));
            foreach (DictionaryEntry resource in set)
            {
                Logging.SetConsoleColor(ConsoleColor.Yellow);
                if (resource.Value is T||(typeof(T)==typeof(SKBitmap)&&resource.Value.GetType()==typeof(byte[])) || (typeof(T).IsAssignableFrom(typeof(ContentFile)) && resource.Value.GetType() == typeof(string)))
                    try
                    {
                        Logging.MessageAction("ASSETS", "Loading asset \"{0}\" with type {1}...", ConsoleColor.Cyan, ConsoleColor.Gray, resource.Key, resource.Value);
                        object obj = resource.Value;
                        //There may be exception
                        bool add=true;
                        object Asset=new object();
                        if (typeof(T) == typeof(SKBitmap))
                        {
                            Asset = SKBitmap.Decode(obj as byte[]);
                        }
                        else if (typeof(T) == typeof(string))
                        {
                            Asset = obj as string;
                        }
                        else if (typeof(T) == typeof(Level))
                        {
                            string level = Encoding.UTF8.GetString(obj as byte[]);
                            Level lvl = level.FromJSON<Level>();
                            if (lvl.ContentType == "level")
                                Asset = lvl;
                            else add = false;
                        }
                        else if (typeof(T) == typeof(byte[]))
                        {
                            Asset = obj as byte[];
                        }
                        else if (typeof(T) == typeof(SpriteManifest))
                        {
                            ContentFile file = (obj as string).FromJSON<SpriteManifest>();
                            if (file.ContentType == "sprite")
                                Asset = file;
                            else add = false;
                        }
                        else if (typeof(T) == typeof(ContentFile))
                        {
                            Asset = (obj as string).FromJSON<ContentFile>();
                        }
                        else throw new ArgumentException("Unsupported asset type!");
                        if(add)
                        assets.Add((string)resource.Key, (T)Asset);
                    }
                    catch (Exception ex)
                    {
                        Logging.WriteError(ex);
                    }
            }
            Console.ResetColor();
            return assets;
        }
        public static object GetAsset<T>(string name,Assembly assembly)
        {
            if (SupportedAssetType.Contains(typeof(T)))
            {
                string[] names = assembly.GetManifestResourceNames();
                ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(names[0])); ;
                if (set == null)
                    throw new AssetsException("Assembly hasn't resources!");
                Dictionary<string, object> Assets = new Dictionary<string, object>();
                foreach (DictionaryEntry entry in set)
                {
                    Assets.Add((string)entry.Key, entry.Value);
                }

                Console.WriteLine("Trying to get assets: "+name);

                object obj = Assets[name];

                switch (SupportedAssetType.ToList().IndexOf(typeof(T)))
                {
                    case 0:
                        return SKBitmap.Decode(obj as byte[]);
                    case 1:
                        return obj;
                    case 2:
                        return obj;
                    case 3:
                        return (obj as string).FromJSON<Level>();
                    case 4:
                        return (obj as string).FromJSON<EntryPoint>();
                    case 5:
                        return (obj as string).FromJSON<ContentFile>();
                    case 6:
                        return (obj as string).FromJSON<SpriteManifest>();
                    default:
                        throw new ArgumentException();
                }
            }
            else
                throw new ArgumentException();
        }
    }
}
