using System.Runtime.Serialization;

namespace NetGears.Login.Misc
{
    [DataContract]
    public struct LoginConfiguration
    {
        [DataMember(Name = "host")]
        public string Host { get; set; }

        [DataMember(Name = "port")]
        public ushort Port { get; set; }
        
        [DataMember(Name = "max_connections")]
        public uint MaxConnections { get; set; }
        
        [DataMember(Name = "cluster_host")]
        public string ClusterHost { get; set; }
        
        [DataMember(Name = "cluster_port")]
        public ushort ClusterPort { get; set; }
        
        [DataMember(Name = "cluster_token")]
        public string ClusterToken { get; set; }
    }
}