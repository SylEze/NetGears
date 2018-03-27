using Ether.Network;
using Ether.Network.Packets;
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

            if (packet == null)
                return;

            PacketHandler<LoginClient, PacketBase>.ExecuteHandler(this, deserializedPacket);
        }

        public void Disconnect()
        {
            Dispose();
            LoginServer.DisconnectClient(Id);
        }
    }
}