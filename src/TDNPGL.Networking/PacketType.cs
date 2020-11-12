using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Networking
{
    public enum PacketType
    {
        None = 0,
        GetLevel = 1,
        Ping = 2,
        Disconnect = 3,
        UpdateObject = 4,
        Custom = 5,
        Error = 6
    }
}
