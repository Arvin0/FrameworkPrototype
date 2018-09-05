using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace SYN.Common.Config
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        public static void Init(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IConfiguration GetConfiguration()
        {
            return _configuration;
        }

        public static string GetValue(string key)
        {
            return _configuration.GetValue<string>(key);
        }

        public static T GetValue<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }

        /// <summary>
        /// 获取Section对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSection<T>(string key) where T : class, new()
        {
            var value =  new ServiceCollection()
                .Configure<T>(_configuration.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;

            return value;
        }

        /// <summary>
        /// 获取IConfigurationSection对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IConfigurationSection GetSection(string key)
        {
            return _configuration.GetSection(key);
        }

        /// <summary>
        /// 读取本地文件以解决特殊需求
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValueForMigration<T>(string key)
        {
            if (_configuration != null)
            {
                return GetValue<T>(key);
            }

            var migrationConfiguration = new ConfigurationBuilder()
                .SetBasePath(PlatformServices.Default.Application.ApplicationBasePath)
                .AddJsonFile("efsettings.json", true, true)
                .Build();

            if (migrationConfiguration != null)
            {
                return migrationConfiguration.GetValue<T>(key);
            }

            return default(T);
        }
    }
}
