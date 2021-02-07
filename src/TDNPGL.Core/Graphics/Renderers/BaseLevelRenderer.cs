using SkiaSharp;
using System;
using TDNPGL.Core.Debug;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Core.Graphics.Renderers
{
    public class BaseLevelRenderer:ILevelRenderer
    {
        public BaseLevelRenderer()
        {
        }
        public virtual SKBitmap Render(Level level,IGameRenderer renderer,GUI.GUICanvas gcanvas=null)
        {
            SKBitmap bitmap = new SKBitmap((int)renderer.RenderWidth, (int)renderer.RenderHeight);
            SKCanvas canvas = new SKCanvas(bitmap);

            canvas.Clear(level.BackColor);
            try {
                gcanvas?.Render(canvas);
                if (level != null)
                    foreach (GameObject @object in level?.Objects)
                    {
                        @object.Render(canvas,renderer);
                    }
                else throw new ArgumentNullException("level");
            }
            catch(Exception exception)
            {
#if DEBUG
                throw;
#else
                if (exception is not InvalidOperationException)
                {
                    SKPaint textPaint = new SKPaint() { Color = SKColors.White, TextSize = ScreenSize.Width / 40 };
                    canvas.DrawText($"Level can't be rendered", new SKPoint(50, ScreenSize.Height / 2), textPaint);
                    canvas.DrawText($"Exception: {exception.GetType().FullName + ": " + exception.Message}", new SKPoint(50, (ScreenSize.Height / 2) + textPaint.TextSize), textPaint);
                    Logging.WriteError(exception);
                }
#endif
            }
            canvas.Dispose();
            return bitmap;
        }
    }
}
