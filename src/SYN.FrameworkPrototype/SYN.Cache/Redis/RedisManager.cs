using StackExchange.Redis;
using SYN.Cache.Common;
using SYN.Core.Json;
using System;
using System.Threading.Tasks;

namespace SYN.Cache.Redis
{
    /// <summary>
    /// Redis 操作
    /// </summary>
    public class RedisManager : ICacheProvider
    {
        private readonly IDatabase _database;

        public RedisManager(RedisProvider redisProvider)
        {
            _database = redisProvider.Database;
        }

        #region String

        public async Task<T> GetAsync<T>(string key)
        {
            key = KeyHelper.GetKey(key);

            return GetValue<T>(await _database.StringGetAsync(key));
        }

        public T Get<T>(string key)
        {
            key = KeyHelper.GetKey(key);

            return GetValue<T>(_database.StringGet(key));
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<T> getData, TimeSpan? expiry = null)
        {
            var nullKey = $"{key}:NULLDATA";
            if (KeyExistsAsync(nullKey).Result)
            {
                return default(T);
            }

            T value = await GetAsync<T>(key);
            if (value == null || value.Equals(default(T)))
            {
                value = getData();

                // 防止缓存穿透
                if (value == null)
                {
                    await SetAsync(nullKey, "NULLDATA", TimeSpan.FromSeconds(5));
                }
                else
                {
                    await SetAsync(key, value, expiry);
                }
            }

            return value;
        }

        public T GetOrAdd<T>(string key, Func<T> getData, TimeSpan? expiry = null)
        {
            var nullKey = $"{key}:NULLDATA";
            if (KeyExists(nullKey))
            {
                return default(T);
            }

            T value = Get<T>(key);
            if (value == null || value.Equals(default(T)))
            {
                value = getData();

                // 防止缓存穿透
                if (value == null)
                {
                    Set(nullKey, "NULLDATA", TimeSpan.FromSeconds(5));
                    
                }
                else
                {
                    Set(key, value, expiry);
                }
            }

            return value;
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            key = KeyHelper.GetKey(key);
            return await _database.StringSetAsync(key, JsonHelper.SerializeObject(value), expiry);
        }

        public bool Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            key = KeyHelper.GetKey(key);
            return _database.StringSet(key, JsonHelper.SerializeObject(value), expiry);
        }

        #endregion

        #region Remove

        public async Task<bool> RemoveAsync(string key)
        {
            key = KeyHelper.GetKey(key);
            return await _database.KeyDeleteAsync(key);
        }

        public bool Remove(string key)
        {
            key = KeyHelper.GetKey(key);
            return _database.KeyDelete(key);
        }

        #endregion

        #region Private

        /// <summary>
        /// 获取泛型缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        private T GetValue<T>(RedisValue redisValue)
        {
            return redisValue.HasValue ? JsonHelper.DeserializeObject<T>(redisValue) : default(T);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool KeyExists(string key)
        {
            key = KeyHelper.GetKey(key);
            return _database.KeyExists(key);
        }

        /// <summary>
        /// 异步判断key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task<bool> KeyExistsAsync(string key)
        {
            key = KeyHelper.GetKey(key);
            return await _database.KeyExistsAsync(key);
        }

        #endregion
    }
}
