using System;
using System.Collections.Generic;
using Ether.Network;
using Ether.Network.Packets;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.GameData.Packets;
using NetGears.Login.Handlers;

namespace NetGears.Login
{
    public class LoginServer : NetServer<LoginClient>
    {
        private const string ServerConfigurationPath = "server.json";

        public ServerConfiguration ServerConfiguration { get; set; }

        public LoginServer()
        {
            ServerConfiguration = ConfigurationLoader.Instance.Load<ServerConfiguration>(ServerConfigurationPath);

            PacketFactory.Initialize<PacketBase>();
            
            PacketHandler.Initialize<AuthentificationHandler>();

            Configuration.Host = ServerConfiguration.Host;
            Configuration.Port = ServerConfiguration.Port;
            Configuration.MaximumNumberOfConnections = 10;
            Configuration.Backlog = 100;
            Configuration.BufferSize = 4096;
        }

        protected override void Initialize()
        {
            Logger.Info("Server initialized");
        }

        protected override void OnClientConnected(LoginClient connection)
        {
            connection.Initialize(this);
            Logger.Info($"New connected client, id:{connection.Id}");
        }

        protected override void OnClientDisconnected(LoginClient connection)
        {
            Logger.Info($"Client of id:{connection.Id} disconnected");
        }

        protected override IReadOnlyCollection<NetPacketBase> SplitPackets(byte[] buffer)
        {
            return PacketFactory.Deserialize<PacketBase>(buffer);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Logger.Info("Server disposed");
        }
    }
}