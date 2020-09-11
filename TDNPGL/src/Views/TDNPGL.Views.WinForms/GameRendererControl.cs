using SkiaSharp.Views.Desktop;
using SkiaSharp;

using System.Runtime.ExceptionServices;
using System.Reflection;
using System.Linq;
using System;
using System.Windows.Forms;

using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Math;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Sound;
using TDNPGL.Core.GUI;
using System.Media;

namespace TDNPGL.Views.WinForms
{
    public partial class GameRendererControl : SKGLControl,IGameRenderer,ISoundProvider
    {
        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height); 

        public double width => Width;
        public double height => Height;

        public bool Rendering = true; 
        
        public void InitGame(Assembly assembly,string GameName)
        {
            TDNPGL.Core.Game.Init(new Core.Gameplay.Interfaces.GameProvider(this,this), assembly,GameName,true);
        }
        public void InitGame<EntryType>(string GameName) where EntryType : EntryPoint => InitGame(Assembly.GetAssembly(typeof(EntryType)), GameName);

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

        SKPoint IGameRenderer.MousePosition => throw new NotImplementedException();

        private SKBitmap currentGameBitmap = new SKBitmap();

        public GameRendererControl()
        {
            InitializeComponent();
            this.GUICanvas = new GUICanvas(new SKPoint(), new SKSize((float)width, (float)height), null);
        }

        public void InitializeComponent()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.SizeChanged += GameRendererControl_SizeChanged;
            this.VSync = true;
            this.PaintSurface += new System.EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(this.This_PaintSurface);
            this.SizeChanged += new System.EventHandler(this.skglControl1_SizeChanged);
            this.MouseUp += GameRendererControl_MouseReleased;
        }

        private void GameRendererControl_MouseReleased(object sender, MouseEventArgs e)
        {
            SKPoint point = new SKPoint(e.X, e.Y);
            MouseButtons[] buttons = { MouseButtons.Left, MouseButtons.Middle, MouseButtons.Right };
            int b = buttons.ToList().IndexOf(e.Button);
            TDNPGL.Core.Game.MouseReleased(b,point);
        }

        private void GameRendererControl_SizeChanged(object sender, EventArgs e)
        {
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

        public void DrawBitmap(SKBitmap bitmap)
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

        public void Play(SoundAsset asset)
        {
            SoundPlayer player = new SoundPlayer(asset.GetMemoryStream());
            player.Play();
        }
    }
}
