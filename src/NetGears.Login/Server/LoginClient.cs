using Ether.Network.Common;
using Ether.Network.Packets;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.Game.Packet;

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

        public override void HandleMessage(INetPacketStream netPacketStream)
        {
            var packets = PacketHandler<LoginClient, PacketBase>.DeserializePackets(netPacketStream.ReadArray<byte>(netPacketStream.Size));

            foreach (var packet in packets)
            {
                Logger.Debug($"{Id} new packet received.");
                PacketHandler<LoginClient, PacketBase>.ExecuteHandler(this, packet);
            }
        }

        public void Disconnect()
        {
            Dispose();
            _loginServer.DisconnectClient(Id);
        }
    }
}