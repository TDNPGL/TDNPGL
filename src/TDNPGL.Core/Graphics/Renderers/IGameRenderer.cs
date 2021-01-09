using SkiaSharp;
using System;

namespace TDNPGL.Core.Graphics.Renderers
{
    public interface IGameRenderer : IDisposable
    {
        void RenderBitmap(SKBitmap bitmap);

        double width { get;}
        double height { get; }
        double PixelSize { get; }
        ILevelRenderer LevelRenderer { get; }
    }
}
