using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.ADOProvider.CommandExecutor
{
    public interface ICommandExecutor<T> where T : new()
    {
        IEnumerable<T> Execute(string command);
        void ExecuteNonQuery(string command);
    }
}