using System.IO;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Math;
using TDNPGL.Core.Sound;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TDNPGL.Views.UWP
{
    public sealed class GameRenderElement : FrameworkElement, IGameRenderer, ISoundProvider
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public double width => this.Width;

        public double height => this.Height;

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width,height);

        private BaseLevelRenderer renderer = new BaseLevelRenderer();
        public ILevelRenderer LevelRenderer => renderer;

        public void Dispose()
        {
        }

        public void DrawBitmap(SkiaSharp.SKBitmap bitmap)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="sync">Not working</param>
        public void PlaySound(SoundAsset asset, bool sync=false)
        {
            IRandomAccessStream randomAccessStream = asset.AsStream().AsRandomAccessStream();
            this.mediaPlayer.SetStreamSource(randomAccessStream);
            mediaPlayer.Play();
        }
    }
}
