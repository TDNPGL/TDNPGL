using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TDNPGL.Core.Sound;
using TDNPGL.Core.GUI;

namespace TDNPGL.Views.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameRendererView : SKGLView,IGameRenderer,ISoundProvider
    {
        public double width => CanvasSize.Width;
        public double height => CanvasSize.Height;

        public double PixelSize => ((width + height) / 2) / (Math.PI * 100);
        public GameRendererView()
        {
            InitializeComponent();
            this.Touch+=TouchEvent;
            this.GUICanvas = new GUICanvas(new SKPoint(), new SKSize((float)width, (float)height),null);
        }
        public void InitGame(Assembly assembly, string GameName)=>
            TDNPGL.Core.Game.Init(new Core.Gameplay.Interfaces.GameProvider(this,this), assembly, GameName, true);
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public SKBitmap CurrentGameBitmap { get; set; }

        public override IDispatcher Dispatcher => base.Dispatcher;

        public GUICanvas GUICanvas { get; set; }

        public SKPoint MousePosition=>Mpoint;
        private SKPoint Mpoint;

        public void Dispose()
        {
            
        }

        public void TouchEvent(object sender,SKTouchEventArgs args){
            TDNPGL.Core.Game.MouseReleased(((int)args.MouseButton)-1,args.Location);
            Mpoint = args.Location;
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

        public void Play(SoundAsset asset)
        {
            throw new NotImplementedException();
        }
    }
}