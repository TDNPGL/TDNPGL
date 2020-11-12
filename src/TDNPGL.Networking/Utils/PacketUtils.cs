using Catty.Core.Buffer;
using Catty.Core.Channel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TDNPGL.Core.Gameplay;

namespace TDNPGL.Networking.Utils
{
    public static class PacketUtils
    {
        public static DynamicByteBuf GetByteBuf(PacketType ptype,params object[] content)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((int)(ptype));
            foreach (object item in content)
            {
                Type type = item.GetType();
                if (type == typeof(int))
                    writer.Write((int)item);
                else if (type == typeof(float))
                    writer.Write((float)item);
                else if (type == typeof(string))
                    writer.Write((string)item);
                else if (type == typeof(bool))
                    writer.Write((bool)item);
                else if (type == typeof(double))
                    writer.Write((double)item);
                else if (type == typeof(short))
                    writer.Write((short)item);
                else if (type == typeof(long))
                    writer.Write((long)item);
                else writer.Write(item.ToJSON());
            }
            DynamicByteBuf buf = new DynamicByteBuf();
            buf.WriteBytes(stream.GetBuffer());
            return buf;
        }
    }
}
