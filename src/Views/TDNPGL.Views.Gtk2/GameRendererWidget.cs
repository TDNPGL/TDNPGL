using TDNPGL.Core.Debug;
using TDNPGL.Core.Graphics.Renderers;
using Gtk;
using System;
using SkiaSharp.Views.Gtk;
using SkiaSharp;
using System.Reflection;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Sound;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Math;
using TDNPGL.Core;

namespace TDNPGL.Views.Gtk2
{
    public class GameRendererWidget : Widget,IGameRenderer,ISoundProvider
    {
        public SKWidget SKWidget = new SKWidget();

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height);
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

        public ILevelRenderer LevelRenderer => new BaseLevelRenderer();

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

        public unsafe GameRendererWidget()
        {
            SKWidget.PaintSurface += SKWidget_PaintSurface;
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
            Game.MouseReleased((int)evnt.Button,new SKPoint((float)evnt.X,(float)evnt.Y));
            return base.OnButtonReleaseEvent(evnt);
        }
        public void InitGame(Assembly assembly,string GameName)
        {
            Game.Init(new GameProvider(this,this), assembly,GameName,true);
        }
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public unsafe void PlaySound(SoundAsset asset,bool sync)
        {
            
        }
    }
}
