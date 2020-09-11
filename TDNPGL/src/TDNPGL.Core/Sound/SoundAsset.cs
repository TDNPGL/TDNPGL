using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TDNPGL.Core.Sound
{
    public class SoundAsset : IDisposable
    {
        public byte[] Buffer { get; private set; }
        public SoundAsset(byte[] bytes)
        {
            Buffer = bytes;
        }
        public void Play()
        {
            Game.Provider.SoundProvider.Play(this);
        }

        public MemoryStream GetMemoryStream() => new MemoryStream(Buffer);

        public void Dispose()
        {
            Buffer = new byte[0];
            GC.Collect();
        }
    }
}
