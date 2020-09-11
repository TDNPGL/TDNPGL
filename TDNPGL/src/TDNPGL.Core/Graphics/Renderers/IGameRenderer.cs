using SkiaSharp;
using System;
using TDNPGL.Core.GUI;

namespace TDNPGL.Core.Graphics.Renderers
{
    public interface IGameRenderer : IDisposable
    {
        void DrawBitmap(SKBitmap bitmap);

        SKBitmap CurrentGameBitmap { get; set; }
        double width { get;}
        double height { get; }
        double PixelSize { get; }
        public GUICanvas GUICanvas { get; set; }
        public SKPoint MousePosition { get; }
    }
}
