using Catty.Core.Buffer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TDNPGL.Core;

namespace TDNPGL.Networking.Utils
{
    public static class PacketUtils
    {
        private static Type[] types = new Type[] { };
        public static DynamicByteBuf GetByteBuf(PacketType ptype, params object[] content)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write((int)(ptype));
            foreach (object item in content)
            {
                Type type = item.GetType();
                int index = types.ToList().IndexOf(type);
                if (index >= 0)
                    if (type == typeof(float))
                        writer.Write((float)item);
                    else if (type == typeof(string))
                        writer.Write((string)item);
                    else if (type == typeof(bool))
                        writer.Write((bool)item);
                    else if (type == typeof(double))
                        writer.Write((double)item);
                        else if (type == typeof(Guid))
                        writer.Write(((Guid)item).ToString());

                    else if (type == typeof(sbyte))
                        writer.Write((sbyte)item);
                    else if (type == typeof(short))
                        writer.Write((short)item);
                    else if (type == typeof(int))
                        writer.Write((int)item);
                    else if (type == typeof(long))
                        writer.Write((long)item);

                    else if (type == typeof(byte))
                        writer.Write((byte)item);
                    else if (type == typeof(ushort))
                        writer.Write((ushort)item);
                    else if (type == typeof(uint))
                        writer.Write((uint)item);
                    else if (type == typeof(ulong))
                        writer.Write((ulong)item);
                    else writer.Write(item.ToJSON());
            }
            DynamicByteBuf buf = new DynamicByteBuf();
            buf.WriteBytes(stream.GetBuffer());
            return buf;
        }
        /// <summary>
        /// Get objects from buffer.
        /// </summary>
        /// <param name="byteBuf"></param>
        /// <param name="types">If length of 'types' more, than 255, game can crush. It's not bug. Who need to write 256 item in packet?</param>
        /// <returns></returns>
        public static object[] GetObjects(IByteBuf byteBuf, params Type[] types)
        {
            List<byte> byteList = new List<byte>();
            byteBuf.ReadBytes(byteBuf.Capacity, delegate (byte[] buf, int index, int length)
            {
                byteList.AddRange(buf);
                return 0;
            });
            MemoryStream stream = new MemoryStream(byteList.ToArray());
            BinaryReader reader = new BinaryReader(stream);

            List<object> objects = new List<object>();

            for (byte i = 0; i < types.Length; i++)
            {
                Type type = types[i];

                object value;

                if (type == typeof(float))
                    value = reader.ReadSingle();
                else if (type.IsAssignableFrom(typeof(Enum)))
                    value = reader.ReadInt16();
                else if (type == typeof(string))
                    value = reader.ReadString();
                else if (type == typeof(bool))
                    value = reader.ReadBoolean();
                else if (type == typeof(double))
                    value = reader.ReadDouble();
                else if (type == typeof(Guid))
                    value = Guid.Parse(reader.ReadString());
                #region Decimals
                #region Signed
                else if (type == typeof(sbyte))
                    value = reader.ReadSByte();
                else if (type == typeof(short))
                    value = reader.ReadInt16();
                if (type == typeof(int))
                    value = reader.ReadInt32();
                else if (type == typeof(long))
                    value = reader.ReadInt64();
                #endregion Signed
                #region Unsigned
                else if (type == typeof(byte))
                    value = reader.ReadByte();
                else if (type == typeof(ushort))
                    value = reader.ReadUInt16();
                if (type == typeof(uint))
                    value = reader.ReadUInt32();
                else if (type == typeof(ulong))
                    value = reader.ReadUInt64();
                #endregion Unsigned
                #endregion Decimals
                else
                    try
                    {
                        string json = reader.ReadString();
                        value = json.FromJSON<object>();
                    }
                    catch (Exception ex)
                    {
                        Exceptions.Call(ex);
                        value = null;
                    }
                objects.Add(value);
            }
            return objects.ToArray();
        }
    }
}
