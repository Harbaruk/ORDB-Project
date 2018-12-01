using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Starter.Common.TypeHelper
{
    public interface ITypeHelper
    {
        IEnumerable<Type> GetAllBaseTypes(string typeName, Assembly assembly = null);
        IEnumerable<string> GetPropertiesNames(string type);
        IEnumerable<PropertyInfo> GetProperties(string type);
        IEnumerable<PropertyInfo> GetConcreteProperties(string type);
    }
}