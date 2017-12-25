using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;
using NetGears.Core.Logger;

namespace NetGears.Login
{
    internal static class LoginServer
    {
        private static void InitializeLogger()
        {
            ILoggerRepository loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));
            Logger.Initialize(LogManager.GetLogger(typeof(LoginServer)));
        }
        
        private static void Initialize()
        {
            InitializeLogger();
        }
        
        private static void Main(string[] args)
        {
            Initialize();
            Logger.Info("Login server started");
        }
    }
}