namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IUpdateable : IParentable
    {
        void OnTick();
        void OnCreate();
        void OnFirstTick();
        void OnMouseReleasedOver(int button, SkiaSharp.SKPoint point);
        void OnMouseReleased(int button,SkiaSharp.SKPoint point);
        void OnMouseMove(int button,SkiaSharp.SKPoint point);
        void OnMouseDown(int button, SkiaSharp.SKPoint point);
        void OnKeyDown(SkiaSharp.SKPoint point);
    }
}
