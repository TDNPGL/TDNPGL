using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TDNPGL.Native.Win32.Extensions.Delegates
{
    public delegate bool IsPointOver(float x, float y, float minx, float miny, float maxx, float maxy);
}
