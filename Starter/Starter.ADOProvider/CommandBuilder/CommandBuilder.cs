using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Starter.ADOProvider.CommandBuilder.Models;
using Starter.Common.TypeHelper;

namespace Starter.ADOProvider.CommandBuilder
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly ITypeHelper _typeHelper;

        public CommandBuilder(ITypeHelper typeHelper)
        {
            _typeHelper = typeHelper;
        }

        //TODO: make separator as enum
        public SqlCommand GetList(string tableName, IEnumerable<(WhereClauseSqlModel filter, string separator)> commandOperands)
        {
            var command = new SqlCommand("select * from @tableName @innerJoin where @whereClause");

            var baseTableNames = GetParentTables(tableName);

            var tableNameParameter = new SqlParameter("@tableName", tableName);
            var innerJoinParameter = new SqlParameter("@innerJoin", GenerateInnerJoin(tableName, baseTableNames));
            var whereClauseParameter = new SqlParameter("@whereClause", GenerateWhereClause(commandOperands));

            command.Parameters.Add(tableNameParameter);
            command.Parameters.Add(innerJoinParameter);
            command.Parameters.Add(whereClauseParameter);

            return command;
        }

        public SqlCommand DeleteById(string typeName, int id)
        {
            SqlCommand command = new SqlCommand("delete from @table where Id=@id");
            var baseTableNames = GetParentTables(typeName);

            var innerJoinParameter = new SqlParameter("@innerJoin", GenerateInnerJoin(typeName, baseTableNames));
            var idParameter = new SqlParameter("@id", id);

            command.Parameters.Add(innerJoinParameter);
            command.Parameters.Add(idParameter);

            return command;
        }

        public SqlCommand GetById(string tableName, int id)
        {
            SqlCommand command = new SqlCommand($"select * from {tableName} @innerJoin where Id=@id");
            var baseTableNames = GetParentTables(tableName);
            var join = GenerateInnerJoin(tableName, baseTableNames);

            var idParameter = new SqlParameter("@id", id);

            if (string.IsNullOrEmpty(join))
            {
                command.CommandText = command.CommandText.Replace("@innerJoin", "");
            }
            else
            {
                var innerJoinParameter = new SqlParameter("@innerJoin", GenerateInnerJoin(tableName, baseTableNames));
                command.Parameters.Add(innerJoinParameter);
            }

            command.Parameters.Add(idParameter);

            return command;
        }

        public SqlCommand Update<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public SqlCommand Insert<T>(T obj)
        {
            var baseTypes = GetParentTables(nameof(T));
            baseTypes.Reverse();
            baseTypes.Add(nameof(T));

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < baseTypes.Count; i++)
            {
                var fields = _typeHelper.GetFields(baseTypes[i]);
                var idField = fields.FirstOrDefault(x => x.Name == "Id");
                fields = fields.Except(new List<FieldInfo> { idField });
                var fieldNames = fields.Select(x => x.Name).Except(new List<string> { "Id" });

                stringBuilder.Append($"insert into {baseTypes[i]} (Id,{string.Join(',', fieldNames)} values (@id,{fields.Select(x => x.GetValue(x).ToString())};");
            }
            return new SqlCommand(stringBuilder.ToString());
        }

        private string GenerateInnerJoin(string tableName, List<string> baseTableNames)
        {
            var stringBuilder = new StringBuilder();
            if (baseTableNames.Any())
            {
                stringBuilder.Append(tableName);
                foreach (var name in baseTableNames)
                {
                    stringBuilder.Append($" inner join {name} on {name}.Id = {tableName}.Id ");
                }
            }
            return stringBuilder.ToString();
        }

        private string GenerateWhereClause(IEnumerable<(WhereClauseSqlModel filter, string separator)> commandOperands)
        {
            var stringBuilder = new StringBuilder();
            foreach (var command in commandOperands)
            {
                stringBuilder.Append($" {command.filter.DestinationField} {GetSqlRepresentation(command.filter.Filter)} {command.filter.ComparisonValue} {command.separator ?? string.Empty} ");
            }
            return stringBuilder.ToString();
        }

        private List<string> GetParentTables(string tableName)
        {
            return _typeHelper.GetAllBaseTypes(tableName).Select(x => x.Name).ToList();
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