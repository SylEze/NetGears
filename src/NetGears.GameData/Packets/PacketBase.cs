using Ether.Network.Packets;
using NetGears.Core.Network;

namespace NetGears.GameData.Packets
{
    public class PacketBase : NetPacketBase, IPacket
    {
        public PacketBase(byte[] buffer, object id)
        {
            Buffer = buffer;
            Id = id;
        }

        public override byte[] Buffer { get; }

        public object Id { get; set; }

        public virtual void Deserialize()
        {
            //Do nothing
        }

        public virtual byte[] Serialize()
        {
            return new byte[0];
        }
    }
}