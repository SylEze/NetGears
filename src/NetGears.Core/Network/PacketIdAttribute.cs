using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PacketIdAttribute : Attribute
    {
        public object Id { get; }

        public PacketIdAttribute(object id)
        {
            Id = id;
        }
    }
}
