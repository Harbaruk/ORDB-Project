using System;
using System.Collections.Generic;
using System.Text;
using Starter.ADOProvider.CommandBuilder.Models;

namespace Starter.DAL.Infrastructure.ADORepository
{
    public interface IADORepository<T> where T : class, new()
    {
        void Insert(T obj);
        IEnumerable<T> GetList(IEnumerable<(WhereClauseSqlModel, string separator)> command);
        T GetById(int id);
        void Update(T obj);
        void DeleteById(int id);
    }
}