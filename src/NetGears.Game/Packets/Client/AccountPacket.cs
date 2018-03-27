using System;
using System.Collections.Generic;
using System.Text;
using NetGears.Core.Network;
using NetGears.Game.Enums;

namespace NetGears.Game.Packets.Client
{
    [PacketHeader((short)LoginPacketId.ACCOUNT_PACKET, 56)]
    public class AccountPacket : PacketBase
    {
        public AccountPacket(byte[] buffer) : base(buffer) { }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public override void Deserialize()
        {
            Username = Encoding.Default.GetString(Buffer, 5, 19).Replace("\0", String.Empty);
            Password = Encoding.Default.GetString(Buffer, 22, 32).Replace("\0", String.Empty);
        }
    }
}