using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Starter.ADOProvider.CommandBuilder;
using Starter.ADOProvider.CommandExecutor;
using Starter.Services.ADOServices.UserManagement.Models;

namespace Starter.Services.ADOServices.UserManagement
{
    public class ADOUserService : IADOUserManagement
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly ICommandExecutor<Users> _commandExecutor;

        public ADOUserService(ICommandBuilder commandBuilder, ICommandExecutor<Users> commandExecutor)
        {
            _commandBuilder = commandBuilder;
            _commandExecutor = commandExecutor;
        }

        public IEnumerable<Users> GetAll()
        {
            throw new NotImplementedException();
        }

        public Users GetById(int id)
        {
            var command = _commandBuilder.GetById(nameof(Users), id);
            return _commandExecutor.Execute(command).FirstOrDefault();
        }
    }
}