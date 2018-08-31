using System.Text;

using NetGears.Core.Network;
using NetGears.Game.Enum;

namespace NetGears.Game.Packet.Login.Client
{
    [PacketHeader((short)LoginPacketId.ACCOUNT_PACKET, 56, PacketSenderType.CLIENT, PacketServerType.LOGIN)]
    public class AccountPacket : PacketBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public override void Deserialize(byte[] buffer)
        {
            Username = Encoding.Default.GetString(buffer, 0, 19).Replace("\0", "");
            Password = Encoding.Default.GetString(buffer, 19, 32).Replace("\0", "");
        }

        public override byte[] Serialize()
        {
            throw new System.NotImplementedException();
        }
    }
}