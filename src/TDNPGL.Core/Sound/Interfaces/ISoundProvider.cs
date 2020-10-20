namespace TDNPGL.Core.Sound{
    public interface ISoundProvider
    {
        public void PlaySound(SoundAsset asset, bool sync);
    }
}