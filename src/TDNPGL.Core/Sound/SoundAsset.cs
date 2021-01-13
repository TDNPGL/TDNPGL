using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TDNPGL.Core.Sound
{
    public class SoundAsset : MemoryStream
    {
        private byte[] buffer;
        public SoundAsset(byte[] buffer) : base(buffer)
        {
            this.buffer = buffer;
        }
        public MemoryStream AsStream()
        {
            return new MemoryStream(buffer);
        }
    }
}
