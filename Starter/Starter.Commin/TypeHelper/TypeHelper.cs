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
            return new List<Type>();
            var result = new List<Type>();
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }
            var type = assembly.GetTypes().FirstOrDefault(x => x.Name == typeName);

            while (type.BaseType != null || type.BaseType != typeof(object))
            {
                result.Add(type.BaseType);
                type = type.BaseType;
            }
            return result;
        }

        public IEnumerable<string> GetFieldNames(string typeName)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetFields().Select(x => x.Name);
        }

        public IEnumerable<FieldInfo> GetFields(string typeName)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == typeName);

            return type.GetFields().ToList();
        }
    }
}