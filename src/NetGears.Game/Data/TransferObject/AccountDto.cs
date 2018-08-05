using System;
using System.Net;
using NetGears.Game.Enum;

namespace NetGears.Game.Data.TransferObject
{
    public class AccountDto
    {
        public uint Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public AccountRank Rank { get; set; }

        public DateTime LastConnectionDate { get; set; }

        public IPAddress LastConnectionIp { get; set; }

        public string RegistrationEmail { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IPAddress RegistrationIp { get; set; }
    }
}
