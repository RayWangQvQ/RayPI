using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RayPI.Infrastructure.Ioc.Helpers
{
    /// <summary>
    /// 反射辅助类
    /// </summary>
    public static class IocHelper
    {
        /// <summary>
        ///  获取Asp.Net Core项目所有程序集
        /// </summary>
        /// <returns></returns>
        public static Assembly[] GetAllAssembliesCoreWeb()
        {
            //1.获取当前程序集(Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc)所有引用程序集
            Assembly executingAssembly = Assembly.GetExecutingAssembly();//当前程序集
            List<Assembly> assemblies = executingAssembly.GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Where(m => m.FullName.Contains("RayPI"))
                .ToList();
            //2.获取启动入口程序集（Ray.EssayNotes.AutoFac.CoreApi）
            Assembly assembly = Assembly.GetEntryAssembly();
            assemblies.Add(assembly);
            return assemblies.ToArray();
        }
    }
}
