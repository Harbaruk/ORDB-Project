using System;
using System.Collections.Generic;
using System.Text;
using Starter.DAL.Infrastructure.ADORepository;

namespace Starter.DAL.Infrastructure.ADOUnitOfWork
{
    public interface IADOUnitOfWork
    {
        IADORepository<T> Repository<T>() where T : class, new();
    }
}