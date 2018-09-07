using System;
using System.Threading.Tasks;

namespace SYN.Cache
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public interface ICacheProvider
    {
        /// <summary>
        /// 读取String类型缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 读取String类型缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 读取String类型缓存，不存在则读取一次并缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getData"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<T> GetOrAddAsync<T>(string key, Func<T> getData, TimeSpan? expiry = null);

        /// <summary>
        /// 读取String类型缓存，不存在则读取一次并缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getData"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        T GetOrAdd<T>(string key, Func<T> getData, TimeSpan? expiry = null);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool Set<T>(string key, T value, TimeSpan? expiry = null);

        /// <summary>
        /// 清除指定key数据
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 清除指定key数据
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        bool Remove(string key);
    }
}
