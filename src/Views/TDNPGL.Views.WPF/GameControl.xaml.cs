using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Media;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Math;
using TDNPGL.Core.Sound;

namespace TDNPGL.Views.WPF
{
    /// <summary>
    /// Логика взаимодействия для GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl,ISoundProvider,IGameRenderer
    {
        public GameControl()
        {
            InitializeComponent();
        }

        public double width => Width;
        public double height => Height;
        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height);

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

        public void PlaySound(SoundAsset asset, bool sync = false)
        {
            soundPlayer.Stream = asset.AsStream();
            if (sync)
                soundPlayer.Play();
            else soundPlayer.Play();
        }
    }
}
