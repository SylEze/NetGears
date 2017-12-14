using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace NetGears.Core.Logger
{
    public class Logger
    {
        public static ILog Log;

        public static void Initialize(ILog log)
        {
            Log = log;
        }

        public static void Info(object message)
        {
            Log?.Info($"{message}");
        }

        public static void Debug(object message)
        {
            Log?.Debug($"{message}");
        }

        public static void Warn(object message)
        {
            Log?.Warn($"{message}");
        }

        public static void Error(object message, Exception ex = null)
        {
            Log?.Error($"{message} {ex}");
        }

        public static void Fatal(object message, Exception ex = null)
        {
            Log?.Fatal($"{message} {ex}");
        }
    }
}
