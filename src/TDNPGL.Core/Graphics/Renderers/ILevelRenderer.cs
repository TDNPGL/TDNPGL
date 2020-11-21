using SkiaSharp;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Core.Graphics.Renderers
{
    public interface ILevelRenderer
    {
        public SKBitmap Render(Level level, SKSize ScreenSize);
    }
}
