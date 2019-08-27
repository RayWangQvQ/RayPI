using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace RayPI.Infrastructure.Treasury.Di
{
    public static class DiHelper
    {
        /// <summary>
        ///  获取Asp.Net Core项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAllAssembliesCoreWeb()
        {
            var assemblies = new List<Assembly>();
            DependencyContext dependencyContext = DependencyContext.Default;
            IEnumerable<CompilationLibrary> libs = dependencyContext.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Name.StartsWith("RayPI"));
            foreach (var lib in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                assemblies.Add(assembly);
            }

            return assemblies;
        }
    }
}
