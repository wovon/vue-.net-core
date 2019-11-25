using System;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using V.NetCore.App;
using V.NetCore.Infrastructure;
using V.NetCore.Infrastructure.Helpers;
using V.NetCore.Repository;

namespace V.NetCore.Api
{
    public class Startup
    {
        public static ILoggerRepository Repository { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化log4net
            Repository = LogManager.CreateRepository("NETCoreRepository");
            Log4NetHelper.SetConfig(Repository, "log4net.config");
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //启用MemoryCache
            services.AddMemoryCache();
            //设置MemoryCache缓存有效时间为5分钟。
            services.Configure<MemoryCacheEntryOptions>(
                options => options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5));
            //跨域
            services.AddCors();
            //配置DBConctext
            services.AddDbContext<VDbContext>(options =>
            {
                options.EnableSensitiveDataLogging(true);
                options.UseSqlServer(Configuration.GetConnectionString("VDbContext"));
            });
            //记录日志
            services.AddMvc(option => { option.Filters.Add(new GlobalExceptionFilter()); })
                .AddControllersAsServices();
            return new AutofacServiceProvider(AutofacExt.InitAutofac(services));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //可以允许任意跨域
            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseExceptionHandler("/Error/Show");
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Api_default",
                    template: "api/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
