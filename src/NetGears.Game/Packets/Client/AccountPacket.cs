using System;
using System.Collections.Generic;
using System.Text;
using NetGears.Core.Network;
using NetGears.Game.Enums;

namespace NetGears.Game.Packets.Client
{
    [PacketHeader((short)LoginPacketId.ACCOUNT_PACKET, 55)]
    public class AccountPacket : PacketBase
    {
        public AccountPacket(byte[] buffer) : base(buffer) { }
        
        public string Username { get; set; }

        public string Password { get; set; }

        public override void Deserialize()
        {
            Username = Encoding.Default.GetString(Buffer, 3, 19);
            Password = Encoding.Default.GetString(Buffer, 22, 32);
        }
    }
}