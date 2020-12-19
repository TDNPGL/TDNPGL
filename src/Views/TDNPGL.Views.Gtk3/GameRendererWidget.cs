﻿using TDNPGL.Core.Debug;
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
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Math;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Views.Gtk3
{
    public class GameRendererWidget : Widget, IGameRenderer, ISoundProvider, IGameInitializer
    {
        #region Fields
        public Game game{get;set;}

        public SKDrawingArea DrawingArea = new SKDrawingArea();

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
        #endregion
        private readonly BaseLevelRenderer renderer = new BaseLevelRenderer();
        public ILevelRenderer LevelRenderer => renderer;

        private SKBitmap currentGameBitmap = new SKBitmap();

        public void DrawBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;

            DrawingArea.QueueDrawArea(0, 0, (int)width, (int)height);
        }
        public GameRendererWidget()
        {
            DrawingArea.PaintSurface += SKWidget_PaintSurface;
        }
        protected override bool OnButtonReleaseEvent(EventButton evnt)
        {
            SKPoint point = new SKPoint((float)evnt.X, (float)evnt.Y);
            int b = (int)evnt.Button;
            game.OnMouseReleased(b, point);
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

        public Game CreateGame(Assembly assembly, string GameName){
            Game g=TDNPGL.Core.Game.Create(new GameProvider(this,this), assembly, GameName, true);
            this.game=g;
            return g;
        }
        public Game CreateGame<EntryType>(string GameName) where EntryType : EntryPoint => CreateGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

        public void PlaySound(SoundAsset asset, bool sync)
        {
            if (sync)
                throw new InvalidOperationException();
        }
    }
}
