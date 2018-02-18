using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;

namespace NetGears.Login
{
    public class LoginServer : NetServer<LoginClient>
    {
        private const string ServerConfigurationPath = "server.json";
        private const string LoginConfigurationPath = "login.json";
        private const string DatabaseConfigurationPath = "database.json";

        private readonly ServerConfiguration _serverConfiguration;
        private readonly DatabaseConfiguration _databaseConfiguration;

        public LoginServer()
        {
            _serverConfiguration = ConfigurationLoader.Instance.Load<ServerConfiguration>(ServerConfigurationPath);
            _databaseConfiguration = ConfigurationLoader.Instance.Load<DatabaseConfiguration>(DatabaseConfigurationPath);

            Configuration.Host = _serverConfiguration.Host;
            Configuration.Port = _serverConfiguration.Port;
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

        protected override void Dispose(bool disposing)
        {
            ConfigurationLoader.Instance.Save(_serverConfiguration, ServerConfigurationPath);
            ConfigurationLoader.Instance.Save(_databaseConfiguration, DatabaseConfigurationPath);

            base.Dispose(disposing);

            Logger.Info("Server disposed");
        }
    }
}
