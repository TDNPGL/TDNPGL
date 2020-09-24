using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TDNPGL.Core.Sound
{
    public class SoundAsset
    {
        internal byte[] buffer;
        public SoundAsset(byte[] buffer)
        {
            this.buffer = buffer;
        }
        public Stream AsStream()
        {
            return new MemoryStream(buffer);
        }
    }
}
