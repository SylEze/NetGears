using System;
using NLog;
using NLog.Conditions;
using NLog.Config;
using NLog.Targets;

namespace NetGears.Core.Logger
{
    public class Logger
    {
        private const string DefaultLayout = "[${date}][${level:uppercase=true}][${logger:shortName=true}] ${message} ${exception:format=tostring}";

        private ILogger _log { get; }

        private Logger(Type type) => _log = LogManager.GetLogger(type.ToString());

        private Logger(string loggerName) => _log = LogManager.GetLogger(loggerName);

        /// <summary>
        /// Initialize logger's configuration.
        /// Refer to https://github.com/nlog/NLog/wiki/Layout-Renderers for custom layouts.
        /// </summary>
        /// <param name="consoleLayout"></param>
        /// <param name="fileLayout"></param>
        public static void Initialize(string consoleLayout, string fileLayout)
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget();
            var fileTarget = new FileTarget();

            consoleTarget.Layout = consoleLayout;

            var infoHighlightRule = new ConsoleRowHighlightingRule
            {
                Condition = ConditionParser.ParseExpression("level == LogLevel.Info"),
                ForegroundColor = ConsoleOutputColor.Green
            };
            var errorHighlightRule = new ConsoleRowHighlightingRule
            {
                Condition = ConditionParser.ParseExpression("level == LogLevel.Error"),
                ForegroundColor = ConsoleOutputColor.Red
            };
            var warnHighlightingRule = new ConsoleRowHighlightingRule
            {
                Condition = ConditionParser.ParseExpression("level == LogLevel.Warn"),
                ForegroundColor = ConsoleOutputColor.DarkYellow
            };
            consoleTarget.RowHighlightingRules.Add(infoHighlightRule);
            consoleTarget.RowHighlightingRules.Add(errorHighlightRule);
            consoleTarget.RowHighlightingRules.Add(warnHighlightingRule);

            fileTarget.Layout = fileLayout;
            fileTarget.FileName = "logs/" + DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss") + ".log";

            config.AddTarget("console", consoleTarget);
            config.AddTarget("file", fileTarget);

#if (DEBUG)
            var rule1 = new LoggingRule("*", LogLevel.Trace, consoleTarget);
            config.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Trace, fileTarget);
            config.LoggingRules.Add(rule2);
#elif (RELEASE)
            var rule1 = new LoggingRule("*", LogLevel.Info, consoleTarget);
            config.LoggingRules.Add(rule1);

            var rule2 = new LoggingRule("*", LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule2);
#endif
    
            LogManager.Configuration = config;
        }

        public static void Initialize()
        {
            Initialize(DefaultLayout, DefaultLayout);
        }

        public static Logger GetLogger<TClass>() => new Logger(typeof(TClass));

        public static Logger GetLogger(string loggerName) => new Logger(loggerName);

        public void Trace(string msg)
        {
            _log?.Trace(msg);
        }

        public void Debug(string msg)
        {
            _log?.Debug(msg);
        }

        public void Info(string msg)
        {
            _log?.Info(msg);
        }

        public void Warn(string msg)
        {
            _log?.Warn(msg);
        }

        public void Error(string msg, Exception ex)
        {
            _log?.Error(ex, msg);
        }

        public void Fatal(string msg, Exception ex)
        {
            _log?.Fatal(ex, msg);
        }
    }
}
