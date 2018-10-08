using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Starter.ADOProvider.CommandExecutor
{
    public interface ICommandExecutor<T> where T : new()
    {
        IEnumerable<T> Execute(SqlCommand command);
        int ExecuteScalar(SqlCommand command);
    }
}