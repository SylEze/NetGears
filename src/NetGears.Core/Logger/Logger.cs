using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace NetGears.Core.Logger
{
    public class Logger
    {
        private static ILog _log;

        public static void Initialize(ILog log)
        {
            _log = log;
        }

        public static void Info(object message)
        {
            _log?.Info($"{message}");
        }

        public static void Debug(object message)
        {
            _log?.Debug($"{message}");
        }

        public static void Warn(object message)
        {
            _log?.Warn($"{message}");
        }

        public static void Error(object message, Exception ex = null)
        {
            _log?.Error($"{message} {ex}");
        }

        public static void Fatal(object message, Exception ex = null)
        {
            _log?.Fatal($"{message} {ex}");
        }
    }
}
