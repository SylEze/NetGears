using NetGears.Core.Network;

namespace NetGears.Game.Packet
{
    public abstract class PacketBase : IPacket
    {
        public abstract void Deserialize(byte[] buffer);

        public abstract byte[] Serialize();
    }
}