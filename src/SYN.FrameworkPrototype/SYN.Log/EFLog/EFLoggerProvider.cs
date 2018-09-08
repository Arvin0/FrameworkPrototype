using Microsoft.Extensions.Logging;

namespace SYN.Log.EFLog
{
    public class EFLoggerProvider : ILoggerProvider
    {
        private readonly ILogger<EFLoggerProvider> _logger;

        public EFLoggerProvider(ILogger<EFLoggerProvider> logger)
        {
            _logger = logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new EFLogger(categoryName, _logger);
        }

        /// <summary>
        /// There is no resource to release.
        /// </summary>
        public void Dispose()
        {
        }
    }
}
