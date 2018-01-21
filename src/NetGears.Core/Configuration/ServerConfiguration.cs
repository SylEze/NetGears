using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace NetGears.Core.Configuration
{
    [DataContract]
    public class ServerConfiguration
    {
        [DataMember(Name = "port")]
        public ushort Port { get; set; }

        [DataMember(Name = "host")]
        public string Host { get; set; }
    }
}
