using SkiaSharp;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        public static Level CurrentLevel { get; private set; }

        private Game() { 
        }

        public static string GameName= "Game01";
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

        public static void Init(IGameRenderer renderer,Assembly AssetsAssembly,string GameName,bool EnableCustomLogger)
        {
            if(EnableCustomLogger)
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

            GraphicsOutput.AddOutputGameRenderer(renderer);
            Logging.MessageAction("LAUNCH", "{0} is running platform",ConsoleColor.Green,ConsoleColor.Gray,Environment.OSVersion.ToString());
            Logging.MessageAction("LAUNCH", "{0} is game-renderer size", ConsoleColor.Green, ConsoleColor.Gray, GetCurrentDisplaySize());
            GraphicsOutput.BeginRender();
        }
        public static void Init<EntryType>(IGameRenderer renderer, string GameName, bool EnableCustomLogger) where EntryType : EntryPoint
        {
            Init(renderer, Assembly.GetAssembly(typeof(EntryType)), GameName, EnableCustomLogger);
        }
        #region User interact
        public static void KeyDown(ConsoleKeyInfo key)
        {
        }
        public static void KeyPress(ConsoleKeyInfo key)
        {
        }
        public static void MouseClick(int button,SKPoint point)
        {
            try{
                foreach(GameObject obj in CurrentLevel.Objects){
                    obj.OnMouseReleased(point);
                }
            }catch(Exception ex){
                Console.WriteLine(ex);
            }
        }
        #endregion
    }
}
