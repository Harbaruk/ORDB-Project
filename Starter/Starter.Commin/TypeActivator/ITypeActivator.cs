using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starter.Common.TypeActivator
{
    public interface ITypeActivator<T>
    {
        T CreateType(IEnumerable<KeyValuePair<string, object>> properties);
    }
}