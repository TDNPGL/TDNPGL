using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TDNPGL.Core.Sound
{
    public class SoundAsset : MemoryStream
    {
        public SoundAsset(byte[] buffer) : base(buffer)
        {
        }
    }
}
