using System;
using System.Collections.Generic;
using System.Linq;
using Starter.ADOProvider.CommandBuilder;
using Starter.ADOProvider.CommandBuilder.Models;
using Starter.ADOProvider.CommandExecutor;
using Starter.Common.Extensions;

namespace Starter.DAL.Infrastructure.ADORepository
{
    public class ADORepository<T> : IADORepository<T> where T : class, new()
    {
        private readonly ICommandBuilder _builder;
        private readonly ICommandExecutor<T> _commandExecutor;
        private readonly ITypeTransformer _transformer;

        public ADORepository(ICommandBuilder builder, ICommandExecutor<T> commandExecutor, ITypeTransformer transformer)
        {
            _builder = builder;
            _commandExecutor = commandExecutor;
            _transformer = transformer;
        }

        public void DeleteById(int id)
        {
            var type = typeof(T);
            var command = _builder.DeleteById(_transformer.Transform<T>(), id);
            _commandExecutor.Execute(command);
        }

        public T GetById(int id)
        {
            var command = _builder.GetById(_transformer.Transform<T>(), id);
            return _commandExecutor.Execute(command).FirstOrDefault();
        }

        public IEnumerable<T> GetList(IEnumerable<(WhereClauseSqlModel, string separator)> commandParams)
        {
            var command = _builder.GetList(_transformer.Transform<T>(), commandParams);
            return _commandExecutor.Execute(command);
        }

        public void Insert(T obj)
        {
            var identityCommand = _builder.GetTableIdentitytValue(_transformer.Transform<T>());
            var id = _commandExecutor.ExecuteScalar(identityCommand) + 1;

            var command = _builder.Insert(obj, id);
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}