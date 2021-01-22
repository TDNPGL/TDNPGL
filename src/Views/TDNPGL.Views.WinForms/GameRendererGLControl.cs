using SkiaSharp.Views.Desktop;
using SkiaSharp;

using System.Runtime.ExceptionServices;
using System.Reflection;
using System.Linq;
using System;
using System.Windows.Forms;
using System.Media;

using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Math;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Sound;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core;

namespace TDNPGL.Views.WinForms
{
    public partial class GameRendererGLControl : SKGLControl, IGameRenderer, ISoundProvider, IGameInitializer
    {

        #region Fields
        public Game game{get;set;}
        public SoundPlayer SoundPlayer = new SoundPlayer();
        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height);
        public double width => Width;
        public double height => Height;

        public bool Rendering = true;

        public ILevelRenderer LevelRenderer => new BaseLevelRenderer();
        private SKBitmap currentGameBitmap = new SKBitmap();
        #endregion
        public Game CreateGame(Assembly assembly, string GameName){
            Game g=TDNPGL.Core.Game.Create(new GameProvider(this,this), assembly, GameName, true);
            this.game=g;
            return g;
        }
        public Game CreateGame<EntryType>(string GameName) where EntryType : EntryPoint =>
            CreateGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

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

        public GameRendererGLControl()
        {
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GameRendererGLControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GameRendererGLControl";
            this.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.This_PaintSurface);
            this.SizeChanged += new System.EventHandler(this.skglControl1_SizeChanged);
            this.ResumeLayout(false);
        }

        private void GameRendererControl_MouseReleased(object sender, MouseEventArgs e)
        {
            SKPoint point = new SKPoint(e.X, e.Y);
            MouseButtons[] buttons = { MouseButtons.Left, MouseButtons.Middle, MouseButtons.Right };
            int b = buttons.ToList().IndexOf(e.Button);
            game.OnMouseReleased(b, point);
        }
        [HandleProcessCorruptedStateExceptions]
        private void This_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
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
                    Logging.MessageAction("GAMECONTROL", ex.Message + " on rendering game on canvas!", ConsoleColor.Red);
                    return;
                }
                Logging.WriteError(ex);
                CurrentGameBitmap.Dispose();
            }
        }

        public void RenderBitmap(SKBitmap bitmap)
        {
            if (!Rendering)
            {
                Rendering = true;
                return;
            }

            CurrentGameBitmap = bitmap;

            Invalidate();
        }

        private void skglControl1_SizeChanged(object sender, EventArgs e)
        {

        }

        public void PlaySound(SoundAsset asset, bool sync = false)
        {
            SoundPlayer.Stream = asset.AsStream();
            SoundPlayer.Play();
        }
    }
}
