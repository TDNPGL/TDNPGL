using System;
namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IUpdateable : IParentable
    {
        public void OnTick();
        public void OnCreate();
        public void OnFirstTick();
        public void OnMouseReleasedOver(int button, SkiaSharp.SKPoint point);
        public void OnMouseReleased(int button,SkiaSharp.SKPoint point);
        public void OnMouseMove(int button,SkiaSharp.SKPoint point);
        public void OnMouseDown(int button, SkiaSharp.SKPoint point);
        public void OnKeyDown(ConsoleKeyInfo key);
    }
}
