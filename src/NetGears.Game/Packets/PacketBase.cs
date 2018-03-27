using Ether.Network.Packets;
using NetGears.Core.Network;
using System;

namespace NetGears.Game.Packets
{
    public abstract class PacketBase : NetPacketBase
    {
        protected PacketBase(byte[] buffer)
        {
            Buffer = buffer;
        }

        public short Id { get; set; }

        public short Length { get; set; }

        public byte Hash { get; set; }

        public override byte[] Buffer { get; }

        public virtual void Deserialize() { }

        public virtual byte[] Serialize()
        {
            return new byte[0];
        }
    }
}