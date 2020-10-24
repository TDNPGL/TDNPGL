using SkiaSharp;
using System.Collections.Generic;
using System.ComponentModel;
using TDNPGL.Core.GUI;

namespace TDNPGL.Core.Graphics.Renderers
{
    public class GUICanvas
    {
        private SKPoint mousePosition = new SKPoint();
        public List<AbstractComponent> Components;
        public void Render(SKCanvas canvas)
        {
            foreach(AbstractComponent component in Components)
            {
                component.Render(canvas,mousePosition);
            }
        }
    }
}
