using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using SYN.Cache.Redis;
using SYN.Common.Config;
using System;
using Xunit;

namespace SYN.Cache.Test
{
    public class RedisManagerTest : IDisposable
    {
        private ICacheProvider _cacheProvider;
        
        public RedisManagerTest()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(PlatformServices.Default.Application.ApplicationBasePath)
                .AddJsonFile("appsettings.json", false, true);
            ConfigHelper.Init(configurationBuilder.Build());
            _cacheProvider = new RedisManager(new RedisProvider());
        }

        [Fact]
        public void TestSet()
        {
            var key = "test_set";
            var value = DateTime.Now.ToString();
            _cacheProvider.Set(key, value, TimeSpan.FromMinutes(10));
            var result = _cacheProvider.Get<string>(key);
            Assert.NotNull(result);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestSetAsync()
        {
            var key = "test_set_async";
            var value = DateTime.Now.ToString();
            _cacheProvider.SetAsync(key, value, TimeSpan.FromMinutes(10));
            var result = _cacheProvider.GetAsync<string>(key);
            Assert.NotNull(result);
            Assert.Equal(value, result.Result);
        }

        [Fact]
        public void TestGetAdd()
        {
            var key = "test_get_add";
            var value = DateTime.Now.ToString();
            var result = _cacheProvider.GetOrAdd(key, () => value);
            Assert.NotNull(result);
            Assert.Equal(value, result);
        }

        [Fact]
        public void TestGetAddAsync()
        {
            var key = "test_get_add_async";
            var value = DateTime.Now.ToString();
            var result = _cacheProvider.GetOrAddAsync<string>(key, () => value);
            Assert.NotNull(result);
            Assert.Equal(value, result.Result);
        }

        [Fact]
        public void TestRemove()
        {
            var key = "test_remove";
            var value = DateTime.Now.ToString();
            _cacheProvider.Set(key, value, TimeSpan.FromMinutes(5));
            _cacheProvider.Remove(key);
            var result = _cacheProvider.Get<string>(key);
            Assert.Null(result);
        }

        [Fact]
        public void TestRemoveAsync()
        {
            var key = "test_remove_async";
            var value = DateTime.Now.ToString();
            _cacheProvider.SetAsync(key, value, TimeSpan.FromMinutes(5));
            _cacheProvider.RemoveAsync(key);
            var result = _cacheProvider.Get<string>(key);
            Assert.Null(result);
        }

        public void Dispose()
        {
            _cacheProvider = null;
        }
    }
}
