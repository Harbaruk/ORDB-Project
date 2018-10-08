using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Starter.Common.TypeHelper
{
    public interface ITypeHelper
    {
        IEnumerable<Type> GetAllBaseTypes(string typeName, Assembly assembly = null);
        IEnumerable<string> GetFieldNames(string type);
        IEnumerable<FieldInfo> GetFields(string type);
    }
}