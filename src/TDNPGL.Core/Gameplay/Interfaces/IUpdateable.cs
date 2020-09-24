using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TDNPGL.Core.Gameplay.LowLevel;

namespace TDNPGL.Core.Gameplay.Interfaces
{
    public interface IUpdateable : IParentable
    {
        void OnTick();
        void OnCreate();
        void OnFirstTick();
        void OnMouseReleased(SkiaSharp.SKPoint point);
        void OnMouseReleasedOver(SkiaSharp.SKPoint point);
    }
}
