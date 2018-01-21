using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;

namespace NetGears.Login
{
    internal static class LoginServer
    {
        private const string LOG4_NET_CONFIG_PATH = "log4net.config";

        private const string SERVER_CONFIG_PATH = "server_config.json";

        private static ServerConfiguration _serverConfiguration;

        private static void InitializeLogger()
        {
            ILoggerRepository loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(loggerRepository, new FileInfo(LOG4_NET_CONFIG_PATH));
            Logger.Initialize(LogManager.GetLogger(typeof(LoginServer)));
        }

        private static void InitializeConfiguration()
        {
            _serverConfiguration = ConfigurationLoader.Instance.Load<ServerConfiguration>(SERVER_CONFIG_PATH);
        }

        private static void SaveConfiguration()
        {
            ConfigurationLoader.Instance.Save<ServerConfiguration>(_serverConfiguration, SERVER_CONFIG_PATH);
        }
        
        private static void Initialize()
        {
            InitializeLogger();
            InitializeConfiguration();
        }

        private static void Save()
        {
            SaveConfiguration();
        }
        
        private static void Main(string[] args)
        {
            Initialize();

            Logger.Info("Login server started");

            Save();
        }
    }
}