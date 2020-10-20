//Systen
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
//TDNPGL
using TDNPGL.Core.Debug;
using TDNPGL.Core.Graphics.Handlers;
using TDNPGL.Core.Graphics.Renderers;

namespace TDNPGL.Core.Graphics
{
    /// <summary>
    /// Graphics output class
    /// </summary>
    public static class GraphicsOutput
    {
        public static FrameUpdateEventHandler FrameUpdate;
        public static BaseLevelRenderer MainLevelRenderer { private set; get; }
        /// <summary>
        /// Main graphics thread
        /// </summary>
        private static Thread MainGraphicsThread;
        /// <summary>
        /// All graphics outputs
        /// </summary>
        private static List<IGameRenderer> Renderers = new List<IGameRenderer>();
        /// <summary>
        /// Add new renderer
        /// </summary>
        /// <param name="renderer"></param>
        public static void AddOutputGameRenderer(IGameRenderer renderer) =>
            Renderers.Add(renderer);
        /// <summary>
        /// Begins rendering
        /// </summary>
        public static void BeginRender()
        {
            try
            {
                MainGraphicsThread = new Thread(Render) { Name = "MainGraphicsThread" };
                MainGraphicsThread.Start();

                MainLevelRenderer = new BaseLevelRenderer();

                Logging.MessageAction("GRAPHICS", "Begin graphics rendering at {0} renderers in {1}!", ConsoleColor.Green, ConsoleColor.Gray, Renderers.Count, MainGraphicsThread.Name);
            }
            catch (Exception ex)
            {
                Logging.WriteError(ex);
            }
        }
        public static IGameRenderer GetMainRenderer()
        {
            return Renderers[0];
        }
        public static void PauseRender(int pauseTime)
        {
        }
        /// <summary>
        /// Rendering method(only thread)
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        private static void Render()
        {
            while (true)
            {
                if (MainLevelRenderer != null)
                    foreach (IGameRenderer renderer in Renderers)
                        try
                        {
                            SKBitmap bitmap = MainLevelRenderer.Render(TDNPGL.Core.Game.CurrentLevel, new SKSize((float)renderer.width, (float)renderer.height));
                            renderer.DrawBitmap(bitmap);
                            if (GC.GetTotalMemory(true) / 1024 / 1024 > 40)
                                GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            Logging.WriteError(ex);
                        }
                Thread.Sleep(Options.FPS.FPSDelay);
            }
        }
        /// <summary>
        /// Stops rendering thread
        /// </summary>
        public static void StopRender()
        {
            try
            {
                MainGraphicsThread?.Abort();
                Logging.MessageAction("GRAPHICS", "Rendering stopped!", ConsoleColor.Red);
                GC.Collect();
            }
            catch
            {

            }
        }
    }
}
