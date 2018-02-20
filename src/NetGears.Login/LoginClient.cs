using System.Reflection;
using Ether.Network;
using Ether.Network.Packets;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.GameData.Packets;

namespace NetGears.Login
{
    public class LoginClient : NetConnection, IClient
    {
        public LoginServer LoginServer { get; set; }

        public void Initialize(LoginServer loginServer)
        {
            LoginServer = loginServer;
        }

        public override void HandleMessage(NetPacketBase packet)
        {
            var deserializedPacket = packet as PacketBase;

            PacketHandler.Invoke(deserializedPacket?.Id, this, deserializedPacket);
        }

        public void Disconnect()
        {
            Dispose();
            LoginServer.DisconnectClient(Id);
        }
    }
}