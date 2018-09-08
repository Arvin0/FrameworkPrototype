using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SYN.Log.SeriLog
{
    public static class SeriLogExtensions
    {
        /// <summary>
        /// IApplicationBuilder 扩展，添加Serilog到LogFactory
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ILoggerFactory RegisterSerilog(this ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            var serilog = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.WithThreadId()
                .CreateLogger();
            loggerFactory.AddSerilog(serilog);

            return loggerFactory;
        }

        /// <summary>
        /// IApplicationBuilder 扩展，添加Serilog到LogFactory
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="sectionKey"></param>
        /// <returns></returns>
        public static ILoggerFactory RegisterSerilog(this ILoggerFactory loggerFactory, IConfiguration configuration, string sectionKey)
        {
            var serilog = new LoggerConfiguration()
                .ReadFrom.ConfigurationSection(configuration.GetSection(sectionKey))
                .Enrich.WithThreadId()
                .CreateLogger();
            loggerFactory.AddSerilog(serilog);

            return loggerFactory;
        }
    }
}
