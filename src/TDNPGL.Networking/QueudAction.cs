namespace TDNPGL.Networking
{
    internal enum QueudAction
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
        Ping = 2
    }
}