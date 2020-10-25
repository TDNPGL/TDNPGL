using System;
using System.Linq;
using TDNPGL.Core.Gameplay.LowLevel;
using TDNPGL.Native.Win32.Extensions;

namespace NativeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AABB aABB = new AABB(-1, -1, 0, 0);
            bool b= aABB.IsPointOverNative(new SkiaSharp.SKPoint(1,1));
            System.Console.WriteLine(b);
            bool b2 = aABB.IsPointOver(new SkiaSharp.SKPoint(1, 1));
            System.Console.WriteLine(b2);
        }
    }
}
