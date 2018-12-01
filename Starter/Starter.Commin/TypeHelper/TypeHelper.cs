using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Starter.Common.TypeHelper
{
    public class TypeHelper : ITypeHelper
    {
        public IEnumerable<Type> GetAllBaseTypes(string typeName, Assembly assembly = null)
        {
            var result = new List<Type>();

            assembly = assembly ?? GetAssemblyWithType(typeName);

            var type = assembly.GetTypes().FirstOrDefault(x => x.Name == typeName);

            while (type.BaseType != null && type.BaseType != typeof(object))
            {
                result.Add(type.BaseType);
                type = type.BaseType;
            }
            return result;
        }

        private Assembly GetAssemblyWithType(string typeName)
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetTypes().Any(t => t.Name == typeName));
        }

        public IEnumerable<string> GetPropertiesNames(string typeName)
        {
            var type = GetAssemblyWithType(typeName).GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetProperties().Select(x => x.Name);
        }

        public IEnumerable<PropertyInfo> GetProperties(string typeName)
        {
            var type = GetAssemblyWithType(typeName).GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetProperties().ToList();
        }

        public IEnumerable<PropertyInfo> GetConcreteProperties(string typeName)
        {
            var type = GetAssemblyWithType(typeName).GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).ToList();
        }
    }
}