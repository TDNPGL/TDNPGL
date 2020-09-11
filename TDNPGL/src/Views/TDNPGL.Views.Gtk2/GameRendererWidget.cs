using TDNPGL.Core.Debug;
using TDNPGL.Core.Graphics.Renderers;
using Gtk;
using System;
using SkiaSharp.Views.Gtk;
using SkiaSharp;
using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Sound;
using TDNPGL.Core.GUI;

namespace TDNPGL.Views.Gtk2
{
    public class GameRendererWidget : Widget,IGameRenderer,ISoundProvider
    {
        public SKWidget SKWidget = new SKWidget();

        public double PixelSize => ((width + height) / 2) / (314);

        public double width => this.WidthRequest;
        public double height => this.HeightRequest;

        public SKBitmap CurrentGameBitmap
        {
            get
            {
                return currentGameBitmap;
            }
            set
            {
                currentGameBitmap.Dispose();
                currentGameBitmap = value;
            }
        }

        public GUICanvas GUICanvas { get; set; }

        public SKPoint MousePosition { get {
                int x, y;
                Gdk.Display.Default.GetPointer(out x,out y);
                return new SKPoint(x, y);
            } 
        }

        private SKBitmap currentGameBitmap = new SKBitmap();

        [Obsolete]
        public void DrawBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;

            SKWidget.Draw(new Gdk.Rectangle(0,0,SKWidget.WidthRequest,SKWidget.HeightRequest));
        }

        protected override void OnSizeRequested(ref Requisition requisition)
        {
            base.OnSizeRequested(ref requisition);
            SKWidget.WidthRequest = requisition.Width;
            SKWidget.HeightRequest = requisition.Height;
        }

        public GameRendererWidget()
        {
            SKWidget.PaintSurface += SKWidget_PaintSurface;
            this.GUICanvas = new GUICanvas(new SKPoint(), new SKSize((float)width, (float)height), null);
        }

        private void SKWidget_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            try
            {
                e.Surface.Canvas.Clear(SKColors.Black);
                e.Surface.Canvas.DrawBitmap(CurrentGameBitmap, new SKRect(0, 0, (float)width, (float)height));
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException)
                {
                    Logging.MessageAction("GAMEWIDGET", ex.Message + " on rendering game on canvas!", ConsoleColor.Red);
                    return;
                }
                Logging.WriteError(ex);
                CurrentGameBitmap.Dispose();
            }
        }
    
        protected override bool OnButtonReleaseEvent(Gdk.EventButton evnt){
            TDNPGL.Core.Game.MouseReleased((int)evnt.Button,new SKPoint((float)evnt.X,(float)evnt.Y));
            return base.OnButtonReleaseEvent(evnt);
        }
        public void InitGame(Assembly assembly,string GameName)
        {
            TDNPGL.Core.Game.Init(new Core.Gameplay.Interfaces.GameProvider(this,this), assembly,GameName,true);
        }
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public void Play(SoundAsset asset)
        {
            throw new NotImplementedException();
        }
    }
}
