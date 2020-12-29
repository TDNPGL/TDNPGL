using SkiaSharp;
using System;
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
        private static Game current;
        public EntryPoint CurrentEntry;
        public Level CurrentLevel { get; protected set; }
        public GraphicsOutput GraphicsOutput{get;protected set;}

        private Game(GameProvider provider,Assembly assetsAssembly,string gameName,bool enableCustomLogger) { 
            this.GraphicsOutput=new GraphicsOutput(this);
            if (enableCustomLogger)
                Logging.SetCustomLogger();
            GameName = gameName;

            Console.WriteLine("Game initialized!");

            AssetsAssembly = assetsAssembly;

            CurrentPlatform = Environment.OSVersion.Platform;

            CurrentEntry = AssetLoader.GetEntry(AssetsAssembly);
            Sprite.LoadSprites();
            CurrentEntry.RunMainLevel();

            Logging.MessageAction("LAUNCH", "Waiting objects loading...", ConsoleColor.Green, ConsoleColor.Gray);

            while (true)
            {
                if (CurrentLevel.IsObjectsLoaded()) 
                    break;
                Thread.Sleep(10);
            }

            GraphicsOutput.AddOutputGameRenderer(provider.Renderer);
            Logging.MessageAction("LAUNCH", "{0} is running platform",ConsoleColor.Green,ConsoleColor.Gray,Environment.OSVersion.ToString());
            Logging.MessageAction("LAUNCH", "{0} is game-renderer size", ConsoleColor.Green, ConsoleColor.Gray, GetCurrentDisplaySize());
            GraphicsOutput.BeginRender();

            current=this;
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

        public static SKSize GetCurrentDisplaySize()
        {
            SKSize size;

            IGameRenderer renderer = GraphicsOutput.GetMainRenderer();

            double height=renderer.height;
            double width= renderer.width;
            size = new SKSize((float)width, (float)height);
            return size;
        }
        public static Game Init(GameProvider provider,Assembly assetsAssembly,string gameName,bool enableCustomLogger)
            => new Game(provider,assetsAssembly,gameName,enableCustomLogger);

        public static Game Init<EntryType>(GameProvider provider, string GameName, bool EnableCustomLogger) where EntryType : EntryPoint
            => Init(provider, Assembly.GetAssembly(typeof(EntryType)), GameName, EnableCustomLogger);
        #region User interact
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
        public void OnMouseReleased(int button,SKPoint point)
        {
            try{
                foreach(GameObject obj in CurrentLevel.Objects){
                    obj.OnMouseReleased(button,point);
                }
            }catch(Exception ex){
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
                    obj.OnMouseDown(button,point);
                }
            }
            catch (Exception ex)
            {
                Exceptions.Call(ex);
            }
        }
        public static Game GetInstance(){
            return current;
        }
        public void OnTick(){}
        public void OnStart(){}
        public void OnFirstTick(){}
        public void OnMouseReleasedOver(int button, SKPoint point){}
        #endregion
        #region Events
        #endregion
    }
}
