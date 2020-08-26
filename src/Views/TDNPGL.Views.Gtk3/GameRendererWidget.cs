using TDNPGL.Core.Debug;
using TDNPGL.Core.Graphics.Renderers;
using Gtk;
using System;
using SkiaSharp.Views.Gtk;
using SkiaSharp;
using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;
using Gdk;
using TDNPGL.Core;
using TDNPGL.Core.Sound;

namespace TDNPGL.Views.Gtk3
{
    public class GameRendererWidget : Widget,IGameRenderer,ISoundProvider
    {
        public SKDrawingArea DrawingArea = new SKDrawingArea();

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
        private SKBitmap currentGameBitmap = new SKBitmap();

        [Obsolete]
        public void DrawBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;

            DrawingArea.QueueDrawArea(0,0,(int)width, (int)height);
        }
        public GameRendererWidget()
        {
            DrawingArea.PaintSurface += SKWidget_PaintSurface;
        }
        protected override bool OnButtonReleaseEvent(EventButton evnt)
        {
            SKPoint point = new SKPoint((float)evnt.X, (float)evnt.Y);
            int b = (int)evnt.Button;
            Game.MouseReleased(b, point);
            return base.OnButtonReleaseEvent(evnt);
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

        public void InitGame(Assembly assembly,string GameName)
        {
            TDNPGL.Core.Game.Init(new Core.Gameplay.Interfaces.GameProvider(this,this), assembly,GameName,true);
        }
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);
    }
}
