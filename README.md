# FrameworkPrototype
项目框架

# 目前包含的功能
1. 接入Swagger
2. 跨域设置
3. 日志输出到文件 (loggerFactory.RegisterSerilog(Configuration);)
4. EF Core 执行SQL记录及扩展方法输出执行的SQL(ToSql)
5. 统一异常捕获 (app.UseExceptionHandle();)
6. API执行时间统计 (app.StatisticExecutedTime();)
7. 配置信息统一管理 (services.SetConfiguration(Configuration);)
8. API返回结果格式化处理 (ApiResponse)
9. 扩展方法创建DbContext (services.AddDbContexxt(GetType().Namespace);)
10. API统一添加路由前缀 (options.UseCentralRoutePrefix)
11. 添加Redis缓存(services.AddCache();)


# 技术栈
* 基础框架： .NET Core
* web 框架: web api
* 数据库：Postgresql
* ORM: ef core + EFCoreUtil
* log: Serilog
* 缓存：Redis

# 持续完善中......
