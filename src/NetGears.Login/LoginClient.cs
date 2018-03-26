using System.Reflection;
using Ether.Network;
using Ether.Network.Packets;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.Game.Packets;

namespace NetGears.Login
{
    public class LoginClient : NetConnection
    {
        public LoginServer LoginServer { get; set; }

        public void Initialize(LoginServer loginServer)
        {
            LoginServer = loginServer;
        }

        public override void HandleMessage(NetPacketBase packet)
        {
            var deserializedPacket = packet as PacketBase;

            PacketHandler<LoginClient, PacketBase>.Invoke(this, deserializedPacket);
        }

        public void Disconnect()
        {
            Dispose();
            LoginServer.DisconnectClient(Id);
        }
    }
}