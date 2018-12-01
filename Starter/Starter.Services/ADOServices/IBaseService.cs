using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Services.ADOServices
{
    public interface IBaseService<T> where T : class, new()
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}