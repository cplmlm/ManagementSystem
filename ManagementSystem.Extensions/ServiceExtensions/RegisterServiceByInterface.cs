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
    public static class RegisterServiceByInterface
    {
        /// <summary>
        /// 通过 Interface 批量注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceLifetime"></param>
        public static IServiceCollection BatchRegisterServiceByInterface(this IServiceCollection services, Type lifetimeType)
        {
            List<Type> types = AssemblysExtensions.GetAllAssemblies().SelectMany(t => t.GetTypes()).Where(t => lifetimeType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract && !t.Name.Contains("Base")).ToList();
            foreach (var type in types)
            {
                Type[] interfaces = type.GetInterfaces();
                interfaces.ToList().ForEach(t =>
                {
                    if (t== typeof(ISingletonInterface))
                    {
                        services.AddSingleton(t, type);
                    }
                    else if (t == typeof(IScopedInterface))
                    {
                        services.AddScoped(t, type);
                    }
                    else if (t == typeof(ITransientInterface))
                    {
                        services.AddTransient(t, type);
                    }
                });
            }
            //注册UnitOfWorkManage
            services.AddScoped<IUnitOfWorkManage, UnitOfWorkManage>();
            //要单独注册泛型接口和实现，否则会报错
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
           // services.AddTransient(typeof(IBaseServices<>), typeof(BaseServices<>));
            return services;
        }
    }
}
