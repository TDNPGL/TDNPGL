using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TDNPGL.Core.Math;

namespace TDNPGL.Core.Graphics.Renderers
{
    public sealed partial class BaseGameRenderer : IGameRenderer
    {
        public BaseGameRenderer(int width, int height)
        {
            this.width = width;
            this.height = height;

            renderer = new BaseLevelRenderer();
        }

        public double RenderWidth => width;
        private double width;

        public double RenderHeight => height;
        private double height;

        public double PixelSize => ScreenCalculations.OptimalPixelSize(width,height);

        public ILevelRenderer LevelRenderer=>renderer;
        private ILevelRenderer renderer;

        public SKBitmap CurrentBitmap=>sKBitmap;
        private SKBitmap sKBitmap;

        public void Dispose()
        {
            sKBitmap.Dispose();
        }

        public void RenderBitmap(SKBitmap bitmap)
        {
            sKBitmap = bitmap;
        }
    }
}
