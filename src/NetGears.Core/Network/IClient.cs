using System;
using System.Collections.Generic;
using System.Text;

namespace NetGears.Core.Network
{
    public interface IClient
    {
        Guid Id { get; }

        void Disconnect();
    }
}
