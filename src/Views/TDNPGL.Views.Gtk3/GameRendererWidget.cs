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
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Math;
using TDNPGL.Core.Gameplay.Interfaces;
using OpenTK.Audio.OpenAL;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace TDNPGL.Views.Gtk3
{
    public class GameRendererWidget : Widget, IGameRenderer, ISoundProvider, IGameInitializer
    {
        #region Fields
        private bool soundInitialized = false;
        public Game game{get;set;}

        public SKDrawingArea DrawingArea = new SKDrawingArea();

        public double PixelSize => ScreenCalculations.OptimalPixelSize(RenderWidth, RenderHeight);

        public double RenderWidth => this.WidthRequest;
        public double RenderHeight => this.HeightRequest;

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

        private readonly BaseLevelRenderer renderer = new BaseLevelRenderer();
        public ILevelRenderer LevelRenderer => renderer;

        private SKBitmap currentGameBitmap = new SKBitmap();
        private ALDevice device;
        private ALContext context;
        #endregion

        public void RenderBitmap(SKBitmap bitmap)
        {
            CurrentGameBitmap = bitmap;

            DrawingArea.QueueDrawArea(0, 0, (int)RenderWidth, (int)RenderHeight);
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
                e.Surface.Canvas.DrawBitmap(CurrentGameBitmap, new SKRect(0, 0, (float)RenderWidth, (float)RenderHeight));
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException)
                {
                    Logging.MessageAction("GAMEWIDGET", ex.Message + " on rendering game on canvas!", ConsoleColor.Red);
                    return;
                }
                CurrentGameBitmap.Dispose();
#if DEBUG
                throw;
#else
                Logging.WriteError(ex);
#endif
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
            if (!soundInitialized)
                InitSound();
            unsafe
            {
                void p()
                {
                    ALC.MakeContextCurrent(context);

                    //Process
                    int buffers = 0, source = 0;
                    AL.GenBuffers(1, ref buffers);
                    AL.GenSources(1, ref source);

                    int sampleFreq = 44100;

                    byte[] buffer = asset.AsStream().GetBuffer();

                    IntPtr unmanagedPointer = Marshal.AllocHGlobal(buffer.Length);
                    Marshal.Copy(buffer, 0, unmanagedPointer, buffer.Length);

                    AL.BufferData(buffers, ALFormat.Mono16, unmanagedPointer, buffer.Length, sampleFreq);
                    AL.Source(source, ALSourcei.Buffer, buffers);
                    AL.Source(source, ALSourceb.Looping, true);

                    AL.SourcePlay(source);

                    Marshal.FreeHGlobal(unmanagedPointer);
                }
                if (sync)
                    p();
                else
                    new Task(p).Start();
            }
        }
        private void InitSound()
        {
            soundInitialized = true;
            device = ALC.OpenDevice(null);
            unsafe
            {
                context = ALC.CreateContext(device, (int*)null);
            }
        }
        public void Dispose()
        {
            base.Dispose();

            if (context != ALContext.Null)
            {
                ALC.MakeContextCurrent(ALContext.Null);
                ALC.DestroyContext(context);
            }
            context = ALContext.Null;

            if (device != IntPtr.Zero)
            {
                ALC.CloseDevice(device);
            }
            device = ALDevice.Null;
        }
    }
}
