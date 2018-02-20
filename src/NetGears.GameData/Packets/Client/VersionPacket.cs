using NetGears.Core.Network;
using NetGears.GameData.Enums;

namespace NetGears.GameData.Packets.Client
{
    [PacketId((short)LoginPacketsId.VERSION_PACKET)]
    [PacketLength(25)]
    public class VersionPacket : PacketBase
    {
        public VersionPacket(byte[] buffer, object id) : base(buffer, id)
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
