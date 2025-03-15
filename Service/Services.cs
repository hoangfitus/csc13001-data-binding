using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace csc13001_data_binding.Service
{
    public class Services
    {
        static Dictionary<string, object> _singletons = new Dictionary<string, object>();
        public static void AddSingleton<IParent, Child>()
        {
            Type parent = typeof(IParent);
            Type child = typeof(Child);

            ConstructorInfo constructor = child.GetConstructors()
               .OrderByDescending(c => c.GetParameters().Length)
               .First();

            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parameterInstances = parameters
                .Select(p => GetSingleton(p.ParameterType))
                .ToArray();

            _singletons[parent.Name] = Activator.CreateInstance(child, parameterInstances)!;
        }

        public static IParent GetSingleton<IParent>()
        {
            Type parent = typeof(IParent);
            return (IParent)_singletons[parent.Name];
        }

        public static object GetSingleton(Type type)
        {
            if (!_singletons.ContainsKey(type.Name))
                throw new InvalidOperationException($"Service of type {type.Name} is not registered.");
            return _singletons[type.Name];
        }
    }
}
