using Catty.Core.Bootstrap;
using Catty.Core.Buffer;
using Catty.Core.Channel;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public class Client : NetworkLevel
    {
        #region Fields
        private QueudAction action = QueudAction.None;
        private ClientBootstrap client;
        private EndPoint endPoint;
        private IChannelFuture channelFuture;

        private sbyte pingResult = -1;
        #endregion
        #region Constructors
        public Client(EndPoint endPoint)
        {
            Func<IChannelHandler[]> handlersFactory = () => new IChannelHandler[] { this };
            client = new ClientBootstrap();

            client.SetPipelineFactory(handlersFactory);

            this.endPoint = endPoint;
        }
        public Client(int port, IPAddress ip) : this(new IPEndPoint(ip, port)) { }
        public Client(int port, string ip) : this(port, IPAddress.Parse(ip)) { }
        #endregion
        #region Methods
        /// <summary>
        /// Better use it async, because i don't know, when server return anything
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<sbyte> Ping(params object[] args)
        {
            if (channelFuture == null)
                throw new NotImplementedException("Client not connected. Run Client.Connect() for fix it");
            Channels.Write(channelFuture.GetChannel(),
                           PacketUtils.GetByteBuf(PacketType.Ping, args));
            action = QueudAction.Ping;
            while (pingResult < 0) ;
            return pingResult;
        }
        /// <summary>
        /// Better use it async, because i don't know, when server return anything
        /// </summary>
        /// <returns></returns>
        public void LoadLevel()
        {
            Channels.Write(channelFuture.GetChannel(), PacketUtils.GetByteBuf(PacketType.GetLevel));
            action = QueudAction.GetLevel;
        }
        public IChannelFuture Connect()
        {
            this.channelFuture = client.Connect(endPoint);
            return channelFuture;
        }
        public override void MessageReceived(IChannelHandlerContext ctx, IMessageEvent e)
        {
            switch (action)
            {
                case QueudAction.Ping:
                    object[] objects = PacketUtils.GetObjects((IByteBuf)e.GetMessage(),
                                                              typeof(PacketType),
                                                              typeof(byte),
                                                              typeof(byte));
                    pingResult = (sbyte)objects[2];
                    break;
                case QueudAction.None:
                    break;
                case QueudAction.GetLevel:
                    object[] objects1 = PacketUtils.GetObjects((IByteBuf)e.GetMessage(),
                                                               typeof(PacketType),
                                                               typeof(byte),
                                                               typeof(Level));
                    this.Level = (Level)objects1[2];
                    break;
                default:
                    break;
            }
            object[] objects2 = PacketUtils.GetObjects((IByteBuf)e.GetMessage(),
                                                       typeof(PacketType),
                                                       typeof(int),
                                                       typeof(string),
                                                       typeof(string),
                                                       typeof(string));
            try
            {
                int id = (int)objects2[1];
                string field = (string)objects2[2];
                string value = (string)objects2[3];
                string type = (string)objects2[4];
                Type type1 = Type.GetType(type);
                object valueObj = JsonConvert.DeserializeObject(value, type1);
                GameObject gameObject = Level.GetObject(id);
                type1.GetField(field).SetValue(gameObject, valueObj);
            }
            catch(Exception ex)
            {
                Exceptions.Call(ex);
            }

            action = QueudAction.None;
        }
        #endregion
    }
}
