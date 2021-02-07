using SkiaSharp;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core.Graphics;
using TDNPGL.Core.Graphics.Renderers;

namespace TDNPGL.Core
{
    public class Game : IUpdateable
    {
        internal GameProvider provider;
        public EntryPoint CurrentEntry;
        public Level CurrentLevel { get; protected set; }
        public GraphicsOutput GraphicsOutput { get; protected set; }

        private Game(GameProvider provider, Assembly assetsAssembly, string gameName, bool enableCustomLogger) : this(
            provider
            ,AssetLoader.GetEntry(assetsAssembly)
            ,gameName
            ,enableCustomLogger) { }

        private Game(GameProvider provider, EntryPoint entry, string gameName, bool enableCustomLogger)
        {
            this.provider = provider;

            CurrentPlatform = Environment.OSVersion.Platform;

            this.GraphicsOutput = new GraphicsOutput(this);
            if (enableCustomLogger)
                Logging.SetCustomLogger();
            GameName = gameName;

            AssetsAssembly = entry.CurrentAssembly;

            CurrentEntry = entry;
        }

        public string GameName{ get; set; } = "Unnamed";
        public IParentable Parent { get => null; set {} }

        public PlatformID CurrentPlatform;
        public Assembly AssetsAssembly;

        public void SetLevel(Level level)
        {
            CurrentLevel = level;
            CurrentLevel.BeginUpdate();
            CurrentLevel.ReloadObjectsIDs();
            Logging.MessageAction("LEVEL", "Level setted: {0}", ConsoleColor.Green, ConsoleColor.Gray,CurrentLevel.Name);
        }
        public void Run()
        {
            GraphicsOutput.AddOutputGameRenderer(provider.Renderer);

            Sprite.LoadSprites();
            CurrentEntry.RunMainLevel();

            Logging.MessageAction("RUN", "Loading objects...", ConsoleColor.Green, ConsoleColor.Gray);

            while (true)
            {
                if (CurrentLevel.IsObjectsLoaded())
                    break;
                Thread.Sleep(10);
            }

            GraphicsOutput.AddOutputGameRenderer(this.provider.Renderer);
            Logging.MessageAction("RUN", "{0} is running platform", ConsoleColor.Green, ConsoleColor.Gray, Environment.OSVersion.ToString());
            Logging.MessageAction("RUN", "{0} is game renderer size", ConsoleColor.Green, ConsoleColor.Gray, GetGameRendererSize(0));
            GraphicsOutput.BeginRender();

            current = this;
        }
        #region Static
        private static Game current;
        public SKSize GetGameRendererSize(int id)
        {
            SKSize size;

            IGameRenderer renderer = GraphicsOutput.GetGameRenderers().ToArray()[id];

            double height=renderer.RenderHeight;
            double width= renderer.RenderWidth;
            size = new SKSize((float)width, (float)height);
            return size;
        }
        public static Game GetInstance()
        {
            return current;
        }
        #region Create
        public static Game Create(GameProvider provider,Assembly assetsAssembly,string gameName,bool enableCustomLogger)
            => new Game(provider,assetsAssembly,gameName,enableCustomLogger);
        public static Game Create(GameProvider provider, EntryPoint entry, string gameName, bool enableCustomLogger)
            => new Game(provider, entry, gameName, enableCustomLogger);
        public static Game Create<EntryType>(GameProvider provider, string GameName, bool EnableCustomLogger) where EntryType : EntryPoint
            => Create(provider, Assembly.GetAssembly(typeof(EntryType)), GameName, EnableCustomLogger);
        #endregion Create
        #endregion Static
        #region Events
        public void OnTick() { }
        public void OnStart() { }
        public void OnFirstTick() { }
        public void OnMouseReleasedOver(int button, SKPoint point) { }
        void IUpdateable.OnKeyDown(ConsoleKeyInfo key)
        {
            try
            {
                foreach (GameObject obj in CurrentLevel.Objects)
                {
                    obj.OnKeyDown(key);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        public void OnMouseReleased(int button, SKPoint point)
        {
            try
            {
                foreach (GameObject obj in CurrentLevel.Objects)
                {
                    obj.OnMouseReleased(button, point);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        public void OnMouseMove(int button, SKPoint point)
        {
            try
            {
                foreach (GameObject obj in CurrentLevel.Objects)
                {
                    obj.OnMouseMove(button, point);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        public void OnMouseDown(int button, SKPoint point)
        {
            try
            {
                foreach (GameObject obj in CurrentLevel.Objects)
                {
                    obj.OnMouseDown(button, point);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        #endregion
    }
}
