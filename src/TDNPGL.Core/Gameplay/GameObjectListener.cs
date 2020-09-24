using SkiaSharp;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Core.Gameplay
{
    public abstract class GameObjectListener : IUpdateable
    {
        public IParentable Parent { get; set; }

        public abstract void OnCreate();
        public abstract void OnFirstTick();
        public abstract void OnMouseReleased(SKPoint point);
        public abstract void OnMouseReleasedOver(SKPoint point);
        public abstract void OnTick();
        public abstract void OnCollideWith(GameObject collide);
    }
}
