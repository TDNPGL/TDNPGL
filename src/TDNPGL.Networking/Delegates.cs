using Catty.Core.Channel;

namespace TDNPGL.Networking
{
    public delegate void ClientDisconnect(IChannel channel, byte[] packet);
    public delegate void ClientConnect(IChannel channel);
    public delegate int ClientPing(IChannel channel, byte[] packet);
}
