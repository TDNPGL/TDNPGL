using SkiaSharp;
using System.Collections.Generic;

namespace TDNPGL.Core.GUI
{
    public class GUICanvas : AbstractComponent
    {
        public List<AbstractComponent> Children = new List<AbstractComponent>();
        public GUICanvas(SKPoint location, SKSize size, IEnumerable<AbstractComponent> children) : base(location, size)
        {
            if (children != null)
                Children.AddRange(children);
        }

        public override void MouseReleased(SKPoint mouseLocation)
        {
            Children.ForEach((AbstractComponent com) =>
            {
                if (com.IsPointOver(mouseLocation))
                    com.MouseReleased(mouseLocation);
            });
        }

        public override void Render(SKCanvas canvas, SKPoint mousePos)
        {
            Children.ForEach((AbstractComponent com) =>
            com.Render(canvas, mousePos)
            );
        }
    }
}
