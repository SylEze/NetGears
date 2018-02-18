using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PacketBufferAttribute : Attribute
    {
        public ushort Length { get; set; }

        public ushort BeginPosition { get; set; }

        public byte Delimitor { get; set; }

        public PacketBufferAttribute(ushort length, ushort beginPosition, byte delimitor)
        {
            Length = length;
            BeginPosition = beginPosition;
            Delimitor = delimitor;
        }
    }
}
