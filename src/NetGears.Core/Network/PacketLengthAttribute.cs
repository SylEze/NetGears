using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PacketLengthAttribute : Attribute
    {
        public int Length { get; set; }

        public PacketLengthAttribute(int length)
        {
            Length = length;
        }
    }
}
