using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Sources;

namespace Ray.Infrastructure.Test
{
    public class TestFactory
    {
        private readonly List<Type> _typeList;

        public TestFactory()
        {
            var assembly = Assembly.GetCallingAssembly();
            _typeList = GetTestTypes(new[] { assembly });
        }

        public TestFactory(Assembly[] assemblies)
        {
            _typeList = GetTestTypes(assemblies);
        }

        public ITest Create(string num)
        {
            Type type = _typeList.FirstOrDefault(x => x.Name.EndsWith(num));

            return type == null
                ? null
                : Activator.CreateInstance(type).As<ITest>();
        }

        public Dictionary<string, string> Selections
            => _typeList.ToDictionary(x => x.Name.Substring(x.Name.Length - 2),
                    x => x.GetCustomAttribute(typeof(DescriptionAttribute))?.As<DescriptionAttribute>().Description);


        private List<Type> GetTestTypes(Assembly[] assemblies)
        {
            var resultList = new List<Type>();
            foreach (var assembly in assemblies)
            {
                List<Type> list = assembly
                    .GetTypes()
                    .Where(x => x.IsClass
                                && !x.IsAbstract
                                && !x.IsInterface
                                && typeof(ITest).IsAssignableFrom(x))
                    .ToList();
                resultList.AddRange(list);
            }

            return resultList;
        }

    }
}
