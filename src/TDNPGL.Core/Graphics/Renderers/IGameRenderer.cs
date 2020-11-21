using SkiaSharp;
using System;

namespace TDNPGL.Core.Graphics.Renderers
{
    public interface IGameRenderer : IDisposable
    {
        void DrawBitmap(SKBitmap bitmap);

        double width { get;}
        double height { get; }
        double PixelSize { get; }
        ILevelRenderer LevelRenderer { get; }
    }
}
