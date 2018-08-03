using System;

namespace NetGears.Core.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PacketHeaderAttribute : Attribute
    {
        public short Id { get; }

        public int Length { get; }
        
        public PacketSenderType SenderType { get; }
        
        public PacketServerType ServerType { get; }

        public PacketHeaderAttribute(
            short id, int length, PacketSenderType senderType, PacketServerType serverType)
        {
            Id = id;
            Length = length;
            SenderType = senderType;
            ServerType = serverType;
        }
    }
}