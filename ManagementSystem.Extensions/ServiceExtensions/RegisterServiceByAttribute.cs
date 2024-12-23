using Microsoft.Extensions.DependencyInjection;
using ManagementSystem.Common;
using ManagementSystem.Common.Extensions;
using ManagementSystem.Common.LifetimeInterfaces;
using ManagementSystem.IServices.Base;
using ManagementSystem.Repository.Base;
using ManagementSystem.Repository.UnitOfWorks;
//using ManagementSystem.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManagementSystem.Extensions
{
    public static class RegisterServiceByAttribute
    {
        /// <summary>
        /// 通过 Interface 批量注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceLifetime"></param>
        public static IServiceCollection BatchRegisterServiceByAttribute(this IServiceCollection services, ServiceLifetime serviceLifetime)
        {
            List<Type> types = AssemblysExtensions.GetAllAssemblies().SelectMany(t => t.GetTypes()).Where(t => t.GetCustomAttributes(typeof(ServiceAttribute), false).Length > 0 && t.GetCustomAttribute<ServiceAttribute>()?.Lifetime == serviceLifetime && t.IsClass && !t.IsAbstract).ToList();

            foreach (var type in types)
            {
                Type? typeInterface = type.GetInterfaces().FirstOrDefault();
                if (typeInterface == null)
                {
                    //服务非继承自接口的直接注入
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton: services.AddSingleton(type); break;
                        case ServiceLifetime.Scoped: services.AddScoped(type); break;
                        case ServiceLifetime.Transient: services.AddTransient(type); break;
                    }
                }
                else
                {
                    //服务继承自接口的和接口一起注入
                    switch (serviceLifetime)
                    {
                        case ServiceLifetime.Singleton: services.AddSingleton(typeInterface, type); break;
                        case ServiceLifetime.Scoped: services.AddScoped(typeInterface, type); break;
                        case ServiceLifetime.Transient: services.AddTransient(typeInterface, type); break;
                    }
                }
            }
            //注册UnitOfWorkManage
            services.AddScoped<IUnitOfWorkManage, UnitOfWorkManage>();
            //要单独注册泛型接口和实现，否则会报错
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddTransient(typeof(IBaseServices<>), typeof(BaseServices<>));
            return services;
        }
    }
}
