using NetGears.Core.Network;
using NetGears.Game.Enum;
using System.Text;

namespace NetGears.Game.Packet.Login.Client
{
    [PacketHeader((short)LoginPacketId.VERSION_PACKET, 44, PacketSenderType.CLIENT, PacketServerType.LOGIN)]
    public class VersionPacket : PacketBase
    {
        public string StreetGearsVersion { get; set; }
        
        public string Lang { get; set; }

        public override void Deserialize(byte[] buffer)
        {
            StreetGearsVersion = Encoding.Default.GetString(buffer, 0, 20).Replace("\0", "");
            Lang = Encoding.Default.GetString(buffer, 20, 3).Replace("\0", "");
        }

        public override byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}