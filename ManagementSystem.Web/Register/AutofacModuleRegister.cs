using System.Reflection;
using Autofac;
using ManagementSystem.IServices.Base;
using ManagementSystem.Repository.Base;
using ManagementSystem.Repository.UnitOfWorks;
using ManagementSystem.Services.Base;


namespace ManagementSystem.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="Exception"></exception>
        protected override void Load(ContainerBuilder builder)
        {
            var basePath = AppContext.BaseDirectory;
            var servicesDllFile = Path.Combine(basePath, "ManagementSystem.Services.dll");
            var repositoryDllFile = Path.Combine(basePath, "ManagementSystem.Repository.dll");
            if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
            {
                var msg = "Repository.dll和service.dll 丢失，因为项目解耦了，所以需要先F6编译，再F5运行，请检查 bin 文件夹，并拷贝。";
                //Log.Error(msg);
                throw new Exception(msg);
            }
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency(); //注册仓储
            builder.RegisterGeneric(typeof(BaseServices<>)).As(typeof(IBaseServices<>)).InstancePerDependency();     //注册服务
            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired();       //引用Autofac.Extras.DynamicProxy;

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.RegisterType<UnitOfWorkManage>().As<IUnitOfWorkManage>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();
        }
    }
}
