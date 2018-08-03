using NetGears.Core.Network;
using NetGears.Game.Enum;

namespace NetGears.Game.Packet.Login.Client
{
    [PacketHeader((short)LoginPacketId.VERSION_PACKET, 44, PacketSenderType.CLIENT, PacketServerType.LOGIN)]
    public class VersionPacket : PacketBase
    {
        public string StreetGearsVersion { get; set; }
        
        public string Lang { get; set; }

        public override void Deserialize(byte[] buffer)
        {
            StreetGearsVersion = "StreetGearsVersion";
            Lang = "Lang";
        }

        public override byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}
