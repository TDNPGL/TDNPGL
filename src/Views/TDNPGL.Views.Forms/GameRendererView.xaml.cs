using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TDNPGL.Core.Sound;
using TDNPGL.Core.Gameplay;
using Plugin.SimpleAudioPlayer;
using TDNPGL.Core.Math;

namespace TDNPGL.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
#pragma warning disable CA1063
    public partial class GameRendererView : SKGLView,IGameRenderer,ISoundProvider
#pragma warning restore CA1063
    {
        public double width => CanvasSize.Width;
        public double height => CanvasSize.Height;

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width,height);
        public GameRendererView()
        {
            InitializeComponent();
            this.Touch+=TouchEvent;
        }
        public void InitGame(Assembly assembly, string GameName)=>
            TDNPGL.Core.Game.Init(new GameProvider(this,this), assembly, GameName, true);
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public SKBitmap CurrentGameBitmap { get; set; }
        public ILevelRenderer LevelRenderer => new BaseLevelRenderer();

        public void Dispose() => CurrentGameBitmap.Dispose();

        public void TouchEvent(object sender,SKTouchEventArgs args){
            TDNPGL.Core.Game.MouseReleased(((int)args.MouseButton)-1,args.Location);
        }

        public void DrawBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;
            InvalidateSurface();
        }

        private void SKGLView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintGLSurfaceEventArgs e)
        {
            try
            {
                e.Surface.Canvas.DrawBitmap(CurrentGameBitmap, new SKRect(0, 0, CanvasSize.Width, CanvasSize.Height));
            }
            catch { 
            }
        }

        public void PlaySound(SoundAsset asset, bool sync=false)
        {
            var stream = asset.AsStream();
            var audio = CrossSimpleAudioPlayer.Current;
            audio.Load(stream);
            if (sync)
                throw new InvalidOperationException();
            audio.Play();
        }
    }
}