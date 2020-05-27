using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;

namespace Ray.Infrastructure.Helpers
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        ///  获取Asp.Net FrameWork项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssemblies()
        {
            //todo:需要当前项目引用所有程序集，待改善
            //1.获取当前程序集所有引用程序集
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            List<Assembly> assemblies = entryAssembly?.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(m => m.FullName.Contains("Ray"))
                .ToList();
            assemblies?.Add(entryAssembly);
            return assemblies?.ToArray();
        }

        /// <summary>
        ///  获取托管在IIS上的Asp.Net FrameWork项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesWebIIS()
        {
            return GetAllAssemblies();
            /*
            //应用第一次启动时IIS托管应用里面所有的程序集都被加载进 AppDomain , 但是 当AppDomain被IIS回收时, 程序集只会按需加载.
            //使用如下方法会立刻强制相关的程序集加载进 AppDomain 使其可以被用于模块扫描.
            Assembly[] assemblies = System.Web.Compilation.BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                .Where(x => x.FullName.Contains("Ray"))
                .ToArray();
            return assemblies;
            */
        }

        /// <summary>
        ///  获取Asp.Net Core项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesCoreWeb()
        {
            var list = new List<Assembly>();
            DependencyContext dependencyContext = DependencyContext.Default;
            IEnumerable<CompilationLibrary> libs = dependencyContext.CompileLibraries
                .Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Name.StartsWith("Ray"));
            foreach (var lib in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                list.Add(assembly);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 是否为可空类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type t)
        {
            if (t.IsGenericType)
                return t.GetGenericTypeDefinition() == typeof(Nullable<>);
            return false;
        }

        /// <summary>
        /// Tries to gets an of attribute defined for a class member and it's declaring type including inherited attributes.
        /// Returns default value if it's not declared at all.
        /// </summary>
        /// <typeparam name="TAttribute">Type of the attribute</typeparam>
        /// <param name="memberInfo">MemberInfo</param>
        /// <param name="defaultValue">Default value (null as default)</param>
        /// <param name="inherit">Inherit attribute from base classes</param>
        public static TAttribute GetSingleAttributeOrDefault<TAttribute>(MemberInfo memberInfo, TAttribute defaultValue = default, bool inherit = true)
            where TAttribute : Attribute
        {
            //Get attribute on the member
            if (memberInfo.IsDefined(typeof(TAttribute), inherit))
            {
                return memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>().First();
            }

            return defaultValue;
        }

    }
}
