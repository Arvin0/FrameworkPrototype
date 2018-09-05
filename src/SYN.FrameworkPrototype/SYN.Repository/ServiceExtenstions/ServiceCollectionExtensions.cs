using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SYN.Common.Config;
using SYN.Log.EFLog;
using SYN.Log.SeriLog;
using SYN.Repository.DBContexts;

namespace SYN.Repository.ServiceExtenstions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册DBContext
        /// </summary>
        /// <param name="service"></param>
        /// <param name="migrationsAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContexxt(this IServiceCollection service, string migrationsAssembly)
        {
            var logger = new LoggerFactory().RegisterSerilog(ConfigHelper.GetConfiguration(), "SerilogSQL");

            /*
             * EF Core会自动输出执行SQL，故以下设置不是必须；
             * 但是保留该部分代码，以便后续使用
             */
            //logger.AddProvider(new EFLoggerProvider(logger.CreateLogger<EFLoggerProvider>()));

            service.AddDbContextPool<SynDbContext>(options =>
            {
                options.UseNpgsql(ConfigHelper.GetValue("ConnectionStrings:Default"),
                    opt => opt.MigrationsAssembly(migrationsAssembly))
                    .UseLoggerFactory(logger);
            }).AddUnitOfWork<SynDbContext>();

            return service;
        }
    }
}
