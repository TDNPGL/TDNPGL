using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Networking
{
    public enum AsyncActionResult
    {
        Ping=0,
        Connect=1,
        Level=2
    }
    public delegate void AsyncActionResultHandler(object sender, AsyncActionResult result);
}
