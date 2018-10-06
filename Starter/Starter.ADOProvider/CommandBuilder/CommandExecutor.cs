using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.ADOProvider.CommandBuilder
{
    public class CommandBuilder : ICommandBuilder
    {
        public string CreateCommand(string commandType, string tableName, (FilterType filter, string dst, object value) commandOperands)
        {
            var tableNameString = Type.GetType(tableName);
            var filterSql = GetSqlRepresentation(commandOperands.filter);
            return null;
        }

        private string GetSqlRepresentation(FilterType filter)
        {
            switch (filter)
            {
                case FilterType.Equals:
                    return "=";

                case FilterType.GreaterOrEqual:
                    return ">=";

                case FilterType.GreaterThan:
                    return ">";

                case FilterType.LessOrEqual:
                    return "<=";

                case FilterType.LessThen:
                    return "<";

                case FilterType.NotEquals:
                    return "<>";

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}