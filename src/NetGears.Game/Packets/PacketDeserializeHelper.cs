using System;
using System.Collections.Generic;
using NetGears.Core.Extensions;
using NetGears.Core.Logger;
using NetGears.Core.Misc;

namespace NetGears.Game.Packets
{
    public static class PacketDeserializeHelper
    {
        public static List<PacketBase> DeserializeBuffer(byte[] buffer, Dictionary<PacketHeaderAttribute, Type> packetTypes)
        {
            var result = new List<PacketBase>();

            var bufferIndex = 0;

            deserialize:

            //A StreetGears packet shouldn't have a length inferior than 5
            if (buffer.Length - bufferIndex < 5)
            {
                return result;
            }

            var length = BitConverter.ToInt16(buffer, bufferIndex);
            var id = BitConverter.ToInt16(buffer, bufferIndex + 2);
            var hash = (int) buffer[bufferIndex + 4];

            if (buffer.Length < bufferIndex + length)
            {
                return result;
            }

            var buff = buffer.GetSubArray(bufferIndex, length);
            var packet = GetPacket(id, buff, packetTypes);

            bufferIndex += length;

            packet?.Deserialize();

            result.Add(packet);

            if (bufferIndex < buffer.Length)
            {
                goto deserialize;
            }

            return result;
        }

        private static PacketBase GetPacket(int id, byte[] buffer, Dictionary<PacketHeaderAttribute, Type> packetTypes)
        {
            foreach (var packetType in packetTypes)
            {
                if (packetType.Key.Id == id)
                {
                    var result =  InstanceHelper.CreateInstanceOf<PacketBase>(packetType.Value, buffer);
                    return result;
                }
            }
            return null;
        }
    }
}
