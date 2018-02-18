using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using Ether.Network.Packets;
using NetGears.Core.Logger;

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
            Logger.Debug($"{packet} sent by {Id}");
        }

        public void Disconnect()
        {
            base.Dispose();
            _loginServer.DisconnectClient(Id);
        }
    }
}
