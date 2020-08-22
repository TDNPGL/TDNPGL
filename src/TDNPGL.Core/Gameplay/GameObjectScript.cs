using System;
using System.Collections.Specialized;
using TDNPGL.Core.Gameplay.Interfaces;

namespace TDNPGL.Core.Gameplay
{
    public abstract class GameObjectScript : IUpdateable
    {
        public GameObjectScript(GameObject @object)
        {
            Parent = @object;
        }

        public IParentable Parent { get; set; }

        public virtual void OnCreate() { }

        public virtual void OnFirstTick() { }

        public virtual void OnTick(){ }

        public virtual void OnCollideWith(GameObject collide) { }
    }
}
