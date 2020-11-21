using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Core.Gameplay.Assets
{
    public abstract class ContentFile
    {
        [JsonProperty("content_type")]
        public abstract string ContentType { get; set; }

        public ContentFile(string ContentType)
        {
            this.ContentType = ContentType;
        }
    }
}
