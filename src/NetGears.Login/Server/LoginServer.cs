﻿using System;
using Ether.Network.Packets;
using Ether.Network.Server;
using NetGears.Core.Logger;
using NetGears.Core.Misc;
using NetGears.Core.Network;
using NetGears.Game.Packet;
using NetGears.Login.Misc;

namespace NetGears.Login.Server
{
    public class LoginServer : NetServer<LoginClient>
    {
        private static readonly Logger Logger = Logger.GetLogger<LoginServer>();
        
        private const string ServerConfigurationPath = "config/login.json";

        public LoginConfiguration LoginConfiguration { get; set; }

        protected override IPacketProcessor PacketProcessor => new LoginPacketProcessor();

        public LoginServer()
        {
            LoginConfiguration = JsonConfigurationLoader.Load<LoginConfiguration>(ServerConfigurationPath);
            Configuration.Host = LoginConfiguration.Host;
            Configuration.Port = LoginConfiguration.Port;
            Configuration.MaximumNumberOfConnections = (int)LoginConfiguration.MaxConnections;
            Configuration.Backlog = 100;
            Configuration.BufferSize = 4096;
        }

        protected override void Initialize()
        {
            PacketHandler<LoginClient, PacketBase>.LoadPackets(PacketServerType.LOGIN);
            PacketHandler<LoginClient, PacketBase>.LoadHandlers<LoginPacketHandler>();

            Logger.Info($"Server initialized on {Configuration.Host}:{Configuration.Port}.");
        }
        
        protected override void OnClientConnected(LoginClient connection)
        {
            connection.Initialize(this);
            Logger.Info($"Client {connection.Id} connected.");
        }

        protected override void OnClientDisconnected(LoginClient connection)
        {
            Logger.Info($"Client {connection.Id} disconnected.");
        }

        protected override void OnError(Exception exception)
        {
            Logger.Error("An error occured: ", exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Logger.Info("Server disposed.");
        }
    }
}