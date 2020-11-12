using Catty.Core.Buffer;
using Catty.Core.Channel;
using System.IO;

namespace TDNPGL.Networking
{
    public class Client : SimpleChannelUpstreamHandler
    {
        public override void MessageReceived(IChannelHandlerContext ctx, IMessageEvent e)
        {
        }
    }
}
