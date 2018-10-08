using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Starter.ADOProvider.CommandBuilder.Models;

namespace Starter.ADOProvider.CommandBuilder
{
    public interface ICommandBuilder
    {
        // TODO: few Where clause with OR/AND separators
        // TODO: cascade inner join for ORDB approach
        SqlCommand GetById(string tableName, int id);
        SqlCommand Update<T>(T obj);
        SqlCommand DeleteById(string typeName, int id);
        SqlCommand GetList(string tableName, IEnumerable<(WhereClauseSqlModel filter, string separator)> commandOperands);
        SqlCommand Insert<T>(T obj);
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