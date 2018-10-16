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

        public IEnumerable<string> GetFieldNames(string typeName)
        {
            var type = GetAssemblyWithType(typeName).GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetFields().Select(x => x.Name);
        }

        public IEnumerable<FieldInfo> GetFields(string typeName)
        {
            var type = GetAssemblyWithType(typeName).GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetFields().ToList();
        }
    }
}