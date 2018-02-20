using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    public interface IPacket
    {
        void Deserialize(byte[] buffer);
    }
}