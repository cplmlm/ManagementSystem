using ManagementSystem.Common.Database;
using ManagementSystem.Common.Extensions;
using ManagementSystem.Common.Helper;
using ManagementSystem.Model.Models;
using Newtonsoft.Json;
using SqlSugar;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using ManagementSystem.Common.Const;
using System.Reflection.Metadata;

namespace ManagementSystem.Common.Seed
{
    public class DBSeed
    {
        private static string SeedDataFolder = "BlogCore.Data.json/{0}.tsv";


        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="myContext"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static async Task SeedAsync(MyContext myContext, string WebRootPath)
        {
            try
            {
                if (string.IsNullOrEmpty(WebRootPath))
                {
                    throw new Exception("获取wwwroot路径时，异常！");
                }

                SeedDataFolder = Path.Combine(WebRootPath, SeedDataFolder);

                Console.WriteLine("************ ManagementSystem DataBase Set *****************");
                Console.WriteLine($"Master DB ConId: {myContext.Db.CurrentConnectionConfig.ConfigId}");
                Console.WriteLine($"Master DB Type: {myContext.Db.CurrentConnectionConfig.DbType}");
                Console.WriteLine($"Master DB ConnectString: {myContext.Db.CurrentConnectionConfig.ConnectionString}");
                Console.WriteLine();
                if (BaseDBConfig.MainConfig.SlaveConnectionConfigs.AnyNoException())
                {
                    var index = 0;
                    BaseDBConfig.MainConfig.SlaveConnectionConfigs.ForEach(m =>
                    {
                        index++;
                        Console.WriteLine($"Slave{index} DB HitRate: {m.HitRate}");
                        Console.WriteLine($"Slave{index} DB ConnectString: {m.ConnectionString}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }
                else if (BaseDBConfig.ReuseConfigs.AnyNoException())
                {
                    var index = 0;
                    BaseDBConfig.ReuseConfigs.ForEach(m =>
                    {
                        index++;
                        Console.WriteLine($"Reuse{index} DB Id: {m.ConfigId}");
                        Console.WriteLine($"Reuse{index} DB Type: {m.DbType}");
                        Console.WriteLine($"Reuse{index} DB ConnectString: {m.ConnectionString}");
                        Console.WriteLine($"--------------------------------------");
                    });
                }

                Console.WriteLine();

                // 创建数据库
                Console.WriteLine($"Create Database(The Db Id:{MyContext.ConnId})...");

                if (MyContext.DbType != SqlSugar.DbType.Oracle && MyContext.DbType != SqlSugar.DbType.Dm)
                {
                    myContext.Db.DbMaintenance.CreateDatabase();
                    ConsoleHelper.WriteSuccessLine($"Database created successfully!");
                }
                else
                {
                    //Oracle 数据库不支持该操作
                    ConsoleHelper.WriteSuccessLine($"Oracle 数据库不支持该操作，可手动创建Oracle/Dm数据库!");
                }

                // 创建数据库表，遍历指定命名空间下的class，
                // 注意不要把其他命名空间下的也添加进来。
                Console.WriteLine("Create Tables...");

                var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
                var referencedAssemblies = System.IO.Directory.GetFiles(path, "ManagementSystem.Model.dll")
                    .Select(Assembly.LoadFrom).ToArray();
                var modelTypes = referencedAssemblies
                    .SelectMany(a => a.DefinedTypes)
                    .Select(type => type.AsType())
                    .Where(x => x.IsClass && x.Namespace is "ManagementSystem.Model.Models")
                   // .Where(s => !s.IsDefined(typeof(MultiTenantAttribute), false))
                    .ToList();
                modelTypes.ForEach(t =>
                {
                    // 这里只支持添加表，不支持删除
                    // 如果想要删除，数据库直接右键删除，或者联系SqlSugar作者；
                    if (!myContext.Db.DbMaintenance.IsAnyTable(t.Name))
                    {
                        Console.WriteLine(t.Name);
                        myContext.Db.CodeFirst.SplitTables().InitTables(t);
                    }
                });
                ConsoleHelper.WriteSuccessLine($"Tables created successfully!");
                Console.WriteLine();

               

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"1、若是Mysql,查看常见问题:https://github.com/anjoy8/ManagementSystem/issues/148#issue-776281770 \n" +
                    $"2、若是Oracle,查看常见问题:https://github.com/anjoy8/ManagementSystem/issues/148#issuecomment-752340231 \n" +
                    "3、其他错误：" + ex.Message);
            }
        }
        /// <summary>
        /// 种子初始化数据
        /// </summary>
        /// <param name="myContext"></param>
        /// <returns></returns>
        private static async Task SeedDataAsync(ISqlSugarClient db)
        {
            // 获取所有种子配置-初始化数据
            var seedDataTypes = AssemblysExtensions.GetAllAssemblies().SelectMany(s => s.DefinedTypes)
                .Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass)
                .Where(u =>
                {
                    var esd = u.GetInterfaces()
                        .FirstOrDefault(i => i.HasImplementedRawGeneric(typeof(IEntitySeedData<>)));
                    if (esd is null)
                    {
                        return false;
                    }

                    var eType = esd.GenericTypeArguments[0];
                    //if (eType.GetCustomAttribute<MultiTenantAttribute>() is null)
                    //{
                    //    return true;
                    //}

                    return false;
                });

            if (!seedDataTypes.Any()) return;
            foreach (var seedType in seedDataTypes)
            {
                dynamic instance = Activator.CreateInstance(seedType);
                //初始化数据
                {
                    var seedData = instance.InitSeedData();
                    if (seedData != null && Enumerable.Any(seedData))
                    {
                        var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
                        var entity = db.EntityMaintenance.GetEntityInfo(entityType);

                        if (!await db.Queryable(entity.DbTableName, "").AnyAsync())
                        {
                            await db.Insertable(Enumerable.ToList(seedData)).ExecuteCommandAsync();
                            Console.WriteLine($"Table:{entity.DbTableName} init success!");
                        }
                    }
                }

                //种子数据
                {
                    var seedData = instance.SeedData();
                    if (seedData != null && Enumerable.Any(seedData))
                    {
                        var entityType = seedType.GetInterfaces().First().GetGenericArguments().First();
                        var entity = db.EntityMaintenance.GetEntityInfo(entityType);

                        await db.Storageable(Enumerable.ToList(seedData)).ExecuteCommandAsync();
                        Console.WriteLine($"Table:{entity.DbTableName} seedData success!");
                    }
                }

                //自定义处理
                {
                    await instance.CustomizeSeedData(db);
                }
            }
        }
    }
}