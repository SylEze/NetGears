using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Game.Packets
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PacketHeaderAttribute : Attribute
    {
        public short Id { get; set; }

        public int Length { get; set; }

        public PacketHeaderAttribute(short id, int length = 0)
        {
            Id = id;
            Length = length;
        }
    }
}
