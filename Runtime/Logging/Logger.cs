using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using DA.Exceptions;
using UnityEngine;

namespace DA.Logging
{
    public static class Logger
    {
        private static Serilog.Core.Logger log;

        private static LoggerConfig loggerConfig;

        private static LoggingLevelSwitch level;

        public static LoggerConfig LoggerConfig
        {
            get => loggerConfig;
            internal set
            {
                if (IsLoggerInitialized)
                    throw new InitializationException("The logger is already initialized!");

                loggerConfig = value;
            }
        }

        public static bool IsLoggerInitialized => log != null;

        //[ConVar("cl_log_debug", "Logs debug messages", nameof(DebugLogModeCallback))]
#if UNITY_EDITOR //Default to debug logging in the editor
        internal static bool DebugLogMode = true;
#else
		public static bool DebugLogMode = false;
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        internal static void Init()
        {
            if (IsLoggerInitialized)
                throw new InitializationException("The logger is already initialized!");

            if (loggerConfig == null)
                loggerConfig = new LoggerConfig();

            Application.quitting += Shutdown;

            level = new LoggingLevelSwitch();
            if (DebugLogMode)
                level.MinimumLevel = LogEventLevel.Debug;

            const string outPutTemplate = "{Timestamp:dd-MM hh:mm:ss tt} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            var logFileName = $"{loggerConfig.logDirectory}{DateTime.Now.ToString(loggerConfig.logFileDateTimeFormat)}.log";

            log = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(level)
                .WriteTo.Async(a => a.File(logFileName, outputTemplate: outPutTemplate, buffered: loggerConfig.bufferedFileWrite))
                .WriteTo.Unity()
                //.WriteTo.Console(outPutTemplate)
                .Enrich.WithDemystifiedStackTraces()
                .CreateLogger();
        }

        internal static void Shutdown()
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized");

            log.Debug("Logger shutting down at {Date}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            log.Dispose();
            log = null;

            loggerConfig = null;

            Application.quitting -= Shutdown;
        }

        public static void DebugLogModeCallback()
        {
            level.MinimumLevel = DebugLogMode ? LogEventLevel.Debug : LogEventLevel.Information;
        }

        #region Debug Logging

        public static void Debug(string message)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            if (DebugLogMode)
                log.Debug(message);
        }

        public static void Debug(string message, params object[] values)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            if (DebugLogMode)
                log.Debug(message, values);
        }

        #endregion

        #region Information Logging

        public static void Info(string message)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Information(message);
        }

        public static void Info(string message, params object[] values)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Information(message, values);
        }

        #endregion

        #region Warning Logging

        public static void Warn(string message)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Warning(message);
        }

        public static void Warn(string message, params object[] values)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Warning(message, values);
        }

        #endregion

        #region Error Logging

        public static void Error(string message)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Error(message);
        }

        public static void Error(string message, params object[] values)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Error(message, values);
        }

        public static void Error(Exception exception, string message)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Error(exception, message);
        }

        public static void Error(Exception exception, string message, params object[] values)
        {
            if (IsLoggerInitialized == false)
                throw new InitializationException("The logger isn't initialized!");

            log.Error(exception, message, values);
        }

        #endregion
    }
}
