using System;
using System.Collections.Generic;
using System.Text;
using NetGears.Core.Network;
using NetGears.GameData.Enums;

namespace NetGears.GameData.Packets.Client
{
    [PacketId((short)LoginPacketsId.ACCOUNT_PACKET)]
    [PacketLength(55)]
    public class AccountPacket : PacketBase
    {
        public AccountPacket(byte[] buffer, object id) : base(buffer, id) { }

        public string Username { get; set; }

        public string Password { get; set; }

        public override void Deserialize()
        {
            Username = Encoding.Default.GetString(Buffer, 3, 19);
            Password = Encoding.Default.GetString(Buffer, 22, 32);
        }
    }
}
