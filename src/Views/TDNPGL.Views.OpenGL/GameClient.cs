using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using SkiaSharp;
using System;
using TDNPGL.Core.Graphics.Renderers;
using TDNPGL.Core.Math;
using TDNPGL.Core.Sound;
using TDNPGL.Views.OpenGL.Utils;

namespace TDNPGL.Views.OpenGL
{
    public class GameClient : GameWindow, IGameRenderer, ISoundProvider
    {
        public int VertexBufferObject;
        private SKBitmap bitmap1=new SKBitmap(10,10);
        private int textureId=0;

        public double width => 800;
        public double height => 600;

        public double PixelSize => ScreenCalculations.CalculatePixelSize(width, height);

        public ILevelRenderer LevelRenderer => throw new NotImplementedException();

        public GameClient(SKBitmap bitmap) : base(new GameWindowSettings() { IsMultiThreaded = true }, new NativeWindowSettings() { })
        {
            this.bitmap1 = bitmap;
            this.RenderFrame += GameWindowsClient_RenderFrame;
        }

        private void GameWindowsClient_RenderFrame(FrameEventArgs e)
        {
            Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";

            Color4 backColor = Color4.LightGray;
            GL.ClearColor(backColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Begin(PrimitiveType.Quads);

            GL.Color3(255, 255, 255);

            GL.Vertex2(0, 0);
            GL.Vertex2(1, 0);
            GL.Vertex2(1, -1);
            GL.Vertex2(0, -1);

            GL.End();
            SwapBuffers();
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
            base.OnLoad();
        }

        public void DrawBitmap(SKBitmap bitmap)
        {
            if (textureId != 0)
                GL.DeleteTexture(textureId);
            this.bitmap1 = bitmap;
            this.textureId = BitmapUtils.LoadTexture(bitmap);
        }

        public void PlaySound(SoundAsset asset, bool sync)
        {
        }
    }
}
