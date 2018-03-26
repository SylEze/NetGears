using Ether.Network.Packets;
using NetGears.Core.Network;
using System;

namespace NetGears.Game.Packets
{
    public class PacketBase : NetPacketBase
    {
        public PacketBase(byte[] buffer)
        {
            Buffer = buffer;
        }

        public override byte[] Buffer { get; }

        public virtual void Deserialize() { }

        public virtual byte[] Serialize()
        {
            return new byte[0];
        }
    }
}