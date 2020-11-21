using Newtonsoft.Json;
using TDNPGL.Core.Gameplay.Assets;

namespace TDNPGL.Core.Graphics
{
    public class SpriteManifest : ContentFile
    {
        [JsonProperty("content_type")]
        public override string ContentType { get; set; }

        internal SpriteManifest(int XSize) : base("sprite")
        {
            XFrameSize = XSize;
        }
        public SpriteManifest() : base("sprite") { }

        [JsonProperty("x_size")]
        public int XFrameSize=0;
    }
}
