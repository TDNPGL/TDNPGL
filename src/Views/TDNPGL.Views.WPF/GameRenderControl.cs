using System.Reflection;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Math;
using TDNPGL.Core.Sound;
using System.Media;
using System;

namespace TDNPGL.Views.WPF
{
    /// <summary>
    /// </summary>
    public class GameRenderControl:SKElement,IGameRenderer,ISoundProvider
    {
        static GameRenderControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameRenderControl), new FrameworkPropertyMetadata(typeof(GameRenderControl)));
        }
        public GameRenderControl()
        {
            PaintSurface += This_PaintSurface;
        }

        public double width => Width;
        public double height => Height;
        public double PixelSize => ScreenCalculations.CalculatePixelSize(width,height);

        public ILevelRenderer LevelRenderer => new BaseLevelRenderer();
        public SKBitmap OutputBitmap;
        private SoundPlayer soundPlayer = new SoundPlayer();

        public void Dispose()
        {
            OutputBitmap.Dispose();
        }
        public void DrawBitmap(SKBitmap bitmap)
        {
            InvalidateVisual();
            OutputBitmap = bitmap;
        }
        private void This_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            e.Surface.Canvas.DrawBitmap(OutputBitmap, SKPoint.Empty);
        }

        public void InitGame(Assembly assembly, string GameName)
        {
            TDNPGL.Core.Game.Init(new GameProvider(this, this), assembly, GameName, true);
        }
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public void PlaySound(SoundAsset asset,bool sync=false)
        {
            soundPlayer.Stream = asset.AsStream();
            if(sync)
                soundPlayer.Play();
            else soundPlayer.Play();
        }
    }
}
