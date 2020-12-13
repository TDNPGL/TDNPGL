using SkiaSharp;
using System;
using System.Reflection;
using System.Threading;
using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Graphics;
using TDNPGL.Core.Graphics.Renderers;

namespace TDNPGL.Core
{
    public class Game
    {
        public static EntryPoint CurrentEntry;
        public static Level CurrentLevel { get; protected set; }

        private Game() { 
        }

        public static string GameName{ get; set; } = "Unnamed";
        public static PlatformID CurrentPlatform;
        public static Assembly AssetsAssembly;

        public static void SetLevel(Level level)
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
        public static void Init(GameProvider provider,Assembly AssetsAssembly,string GameName,bool EnableCustomLogger)
        {
            if (EnableCustomLogger)
                Logging.SetCustomLogger();
            Game.GameName = GameName;

            Console.WriteLine("Game initialized!");

            Game.AssetsAssembly = AssetsAssembly;

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
        }

        public static void Init<EntryType>(GameProvider provider, string GameName, bool EnableCustomLogger) where EntryType : EntryPoint
        {
            Init(provider, Assembly.GetAssembly(typeof(EntryType)), GameName, EnableCustomLogger);
        }
        #region User interact
        public static void KeyDown(ConsoleKeyInfo key)
        {
        }
        public static void KeyPress(ConsoleKeyInfo key)
        {

        }
        public static void MouseReleased(int button,SKPoint point)
        {
            try{
                foreach(GameObject obj in CurrentLevel.Objects){
                    obj.OnMouseReleased(button,point);
                }
            }catch(Exception ex){
                Exceptions.Call(ex);
            }
        }
        public static void MouseMove(int button, SKPoint point)
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
        public static void MouseDown(int button, SKPoint point)
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
        #endregion
        #region Events
        #endregion
    }
}
