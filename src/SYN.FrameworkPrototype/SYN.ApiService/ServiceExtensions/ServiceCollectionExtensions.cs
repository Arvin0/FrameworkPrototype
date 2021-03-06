﻿using Microsoft.Extensions.DependencyInjection;
using SYN.Cache;
using SYN.Cache.Redis;
using SYN.Service;
using SYN.Service.Impl;

namespace SYN.ApiService.ServiceExtensions
{
    /// <summary>
    /// 业务服务注册
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加缓存操作
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddSingleton<RedisProvider>();
            services.AddSingleton<ICacheProvider, RedisManager>();
            return services;
        }

        /// <summary>
        /// 添加业务服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ISystemConfigurationService, SystemConfigurationService>();

            return services;
        }
    }
}
