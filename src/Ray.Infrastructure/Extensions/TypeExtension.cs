using System;
using System.Collections.Generic;
using System.Text;

namespace Ray.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static string GetFullNameWithAssemblyName(this Type type)
        {
            return type.FullName + ", " + type.Assembly.GetName().Name;
        }
    }
}
