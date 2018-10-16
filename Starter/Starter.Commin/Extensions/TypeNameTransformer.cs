using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;

namespace Starter.Common.Extensions
{
    public class TypeNameTransformer : ITypeTransformer
    {
        private readonly TypeNameTransformOptions _options;

        public TypeNameTransformer(IOptions<TypeNameTransformOptions> options)
        {
            _options = options.Value;
        }

        public string Transform<T>()
        {
            return typeof(T).Name.Remove(typeof(T).Name.IndexOf(_options.RemoveLast));
        }
        public string Transform(string name)
        {
            return name.Remove(name.IndexOf(_options.RemoveLast));
        }

        public string TransformToTypeName(string name)
        {
            return $"{name}{_options.RemoveLast}";
        }
    }
}