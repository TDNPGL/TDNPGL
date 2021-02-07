using SkiaSharp;
using System;

namespace TDNPGL.Core.Graphics.Renderers
{
    public interface IGameRenderer : IDisposable
    {
        void RenderBitmap(SKBitmap bitmap);

        double RenderWidth { get;}
        double RenderHeight { get; }
        double PixelSize { get; }
        ILevelRenderer LevelRenderer { get; }
    }
}
