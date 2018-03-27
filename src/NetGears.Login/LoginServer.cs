using System;
using System.Collections.Generic;
using Ether.Network;
using Ether.Network.Packets;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.Game.Packets;
using NetGears.Login.Handlers;

namespace NetGears.Login
{
    public class LoginServer : NetServer<LoginClient>
    {
        private const string ServerConfigurationPath = "login.json";

        public ServerConfiguration ServerConfiguration { get; set; }

        public LoginServer()
        {
            ServerConfiguration = ConfigurationLoader.Instance.Load<ServerConfiguration>(ServerConfigurationPath);

            Logger.Info("Server configuration loaded.");
            
            PacketFactory<PacketBase, PacketHeaderAttribute>.Initialize();

            PacketHandler<LoginClient, PacketBase>.LoadFrom<AuthentificationHandler>();

            Configuration.Host = ServerConfiguration.Host;
            Configuration.Port = ServerConfiguration.Port;
            Configuration.MaximumNumberOfConnections = 10;
            Configuration.Backlog = 100;
            Configuration.BufferSize = 4096;
        }

        protected override void Initialize()
        {
            Logger.Info($"Server initialized on {Configuration.Host}:{Configuration.Port}");
        }

        protected override void OnClientConnected(LoginClient connection)
        {
            connection.Initialize(this);
            Logger.Info($"Client {connection.Id} connected");
        }

        protected override void OnClientDisconnected(LoginClient connection)
        {
            Logger.Info($"Client {connection.Id} disconnected");
        }

        protected override IReadOnlyCollection<NetPacketBase> SplitPackets(byte[] buffer)
        {
            return PacketFactory<PacketBase, PacketHeaderAttribute>.Deserialize(buffer, PacketDeserializeHelper.DeserializeBuffer) 
                as IReadOnlyCollection<NetPacketBase>;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Logger.Info("Server disposed");
        }
    }
}