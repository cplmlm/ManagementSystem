using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ManagementSystem.Common.Extensions;

public static class AssemblysExtensions
{
    public static List<Assembly> GetAllAssemblies()
    {
        var list = new List<Assembly>();
        var deps = DependencyContext.Default;
        var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" );
        foreach (var lib in libs)
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
            list.Add(assembly);
        }

        return list;
    }

    /// <summary>
    ///     根据AssemblyName获取所有的类
    /// </summary>
    /// <param name="assemblyName"></param>
    /// <returns></returns>
    public static IList<Type> GetTypesByAssembly(string assemblyName)
    {
        var list = new List<Type>();
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        var typeinfos = assembly.DefinedTypes;
        foreach (var typeinfo in typeinfos) list.Add(typeinfo.AsType());
        return list;
    }
}