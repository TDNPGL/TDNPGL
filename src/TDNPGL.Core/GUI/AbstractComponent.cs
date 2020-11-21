using SkiaSharp;
using System.ComponentModel;

namespace TDNPGL.Core.GUI{
    public abstract class AbstractComponent : Component{
        public SKPoint Location;
        public SKSize Size;
        public abstract void MouseReleased(SKPoint mouseLocation);
        public virtual void Render(SKCanvas canvas,SKPoint mousePos){

        }
        public AbstractComponent(SKPoint location,SKSize size){
            this.Location=location;
            this.Size=size;
        }
    }
}