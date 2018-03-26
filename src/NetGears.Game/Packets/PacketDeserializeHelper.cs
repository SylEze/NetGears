using System;
using System.Collections.Generic;
using NetGears.Core.Extensions;
using NetGears.Core.Misc;

namespace NetGears.Game.Packets
{
    public static class PacketDeserializeHelper
    {
        public static List<PacketBase> DeserializeBuffer(byte[] buffer, Dictionary<PacketHeaderAttribute, Type> packetTypes)
        {
            var result = new List<PacketBase>();

            for (var i = 0; i < buffer.Length; i++)
            {
                if (i + 1 < buffer.Length)
                {
                    //Get packet unique id
                    var id = BitConverter.ToInt16(buffer, i);

                    var packet = GetPacket(id, ref buffer, ref i, ref packetTypes);

                    if (packet != null)
                    {
                        packet?.Deserialize();

                        result.Add(packet);
                    }
                }
            }

            return result;
        }

        private static PacketBase GetPacket(short id, ref byte[] buffer, ref int index, ref Dictionary<PacketHeaderAttribute, Type> packetTypes)
        {
            foreach (var packetType in packetTypes)
            {
                if (packetType.Key.Id == id)
                {
                    var result =  InstanceHelper.CreateInstanceOf<PacketBase>(packetType.Value, buffer.GetSubArray(index, packetType.Key.Length));
                    index += packetType.Key.Length;
                    return result;
                }
            }
            return null;
        }
    }
}
