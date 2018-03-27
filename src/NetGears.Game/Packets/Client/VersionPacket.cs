using NetGears.Core.Network;
using NetGears.Game.Enums;

namespace NetGears.Game.Packets.Client
{
    [PacketHeader((short)LoginPacketId.VERSION_PACKET, 44)]
    public class VersionPacket : PacketBase
    {
        public VersionPacket(byte[] buffer) : base(buffer)
        {
            
        }

        public string StreetGearsVersion { get; set; }
        
        public string Lang { get; set; }

        public override void Deserialize()
        {
            StreetGearsVersion = "StreetGearsVersion";
            Lang = "Lang";
        }
    }
}
