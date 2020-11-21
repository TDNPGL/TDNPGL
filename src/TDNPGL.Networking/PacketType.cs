using System;
using System.Collections.Generic;
using System.Text;

namespace TDNPGL.Networking
{
    public enum PacketType
    {
        /// <summary>
        /// Just none
        /// </summary>
        None = 0,
        /// <summary>
        /// Returns level as JSON
        /// </summary>
        GetLevel = 1,
        /// <summary>
        /// Pings server for check connection.
        /// Returns setted value on SingleLevelServer.ClientPing
        /// </summary>
        Ping = 2,
        /// <summary>
        /// Message about disconnect
        /// </summary>
        Disconnect = 3,
        /// <summary>
        /// Updates object.
        /// Format: 
        ///     (Int32) id, (String) fieldName, (String) jsonValue
        /// </summary>
        UpdateObject = 4,
        /// <summary>
        /// Your custom message
        /// </summary>
        Custom = 5,
        /// <summary>
        /// Any error
        /// </summary>
        Error = 6,
        /// <summary>
        /// Result of ping
        /// Format: (Int32)result
        /// </summary>
        PingResult = 7,
        /// <summary>
        /// Any level.
        /// Format: (String)jsonLevel
        /// </summary>
        Level = 8
    }
}
