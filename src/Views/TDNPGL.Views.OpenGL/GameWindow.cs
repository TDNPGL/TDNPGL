using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Math;
using TDNPGL.Core.Sound;

namespace TDNPGL.Views.OpenGL
{
    public class GameWindow : OpenTK.Windowing.Desktop.GameWindow, IGameRenderer, ISoundProvider
    {
        public int VertexBufferObject;
        private SKBitmap bitmap = new SKBitmap(10, 10);
        private int textureId = 0;

        public double width => 800;
        public double height => 600;

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height);

        private BaseLevelRenderer renderer = new BaseLevelRenderer();
        public ILevelRenderer LevelRenderer => renderer;

        public GameWindow(SKBitmap bitmap) : base(new GameWindowSettings() { IsMultiThreaded = true }, new NativeWindowSettings() { })
        {
            this.bitmap = bitmap;
            this.RenderFrame += GameWindowsClient_RenderFrame;
        }

        private void GameWindowsClient_RenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f  //Top vertex
            };

            SwapBuffers();
            base.OnRenderFrame(e);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }
        protected override void OnUnload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload();
        }
        protected override void OnLoad()
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            base.OnLoad();
        }

        public void DrawBitmap(SKBitmap bitmap)
        {
            if (textureId != 0)
                GL.DeleteTexture(textureId);
            this.bitmap = bitmap;
        }

        public void PlaySound(SoundAsset asset, bool sync)
        {
        }
    }
}
