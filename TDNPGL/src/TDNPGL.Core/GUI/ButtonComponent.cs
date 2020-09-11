using SkiaSharp;
using System;

namespace TDNPGL.Core.GUI
{
    public class ButtonComponent : AbstractComponent
    {
        private Action<ButtonComponent,SKPoint> OnRelease;
        public string Text;
        public SKColor Color=SKColors.White;
        protected SKPaint ButtonPaint; 
        public override void MouseReleased(SKPoint point){
            OnRelease.Invoke(this,point);
        }
        /// <summary>
        /// If you want to make more beautiful button,
        /// best way - create new class, extending ButtonComponent
        /// and override "Render"
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="mousePos"></param>
        public override void Render(SKCanvas canvas, SKPoint mousePos)
        {
            canvas.DrawRect(Rect,
                            ButtonPaint);
        }

        public ButtonComponent(SKPoint loc,SKSize size,Action<ButtonComponent, SKPoint> OnRelease):base(loc,size){
            this.OnRelease = OnRelease;
            ButtonPaint = new SKPaint() { 
                StrokeWidth = 10, 
                IsStroke = true, 
                Color = Color 
            };
        }
    }
}