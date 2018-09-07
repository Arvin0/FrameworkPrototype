using StackExchange.Redis;
using SYN.Common.Config;
using SYN.Core.Exceptions;

namespace SYN.Cache.Redis
{
    public class RedisProvider
    {
        #region 配置项

        private static string _host;

        /// <summary>
        /// Redis Host
        /// </summary>
        private static string Host
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_host))
                {
                    var key = "Cache:Redis:Host";
                    _host = ConfigHelper.GetValue(key);
                    if (string.IsNullOrWhiteSpace(_host))
                    {
                        throw new ConfigurationException(key);
                    }
                }

                return _host;
            }
        }

        private static string _port;

        /// <summary>
        /// Redis Port
        /// </summary>
        private static string Port
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_port))
                {
                    var key = "Cache:Redis:Port";
                    _port = ConfigHelper.GetValue(key);
                    if (string.IsNullOrWhiteSpace(_port))
                    {
                        throw new ConfigurationException(key);
                    }
                }

                return _port;
            }
        }

        private static string _password;

        /// <summary>
        /// Redis Password
        /// </summary>
        private static string Password
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_password))
                {
                    var key = "Cache:Redis:Password";
                    _password = ConfigHelper.GetValue(key);
                }

                return _password;
            }
        }

        private static int _db = -1;

        /// <summary>
        /// Redis DB
        /// </summary>
        private static int DB
        {
            get
            {
                if (_db == -1)
                {
                    var key = "Cache:Redis:DB";
                    var intDb = ConfigHelper.GetValue(key);
                    if (string.IsNullOrWhiteSpace(intDb) || !int.TryParse(intDb, out var dbValue))
                    {
                        _db = 0;
                    }
                    else
                    {
                        _db = dbValue;
                    }
                }

                return _db;
            }
        }

        #endregion

        #region 实例对象

        /// <summary>
        /// Redis连接对象
        /// </summary>
        private readonly ConnectionMultiplexer _redisConn;

        /// <summary>
        /// 数据库对象
        /// </summary>
        private IDatabase _database;

        public RedisProvider()
        {
            var configurationOptions = ConfigurationOptions.Parse($"{Host}:{Port}");
            if (!string.IsNullOrWhiteSpace(Password))
            {
                configurationOptions.Password = Password;
            }

            _redisConn = ConnectionMultiplexer.Connect(configurationOptions);
            _database = _redisConn.GetDatabase(DB);
        }

        /// <summary>
        /// 数据库对象
        /// </summary>
        public IDatabase Database
        {
            get { return _database; }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        ~RedisProvider()
        {
            _database = null;
            _redisConn.Dispose();
        }

        #endregion
    }
}
