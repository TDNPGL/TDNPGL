using Catty.Core.Bootstrap;
using Catty.Core.Buffer;
using Catty.Core.Channel;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public class SingleLevelServer : SimpleChannelUpstreamHandler
    {
        public event ClientPing Ping = (IChannel channel, byte[] packet) => 1;
        public event ClientConnect Connect;
        public event ClientDisconnect Disconnect;

        public Level Level { get; private set; }
        private IChannel channel;

        public override void MessageReceived(IChannelHandlerContext ctx, IMessageEvent e)
        {
            object msg = e.GetMessage();
            DynamicByteBuf buf = (DynamicByteBuf)msg;
            byte[] buffer = buf.GetByteArray();
            IChannel channel = e.GetChannel();
            BinaryReader reader = new BinaryReader(new MemoryStream(buffer));
            byte packetHeader = reader.ReadByte();
            if (packetHeader == (byte)PacketType.GetLevel)
            {
                Channels.Write(channel, Level);
            }
            if (packetHeader == (byte)PacketType.Ping)
            {
                Channels.Write(channel, Ping.Invoke(channel, buffer));
            }
            if (packetHeader == (byte)PacketType.Disconnect)
            {
                Disconnect.Invoke(channel, buffer);
            }
            if (packetHeader == (byte)PacketType.UpdateObject)
            {
                try
                {
                    Type gameObjectType = typeof(GameObject);
                    int objectId = reader.ReadInt32();
                    string fieldName = reader.ReadString();
                    FieldInfo field = gameObjectType.GetField(fieldName);
                    object value = JsonConvert.DeserializeObject(reader.ReadString(), field.FieldType);
                    field.SetValue(Level.GetObject(objectId), value);
                }
                catch(Exception ex)
                {
                    DynamicByteBuf buf1 = PacketUtils.GetByteBuf(PacketType.Error, ex.Message);
                    Channels.Write(channel, buf1);
                }
            }
        }
        public override void ChannelOpen(IChannelHandlerContext ctx, IChannelStateEvent e)
        {
            base.ChannelOpen(ctx, e);
            this.channel = e.GetChannel();
            Connect.Invoke(e.GetChannel());
        }
        public SingleLevelServer(int port, IPAddress ip, Level level)
        {
            Func<IChannelHandler[]> handlersFactory = () => new IChannelHandler[] { this };
            var server = new SimpleTcpService().SetHandlers(handlersFactory);
            server.Bind(new IPEndPoint(ip, port));
            this.Level = level;
        }
    }
}
