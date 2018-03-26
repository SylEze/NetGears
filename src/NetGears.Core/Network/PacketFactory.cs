using NetGears.Core.Extensions;
using NetGears.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetGears.Game.Packets
{
    public static class PacketFactory<TPacketBase, TPacketHeader>
        where TPacketBase : class
        where TPacketHeader : class
    {
        /// <summary>
        /// PacketReferences dictionary contains as key PacketHeader of the packet.
        /// PacketReferences dictionary contains as value a packet type.
        /// Each packet should have a PacketHeaderAttribute.
        /// </summary>
        private static readonly Dictionary<TPacketHeader, Type> PacketTypes = new Dictionary<TPacketHeader, Type>();

        public delegate IList<TPacketBase> DeserializeMethod(byte[] buffer, Dictionary<TPacketHeader, Type> packetTypes);

        /// <summary>
        /// Add to PacketReferences every packet in the same assembly as TPacketBase
        /// </summary>
        /// <typeparam name="TPacketBase">TPacketBase is a reference used to find packets in the same assembly</typeparam>
        public static void Initialize()
        {
            var packets = typeof(TPacketBase)
                .Assembly
                .GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(TPacketHeader), false).Length > 0)
                .ToList();

            foreach (var packet in packets)
            {
                var idAttribute = packet.GetCustomAttribute(typeof(TPacketHeader));

                PacketTypes.Add(idAttribute as TPacketHeader, packet);
            }
        }

        public static IList<TPacketBase> Deserialize(byte[] buffer, DeserializeMethod deserializeMethod)
        {
            return deserializeMethod(buffer, PacketTypes);
        }
    }
}
