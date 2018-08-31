using Ether.Network.Packets;
using System;

using NetGears.Core.Logger;

namespace NetGears.Login.Server
{
    public class LoginPacketProcessor : IPacketProcessor
    {
        public static readonly Logger Logger = Logger.GetLogger<LoginPacketProcessor>();

        public int HeaderSize => 5;

        public bool IncludeHeader => true;

        public INetPacketStream CreatePacket(byte[] buffer)
        {
            return new NetPacket(buffer);
        }

        public int GetMessageLength(byte[] buffer)
        {
            var length = BitConverter.ToInt16(buffer, 0);

            return length;
        }
    }
}