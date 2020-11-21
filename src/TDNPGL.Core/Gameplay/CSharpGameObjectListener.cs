using System;
using System.Collections.Specialized;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Core.Gameplay
{
    public abstract class CSharpGameObjectListener : GameObjectListener
    {
        public CSharpGameObjectListener(GameObject @object)
        {
            Parent = @object;
        }

        public override void OnCreate() { }
        public override void OnFirstTick() { }
        public override void OnTick(){ }
        public override void OnMouseReleased(int button,SkiaSharp.SKPoint point){ }
        public override void OnMouseReleasedOver(int button, SkiaSharp.SKPoint point){ }
        public override void OnCollideWith(GameObject collide) { }
    }
}
