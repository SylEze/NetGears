using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.ORM.Entities
{
    public class Account
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
