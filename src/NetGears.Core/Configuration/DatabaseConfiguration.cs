using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NetGears.Core.Configuration
{
    [DataContract]
    public class DatabaseConfiguration
    {
        [DataMember(Name = "host")]
        public string Host { get; set; }

        [DataMember(Name = "databaseName")]
        public string DatabaseName { get; set; }
    }
}
