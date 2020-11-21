using Catty.Core.Bootstrap;
using Catty.Core.Buffer;
using Catty.Core.Channel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public class SingleLevelServer : NetworkLevel
    {
        public event ClientPing Ping = (IChannel channel, byte[] packet) => 1;
        public event ClientConnect Connect;
        public event ClientDisconnect Disconnect;

        private List<IChannel> channels = new List<IChannel>();
        
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
                Channels.Write(channel, PacketUtils.GetByteBuf(PacketType.Level,Level));
            }
            if (packetHeader == (byte)PacketType.Ping)
            {
                byte result = Ping.Invoke(channel, buffer);
                DynamicByteBuf buf1 = PacketUtils.GetByteBuf(PacketType.PingResult, result);
                Channels.Write(channel, buf1);
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
                    Exceptions.Call(ex);
                    DynamicByteBuf buf1 = PacketUtils.GetByteBuf(PacketType.Error, ex.Message);
                    Channels.Write(channel, buf1);
                }
            }
        }
        public override void ChannelOpen(IChannelHandlerContext ctx, IChannelStateEvent e)
        {
            base.ChannelOpen(ctx, e);
            this.channels.Add(ctx.GetChannel());
            Connect.Invoke(e.GetChannel());
        }
        public override void ChannelClosed(IChannelHandlerContext ctx, IChannelStateEvent e)
        {
            this.channels.Remove(ctx.GetChannel());
            base.ChannelClosed(ctx, e);
        }
        private SingleLevelServer(Level level) : base(level)
        {
        }
        public static SingleLevelServer CreateServer(int port, string ip, Level level)
            => CreateServer(port,IPAddress.Parse(ip), level);
        public static SingleLevelServer CreateServer(int port, IPAddress ip, Level level)
            => CreateServer(new IPEndPoint(ip, port), level);
        public static SingleLevelServer CreateServer(EndPoint endPoint, Level level)
        {
            SingleLevelServer levelServer = new SingleLevelServer(level);

            Func<IChannelHandler[]> handlersFactory = () => new IChannelHandler[] { levelServer };
            var server = new SimpleTcpService().SetHandlers(handlersFactory);
            server.Bind((IPEndPoint)endPoint);

            return levelServer;
        }
    }
}
