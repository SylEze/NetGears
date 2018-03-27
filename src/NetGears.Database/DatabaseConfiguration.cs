using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NetGears.Database
{
    [DataContract]
    public class DatabaseConfiguration
    {
        [DataMember(Name = "databaseProvider")]
        public DatabaseProvider DatabaseProvider { get; set; }
        
        [DataMember(Name = "host")]
        public string Host { get; set; }
        
        [DataMember(Name = "user")]
        public string User { get; set; }
        
        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "databaseName")]
        public string DatabaseName { get; set; }
    }
}
