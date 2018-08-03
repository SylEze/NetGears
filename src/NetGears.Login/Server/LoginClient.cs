using Ether.Network.Common;
using Ether.Network.Packets;
using NetGears.Core.Logger;

namespace NetGears.Login.Server
{
    public class LoginClient : NetUser
    {
        private static readonly Logger Logger = Logger.GetLogger<LoginClient>();
        
        private LoginServer _loginServer { get; set; }

        public void Initialize(LoginServer loginServer)
        {
            _loginServer = loginServer;
        }

        public override void HandleMessage(INetPacketStream packet)
        {
        }

        public void Disconnect()
        {
            Dispose();
            _loginServer.DisconnectClient(Id);
        }
    }
}