using Catty.Core.Buffer;
using Catty.Core.Channel;
using System;
using System.Linq;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public abstract class NetworkLevelHandler : SimpleChannelUpstreamHandler
    {
        protected Level Level { get; set; }
        protected IChannelFuture channelFuture { get; set; }

        public void UpdateObject(GameObject gameObject, string field, object value)
        {
            if (Level.Objects.ToList().IndexOf(gameObject) != -1)
            {
                Guid id = gameObject.InLevelID;
                string jsonValue = value.ToJSON();
                Type type = value.GetType();

                DynamicByteBuf packet = PacketUtils.GetByteBuf(PacketType.UpdateObject, id, field, jsonValue,type.FullName);

                Channels.Write(this.channelFuture.GetChannel(), packet);
            }
            else
                throw new InvalidOperationException("Objects array not contains given object");
        }
        public NetworkLevelHandler(Level level)
        {
            this.Level = level;
        }
        public NetworkLevelHandler()
        {
            this.Level = Level.Empty;
        }
    }
}
