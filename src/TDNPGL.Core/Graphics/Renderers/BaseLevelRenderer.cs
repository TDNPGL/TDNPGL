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
        public virtual SKBitmap Render(Level level,SKSize ScreenSize)
        {
            SKBitmap bitmap = new SKBitmap((int)ScreenSize.Width, (int)ScreenSize.Height);
            SKCanvas canvas = new SKCanvas(bitmap);

            canvas.Clear(level.BackColor);
            try {
                level?.SortObjects();
                if (level != null)
                    foreach (GameObject @object in level?.Objects)
                    {
                        @object.Render(canvas);
                    }
                else throw new ArgumentNullException("level");
            }
            catch(Exception exception)
            {
                if (exception is InvalidOperationException);
                else
                {
                    SKPaint textPaint = new SKPaint() { Color = SKColors.White, TextSize = ScreenSize.Width / 40 };
                    canvas.DrawText($"Level can't be rendered", new SKPoint(50, ScreenSize.Height / 2), textPaint);
                    canvas.DrawText($"Exception: {exception.GetType().FullName + ": " + exception.Message}", new SKPoint(50, (ScreenSize.Height / 2) + textPaint.TextSize), textPaint);
                    Logging.WriteError(exception);
                }
            }
            canvas.Dispose();
            return bitmap;
        }
    }
}
