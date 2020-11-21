using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IParentable
    {
        public IParentable Parent { get; set; }
    }
}
