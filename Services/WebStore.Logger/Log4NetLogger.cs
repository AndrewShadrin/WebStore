using System;
using System.Reflection;
using System.Xml;
using log4net;
using Microsoft.Extensions.Logging;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog log;
        public Log4NetLogger(string category, XmlElement configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));
            log = LogManager.GetLogger(logger_repository.Name, category);
            log4net.Config.XmlConfigurator.Configure(logger_repository, configuration);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug: return log.IsDebugEnabled;
                case LogLevel.Information: return log.IsInfoEnabled;
                case LogLevel.Warning: return log.IsWarnEnabled;
                case LogLevel.Error: return log.IsErrorEnabled;
                case LogLevel.Critical: return log.IsFatalEnabled;
                case LogLevel.None: return false;
                default: throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception error, Func<TState, Exception, string> formatter)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));

            }

            if (!IsEnabled(logLevel))
            {
                return;
            }

            var log_message = formatter(state, error);

            if (string.IsNullOrEmpty(log_message) && error is null)
            {
                return;
            }

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug: log.Debug(log_message);
                    break;
                case LogLevel.Information: log.Info(log_message);
                    break;
                case LogLevel.Warning: log.Warn(log_message);
                    break;
                case LogLevel.Error: log.Error(log_message, error);
                    break;
                case LogLevel.Critical: log.Fatal(log_message, error);
                    break;
                case LogLevel.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }
    }
}
