using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using NetGears.Core.Extensions;

namespace NetGears.Core.Network
{
    public static class PacketFactory
    {
        /// <summary>
        /// PacketReferences dictionary contains as keys the unique Id of the packet
        /// PacketReferences dictionary contains as values the packet size and its type
        /// </summary>
        private static readonly Dictionary<object, Tuple<int, Type>> PacketReferences = new Dictionary<object, Tuple<int, Type>>(); 

        /// <summary>
        /// Add to PacketReferences every packet in the same assembly as TPacketBase
        /// </summary>
        /// <typeparam name="TPacketBase">TPacketBase is used to find packets in the same assembly</typeparam>
        public static void Initialize<TPacketBase>() where TPacketBase : IPacket
        {
            var packets = typeof(TPacketBase)
                .Assembly
                .GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(PacketIdAttribute), false).Length > 0);

            foreach (var packet in packets)
            {
                var idAttribute = packet.GetCustomAttribute(typeof(PacketIdAttribute)) as PacketIdAttribute;
                var lengthAttribute = packet.GetCustomAttribute(typeof(PacketLengthAttribute)) as PacketLengthAttribute;

                PacketReferences.Add(idAttribute?.Id, new Tuple<int, Type>(lengthAttribute != null ? lengthAttribute.Length : 0, packet));
            }
        }

        public static List<TPacket> Deserialize<TPacket>(byte[] buffer)
            where TPacket : class, IPacket
        {
            var result = new List<TPacket>();

            for (var i = 0; i < buffer.Length; i++)
            {
                if (i + 1 < buffer.Length)
                {
                    //Get packet unique id
                    var id = BitConverter.ToInt16(buffer, i);

                    if (PacketReferences.ContainsKey(id))
                    {
                        //Get sub buffer by using packet's size contained in PacketReferences
                        var buff = buffer.GetSubArray(i, PacketReferences[id].Item1);

                        //Increments index to the end of the packet
                        i += PacketReferences[id].Item1;

                        var packet = Activator.CreateInstance(PacketReferences[id].Item2, buff, id) as TPacket;

                        packet?.Deserialize();

                        result.Add(packet);
                    }
                }
            }

            return result;
        }
    }
}