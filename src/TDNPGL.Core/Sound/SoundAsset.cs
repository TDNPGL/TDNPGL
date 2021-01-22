using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TDNPGL.Core.Sound
{
    public class SoundAsset
    {
        private byte[] buffer;
        public SoundAsset(byte[] buffer)
        {
            this.buffer = buffer;
        }
        public MemoryStream AsStream()
        {
            return new MemoryStream(buffer);
        }
    }
}
