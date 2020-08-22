using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TDNPGL.Core.Gameplay.LowLevel;

namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IUpdateable : IParentable
    {
        public void OnTick();
        public void OnCreate();
        public void OnFirstTick();
    }
}
