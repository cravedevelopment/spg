using System;
using System.Collections.Generic;

namespace SPG.EventSourcing.Logger
{
    public static class LogManager
    {
        private static readonly List<ILogger> Loggers = new List<ILogger>();

        public static void AddLogger(ILogger logger)
        {
            if (Loggers.Contains(logger)) return;
            logger.Log($"Logging started at: {DateTime.Now}", LogSeverity.Information);
            Loggers.Add(logger);
        }

        public static void RemoveLogger(ILogger logger)
        {
            if (!Loggers.Contains(logger)) return;
            logger.Log($"Logging stopped at: {DateTime.Now}", LogSeverity.Information);
            Loggers.Remove(logger);
        }

        public static void Log(string message, LogSeverity severity)
        {
            foreach (var l in Loggers)
            {
                l.Log(message, severity);
            }
        }
    }
}