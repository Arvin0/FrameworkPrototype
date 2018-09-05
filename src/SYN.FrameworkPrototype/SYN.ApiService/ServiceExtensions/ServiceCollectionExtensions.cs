using Microsoft.Extensions.DependencyInjection;
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
