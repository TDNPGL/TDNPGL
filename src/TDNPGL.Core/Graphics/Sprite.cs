using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using TDNPGL.Core.Gameplay.Assets;

namespace TDNPGL.Core.Graphics
{
    public class Sprite
    {
        public SpriteManifest Manifest { get; private set; }
        public SKBitmap OriginalBitmap { get; private set; }
        public SKBitmap[] Frames { get; private set; }

        public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public static void LoadSprites()
        {
            Dictionary<string, SKBitmap> bitmaps = AssetLoader.LoadAssetsFrom<SKBitmap>(Game.GetInstance().AssetsAssembly);
            Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
            foreach (KeyValuePair<string, SKBitmap> pair in bitmaps)
            {
                SpriteManifest manifest = (SpriteManifest)AssetLoader.GetAsset<SpriteManifest>(pair.Key + "_manifest", Game.GetInstance().AssetsAssembly);
                Sprite sprite = new Sprite(pair.Value,manifest);
                sprites.Add(pair.Key, sprite);
            }
            Sprites = sprites;
            bitmaps.Clear();
        }
        public static Stream GetSpriteAssetStream(string name)
        {
            Assembly assembly = Game.GetInstance().AssetsAssembly;
            string[] names = assembly.GetManifestResourceNames();
            ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(names[0]));
            object obj = set.GetObject(name);
            Stream stream = new MemoryStream(obj as byte[]);
            return stream;
        }
        public static SKStream GetSpriteAssetSKStream(string name)
        {
            Assembly assembly = Game.GetInstance().AssetsAssembly;
            string[] names = assembly.GetManifestResourceNames();
            ResourceSet set = new ResourceSet(assembly.GetManifestResourceStream(names[0]));
            object obj = set.GetObject(name);
            SKStream stream = new SKMemoryStream(obj as byte[]);
            return stream;
        }
        public Sprite(SKBitmap original, SpriteManifest manifest)
        {
            OriginalBitmap = original;
            if(manifest.XFrameSize<=0)
                Frames = new SKBitmap[] { original };
            else
            {
                List<SKBitmap> frames = new List<SKBitmap>();
                SKImage originalImage = SKImage.FromBitmap(original);
                int FramesCount = original.Width / manifest.XFrameSize;

                for(int i = 0; i < FramesCount; i++)
                {
                    SKRectI rect = SKRectI.Create(manifest.XFrameSize * i, 0, manifest.XFrameSize, original.Height);
                    SKImage frameImg = originalImage.Subset(rect);
                    SKBitmap frame = SKBitmap.FromImage(frameImg);
                    frames.Add(frame);
                }
                this.Frames = frames.ToArray();
            }
        }
        public Sprite(SKBitmap bitmap):this(bitmap,new SpriteManifest())
        {
        }
    }
}
