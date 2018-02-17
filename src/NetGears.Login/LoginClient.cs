using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using Ether.Network.Packets;

namespace NetGears.Login
{
    public class LoginClient : NetConnection
    {
        private LoginServer _loginServer { get; set; }

        public void Initialize(LoginServer loginServer)
        {
            _loginServer = loginServer;
        }

        public override void HandleMessage(NetPacketBase packet)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            _loginServer.DisconnectClient(Id);
            base.Dispose();
        }
    }
}
