using System;
using Microsoft.Extensions.Logging;

namespace SYN.Log.EFLog
{
    public class EFLogger : ILogger
    {
        private readonly ILogger _logger;
        private readonly string _categoryName;

        public EFLogger(string categoryName, ILogger logger)
        {
            _categoryName = categoryName;
            _logger = logger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (_categoryName.Equals("Microsoft.EntityFrameworkCore.Database.Command"))
            {
                /**
                 * 20100 : CommandExecuting; 20101: CommandExecuted; 20300: DataReaderDisposing;
                 */
                if (eventId.Id == 20101)
                {
                    var logContext = formatter(state, exception);
                    _logger.LogError($"Record SQL Manually : {logContext}");
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
