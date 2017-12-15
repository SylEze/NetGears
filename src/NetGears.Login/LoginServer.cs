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
        private static ILog _log;

        private static void InitializeLogger()
        {
            ILoggerRepository loggerRepository = LogManager.GetRepository(Assembly.GetExecutingAssembly());
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));
            _log = LogManager.GetLogger(typeof(LoginServer));
            Logger.Initialize(_log);
        }
        
        private static void Initialize()
        {
            InitializeLogger();
        }
        
        private static void Main(string[] args)
        {
            Initialize();
            Console.WriteLine("Login server started");
        }
    }
}