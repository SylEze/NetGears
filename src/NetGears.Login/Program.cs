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
    internal static class Program
    {
        private static ServerConfiguration _serverConfiguration;
        
        private static void Main(string[] args)
        {
            Logger.Initialize(typeof(Program));
            _serverConfiguration = ConfigurationLoader.Instance.Load<ServerConfiguration>("login.json");

            Logger.Info("Login server started");

            ConfigurationLoader.Instance.Save<ServerConfiguration>(_serverConfiguration, "login.json");
        }
    }
}