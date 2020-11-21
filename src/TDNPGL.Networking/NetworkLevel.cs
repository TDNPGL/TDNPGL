using Catty.Core.Buffer;
using Catty.Core.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDNPGL.Core;
using TDNPGL.Core.Gameplay;
using TDNPGL.Networking.Utils;

namespace TDNPGL.Networking
{
    public abstract class NetworkLevel : SimpleChannelUpstreamHandler
    {
        protected Level Level { get; set; }
        private IChannelFuture channelFuture { get; set; }

        public void UpdateObject(GameObject gameObject, string field, object value)
        {
            if (Level.Objects.ToList().IndexOf(gameObject) != -1)
            {
                int id = gameObject.LevelID;
                string jsonValue = value.ToJSON();
                Type type = value.GetType();

                DynamicByteBuf packet = PacketUtils.GetByteBuf(PacketType.UpdateObject, id, field, jsonValue,type.FullName);

                Channels.Write(this.channelFuture.GetChannel(), packet);
            }
            else
                throw new InvalidOperationException("Objects array not contains given object");
        }
        public NetworkLevel(Level level)
        {
            this.Level = level;
        }
        public NetworkLevel()
        {
            this.Level = Level.Empty;
        }
    }
}
