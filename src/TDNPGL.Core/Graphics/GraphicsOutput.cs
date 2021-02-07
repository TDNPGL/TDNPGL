//System
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
    public class GraphicsOutput
    {
        public GUI.GUICanvas Canvas;
        public Game Game{get;private set;}
        public FrameUpdateEventHandler FrameUpdate;
        public BaseLevelRenderer MainLevelRenderer { private set; get; }
        /// <summary>
        /// Main graphics thread
        /// </summary>
        private Thread MainGraphicsThread;
        /// <summary>
        /// All graphics outputs
        /// </summary>
        private List<IGameRenderer> Renderers = new List<IGameRenderer>();
        /// <summary>
        /// Add new renderer
        /// </summary>
        /// <param name="renderer"></param>
        public void AddOutputGameRenderer(IGameRenderer renderer) =>
            Renderers.Add(renderer);
        /// <summary>
        /// Begins rendering
        /// </summary>
        public GraphicsOutput(Game game, GUI.GUICanvas canvas = null){
            this.Game=game;
            Canvas = canvas;
            if (canvas == null)
                this.Canvas = new GUI.GUICanvas();
        }
        public void BeginRender()
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
                Exceptions.Call(ex);
            }
        }
        public IEnumerable<IGameRenderer> GetGameRenderers()
        {
            return Renderers;
        }
        public void PauseRender(int pauseTime)
        {
        }
        /// <summary>
        /// Rendering method(only thread)
        /// </summary>
        [HandleProcessCorruptedStateExceptions]
        private void Render()
        {
            while (true)
            {
                if (MainLevelRenderer != null)
                    foreach (IGameRenderer renderer in Renderers)
                        try
                        {
                            SKBitmap bitmap = MainLevelRenderer.Render(Game.CurrentLevel,renderer,Canvas);
                            
                            renderer.RenderBitmap(bitmap);
                            if (GC.GetTotalMemory(true) / 1024 / 1024 > 40)
                                GC.Collect();
                        }
                        catch (Exception ex)
                        {
                            Exceptions.Call(ex);
                        }
                Thread.Sleep(Options.FPS.FPSDelay);
            }
        }
        /// <summary>
        /// Stops rendering thread
        /// </summary>
        public void StopRender()
        {
            try
            {
                MainGraphicsThread?.Abort();
                Logging.MessageAction("GRAPHICS", "Rendering stopped!", ConsoleColor.Red);
                GC.Collect();
            }
            catch(Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
    }
}
