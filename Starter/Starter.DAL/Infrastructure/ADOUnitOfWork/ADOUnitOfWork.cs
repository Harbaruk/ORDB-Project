using System;
using System.Collections.Generic;
using System.Text;
using Starter.ADOProvider.CommandBuilder;
using Starter.ADOProvider.CommandExecutor;
using Starter.Common.TypeActivator;
using Starter.Common.TypeHelper;
using Starter.DAL.Infrastructure.ADORepository;

namespace Starter.DAL.Infrastructure.ADOUnitOfWork
{
    public class ADOUnitOfWork : IADOUnitOfWork
    {
        private Dictionary<string, object> _repositories;
        private readonly IServiceProvider _serviceProvider;

        public ADOUnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IADORepository<T> Repository<T>() where T : class, new()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = _serviceProvider.GetService(typeof(IADORepository<T>));
                _repositories.Add(type, repositoryInstance);
            }
            return (ADORepository<T>)_repositories[type];
        }
    }
}