using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Starter.ADOProvider.CommandBuilder
{
    public interface ICommandBuilder
    {
        // TODO: few Where clause with OR/AND separators
        // TODO: cascade inner join for ORDB approach
        string CreateCommand(string commandType, string tableName, (FilterType filter, string dst, object value) commandOperands);
    }

    public enum FilterType
    {
        Equals,
        NotEquals,
        GreaterThan,
        GreaterOrEqual,
        LessThen,
        LessOrEqual
    }
}