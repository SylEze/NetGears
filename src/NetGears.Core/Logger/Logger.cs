using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace NetGears.Core.Logger
{
    public static class Logger
    {
        private static ILog _log;

        public static void Initialize(Type type)
        {
            ILoggerRepository loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));
            _log = LogManager.GetLogger(type);
        }

        public static void Debug(object message)
        {
            _log?.Debug($"{message}");
        }

        public static void Info(object message)
        {
            _log?.Info($"{message}");
        }

        public static void Warn(object message)
        {
            _log?.Warn($"{message}");
        }

        public static void Error(object message, Exception ex)
        {
            _log?.Error($"{message} {ex}");
        }

        public static void Error(object message)
        {
            _log?.Error($"{message}");
        }

        public static void Fatal(object message, Exception ex)
        {
            _log?.Fatal($"{message} {ex}");
        }

        public static void Fatal(object message)
        {
            _log?.Fatal($"{message}");
        }
    }
}