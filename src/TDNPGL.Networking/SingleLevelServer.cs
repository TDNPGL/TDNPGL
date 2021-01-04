using Catty.Core.Bootstrap;
using Catty.Core.Buffer;
using Catty.Core.Channel;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public class SingleLevelServer : NetworkLevelHandler, IHost
    {
        public event ClientPing Ping = (IChannel channel, byte[] packet) => 1;
        public event ClientConnect Connect;
        public event ClientDisconnect Disconnect;

        private IChannelHandler[] handlersFactory() => new IChannelHandler[] { this };
        private SimpleTcpService server;
        public EndPoint EndPoint { get; private set; }

        private readonly List<IChannel> channels = new List<IChannel>();

        public IServiceProvider Services => null;

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
                Channels.Write(channel, PacketUtils.GetByteBuf(PacketType.Level, Level));
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
                catch (Exception ex)
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
        private SingleLevelServer(EndPoint endPoint, Level level) : base(level)
        {
            server = new SimpleTcpService().SetHandlers(handlersFactory);
            EndPoint = endPoint;
        }
        public SingleLevelServer(int port, IPAddress ip, Level level) : this(new IPEndPoint(ip, port), level) { }
        public SingleLevelServer(int port, string ip, Level level) : this(port, IPAddress.Parse(ip), level) { }

        public Task StartAsync(CancellationToken cancellationToken = default)
        {
            return new Task(() =>
            {
                server.Bind((IPEndPoint)EndPoint);
            });
        }

        public Task StopAsync(CancellationToken cancellationToken = default)
        {
            return new Task(() =>
            {
                channels.ForEach(x => x.Close());
            });
        }

        public void Dispose()
        {
            StopAsync();
            Level.Updater.Stop();
            Level.Objects.ToList().ForEach(x =>
            {
                x.StopAnimation();
                x.Sprite.Frames.ToList().ForEach(y =>
                y.Dispose()
                );
            });
        }
    }
}
