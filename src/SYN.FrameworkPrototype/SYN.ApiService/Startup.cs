using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using SYN.ApiCore.Extensions;
using SYN.ApiCore.Extensions.RoutePrefix;
using SYN.ApiService.ServiceExtensions;
using SYN.Log.SeriLog;
using SYN.Repository.ServiceExtenstions;

namespace SYN.ApiService
{
    internal class Startup
    {
        public Startup(IHostingEnvironment evn)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(evn.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{evn.EnvironmentName}.json", false, true)
                .AddJsonFile("serilogsettings.json", false, true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // 设置Swagger
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "SYN API"
                });
            });

            // 设置配置信息
            services.SetConfiguration(Configuration);

            // 注册DbContext
            services.AddDbContexxt(GetType().Namespace);

            // 添加业务服务
            services.AddBusinessServices();

            // 跨域设置
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllCors", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            services.AddMvc(
                options =>
                {
                    options.UseCentralRoutePrefix(new RouteAttribute("api"));
                }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Serilog
            loggerFactory.RegisterSerilog(Configuration);

            // 异常捕获
            app.UseExceptionHandle();

            // API时间统计
            app.StatisticExecutedTime();

            // swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SYN API v1");
                c.DocExpansion(DocExpansion.None);
            });

            // 跨域
            app.UseCors("AllowAllCors");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
