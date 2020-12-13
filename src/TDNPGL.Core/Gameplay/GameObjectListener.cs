using System;
using SkiaSharp;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Core.Gameplay
{
    public abstract class GameObjectListener : IUpdateable
    {
        public IParentable Parent { get; set; }

        public abstract void OnCreate();
        public abstract void OnFirstTick();
        public abstract void OnMouseReleased(int button, SKPoint point);
        public abstract void OnMouseReleasedOver(int button, SKPoint point);
        public abstract void OnTick();
        public abstract void OnCollideWith(GameObject collide);
        public abstract void OnMouseMove(int button,SKPoint point);
        public abstract void OnMouseDown(int button,SKPoint point);
        public abstract void OnKeyDown(ConsoleKeyInfo point);
    }
}
