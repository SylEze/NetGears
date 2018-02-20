using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    public interface IPacket
    {
        object Id { get; set; }

        byte[] Buffer { get; }

        /// <summary>
        /// Deserialize an instance of packet with a new buffer and an id
        /// </summary>
        void Deserialize();

        byte[] Serialize();
    }
}