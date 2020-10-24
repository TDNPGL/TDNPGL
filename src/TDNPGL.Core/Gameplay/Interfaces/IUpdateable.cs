namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IUpdateable : IParentable
    {
        void OnTick();
        void OnCreate();
        void OnFirstTick();
        void OnMouseReleased(SkiaSharp.SKPoint point);
        void OnMouseMove(SkiaSharp.SKPoint point);
        void OnKeyDown(SkiaSharp.SKPoint point);
        void OnMouseReleasedOver(SkiaSharp.SKPoint point);
    }
}
