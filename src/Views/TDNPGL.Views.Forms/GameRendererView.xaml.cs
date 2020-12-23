using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using Xamarin.Forms.Xaml;
using TDNPGL.Core.Sound;
using TDNPGL.Core.Gameplay;
using Plugin.SimpleAudioPlayer;
using TDNPGL.Core.Math;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core;

namespace TDNPGL.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
#pragma warning disable CA1063
    public partial class GameRendererView : SKGLView,IGameRenderer, ISoundProvider, IGameInitializer
#pragma warning restore CA1063
    {
        #region Fields
        public Game game{get;set;}
        public double width => CanvasSize.Width;
        public double height => CanvasSize.Height;
        public SKBitmap CurrentGameBitmap { get; set; }
        private BaseLevelRenderer renderer = new BaseLevelRenderer();
        public ILevelRenderer LevelRenderer => renderer;

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width,height);
        #endregion
        public GameRendererView()
        {
            InitializeComponent();
            this.Touch+=TouchEvent;
        }
        public Game CreateGame(Assembly assembly, string GameName){
            Game g=TDNPGL.Core.Game.Create(new GameProvider(this,this), assembly, GameName, true);
            this.game=g;
            return g;
        }
        public Game CreateGame<EntryType>(string GameName) where EntryType : EntryPoint => CreateGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public void Dispose() => CurrentGameBitmap.Dispose();

        public void TouchEvent(object sender,SKTouchEventArgs args){
            game.OnMouseReleased(((int)args.MouseButton)-1,args.Location);
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