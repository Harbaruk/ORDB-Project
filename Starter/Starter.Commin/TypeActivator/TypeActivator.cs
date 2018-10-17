using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starter.Common.TypeActivator
{
    public class TypeActivator<T> : ITypeActivator<T> where T : new()
    {
        public T CreateType(IEnumerable<KeyValuePair<string, object>> properties)
        {
            var resultObject = Activator.CreateInstance<T>();

            var fields = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var fieldToSet = fields.FirstOrDefault(x => x.Name == property.Key);
                if (fieldToSet != null)
                {
                    if (string.IsNullOrEmpty(property.Value.ToString()))
                    {
                        var isValueType = fieldToSet.PropertyType.IsValueType;
                        fieldToSet.SetValue(resultObject, isValueType ? Activator.CreateInstance(fieldToSet.PropertyType) : null);
                    }
                    else
                    {
                        fieldToSet.SetValue(resultObject, property.Value);
                    }
                }
            }
            return resultObject;
        }
    }
}