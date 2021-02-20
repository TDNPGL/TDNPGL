using System;
using System.Reflection;
using glfw3;
using SkiaSharp;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay.Assets;
using TDNPGL.Core.Gameplay.Interfaces;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Sound;

namespace TDNPGL.Views.GLFW3
{
    public class GLFWRenderWindow : IGameRenderer, IGameInitializer, ISoundProvider
    {
        public GLFWRenderWindow()
        {
            if (!(Glfw.Init()==1))
                Environment.Exit(-1);
            var mon = Glfw.GetPrimaryMonitor();
            wwindow = Glfw.CreateWindow(200,100,"Test",mon,new GLFWwindow(200,100,"Test"));
            Glfw.MakeContextCurrent(wwindow);
            wwindow.Show();
        }

        private GLFWwindow wwindow;
        public Game game { get; set; }

        public double RenderWidth => wwindow.GetSize().Item1;

        public double RenderHeight => wwindow.GetSize().Item2;

        public double PixelSize => throw new NotImplementedException();

        public ILevelRenderer LevelRenderer => throw new NotImplementedException();

        public Game CreateGame(Assembly assembly, string GameName)
        {
            throw new NotImplementedException();
        }

        public Game CreateGame<EntryType>(string GameName) where EntryType : EntryPoint
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void PlaySound(SoundAsset asset, bool sync)
        {
            throw new NotImplementedException();
        }

        public void RenderBitmap(SKBitmap bitmap)
        {
            VkResult res=Glfw.CreateWindowSurface(wwindow.__Instance, IntPtr.Zero, IntPtr.Zero, 0);
            
        }
    }
}
