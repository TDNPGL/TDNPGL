using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.NativeLoader
{
    public abstract class NativeLibrary : IDisposable
    {
        public abstract T GetDelegate<T>(string name) where T : Delegate;

        public abstract void Dispose();

        protected NativeLibrary(string file, IntPtr lib, int flags) { }
    }
}
