using SkiaSharp;
using System.ComponentModel;

namespace TDNPGL.Core.GUI{
    public abstract class AbstractComponent : Component{
        public SKPoint Location;
        public SKSize Size;
        public SKRect Rect { get {
                return new SKRect(Location.X,
                    Location.Y,
                    Location.X + Size.Width,
                    Location.Y + Size.Height);
            } 
        }
        public abstract void MouseReleased(SKPoint mouseLocation);
        public abstract void Render(SKCanvas canvas, SKPoint mousePos);
        public AbstractComponent(SKPoint location,SKSize size){
            this.Location=location;
            this.Size=size;
        }
        public bool IsPointOver(SKPoint point)
        {
            if ((Location.X < point.X && Rect.Right > point.X)&&
                (Location.Y < point.Y && Rect.Top > point.Y)
                ) return true;
            if ((Location.X > point.X && Rect.Right < point.X) &&
                (Location.Y > point.Y && Rect.Top < point.Y)
                ) return true;
            return false;
        }
    }
}