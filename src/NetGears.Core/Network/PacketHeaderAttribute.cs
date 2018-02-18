using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PacketHeaderAttribute : Attribute
    {
        public object Header { get; }

        public PacketHeaderAttribute(object header)
        {
            Header = header;
        }
    }
}
