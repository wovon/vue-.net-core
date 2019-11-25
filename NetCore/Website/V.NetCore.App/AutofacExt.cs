using V.NetCore.App.Interface;
using V.NetCore.Repository.Interface;
using V.NetCore.Infrastructure.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using V.NetCore.Repository;
using V.NetCore.App.SSO;

namespace V.NetCore.App
{
    public static class AutofacExt
    {
        private static Autofac.IContainer _container;
        public static Autofac.IContainer InitAutofac(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            //注册数据库基础操作和工作单元
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUnitWork), typeof(UnitWork));

            services.AddScoped(typeof(IAuth), typeof(LocalAuth));

            //注册app层
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            //防止单元测试时已经注入
            if (services.All(u => u.ServiceType != typeof(ICacheContext)))
            {
                services.AddScoped(typeof(ICacheContext), typeof(CacheContext));
            }
            
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            builder.Populate(services);

            _container = builder.Build();
            return _container;

        }

        /// <summary>
        /// 从容器中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T GetFromFac<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
