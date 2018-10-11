using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.DAL.Extensions
{
    //TODO: configurable substring for removal
    public class TypeNameTransformer : ITypeTransformer
    {
        public string Transform<T>()
        {
            return typeof(T).Name.Remove(typeof(T).Name.IndexOf("Entity"));
        }
    }
}