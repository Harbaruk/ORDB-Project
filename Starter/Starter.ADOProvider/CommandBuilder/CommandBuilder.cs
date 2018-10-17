using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Starter.ADOProvider.CommandBuilder.Models;
using Starter.Common.Extensions;
using Starter.Common.TypeHelper;

namespace Starter.ADOProvider.CommandBuilder
{
    public class CommandBuilder : ICommandBuilder
    {
        //TODO: Merge into one class
        private readonly ITypeHelper _typeHelper;

        private readonly ITypeTransformer _typeTransformer;

        public CommandBuilder(ITypeHelper typeHelper, ITypeTransformer typeTransformer)
        {
            _typeHelper = typeHelper;
            _typeTransformer = typeTransformer;
        }

        //TODO: make separator as enum
        //TODO: use StringBuilder
        public SqlCommand GetList(string tableName, IEnumerable<(WhereClauseSqlModel filter, string separator)> commandOperands)
        {
            var command = new SqlCommand($"select * from {tableName} @innerJoin @whereClause;");

            var baseTableNames = GetParentTables(tableName);

            command.CommandText = command.CommandText.Replace("@innerJoin", GenerateInnerJoin(tableName, baseTableNames));
            command.CommandText = command.CommandText.Replace("@whereClause", GenerateWhereClause(commandOperands));

            return command;
        }

        public SqlCommand DeleteById(string typeName, int id)
        {
            StringBuilder builder = new StringBuilder($"delete from {typeName} where Id=@id;");

            var baseTableNames = GetParentTables(typeName);
            foreach (var table in baseTableNames)
            {
                builder.AppendLine($"delete from {table} where Id=@id;");
            }
            var idParameter = new SqlParameter("@id", id);

            var command = new SqlCommand(builder.ToString());
            command.Parameters.Add(idParameter);

            return command;
        }

        public SqlCommand GetById(string tableName, int id)
        {
            SqlCommand command = new SqlCommand($"select * from {tableName} @innerJoin where {tableName}.Id=@id");
            var baseTableNames = GetParentTables(tableName);
            var join = GenerateInnerJoin(tableName, baseTableNames);

            var idParameter = new SqlParameter("@id", id);

            if (string.IsNullOrEmpty(join))
            {
                command.CommandText = command.CommandText.Replace("@innerJoin", "");
            }
            else
            {
                command.CommandText = command.CommandText.Replace("@innerJoin", join);
            }

            command.Parameters.Add(idParameter);

            return command;
        }

        public SqlCommand Update<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public SqlCommand Insert<T>(T obj, int id)
        {
            var baseTypes = GetParentTables(nameof(T));
            baseTypes.Reverse();
            baseTypes.Add(nameof(T));

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < baseTypes.Count; i++)
            {
                var fields = _typeHelper.GetFields(baseTypes[i]);

                var fieldNames = fields.Select(x => x.Name);

                stringBuilder.AppendLine($"insert into {baseTypes[i]} (@id,{string.Join(',', fieldNames)} values (@id,{String.Join(',', fields.Select(x => x.GetValue(x).ToString()))};");
            }
            return new SqlCommand(stringBuilder.ToString());
        }

        public SqlCommand GetTableIdentitytValue(string tableName)
        {
            return new SqlCommand($"select MAX(\"Id\") from {tableName})");
        }

        private string GenerateInnerJoin(string tableName, List<string> baseTableNames)
        {
            var stringBuilder = new StringBuilder();
            if (baseTableNames.Any())
            {
                //stringBuilder.Append(tableName);
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
            if (commandOperands != null && commandOperands.Any())
            {
                stringBuilder.Append(" where ");
                foreach (var command in commandOperands)
                {
                    stringBuilder.Append($" {command.filter.DestinationField} {GetSqlRepresentation(command.filter.Filter)} {command.filter.ComparisonValue} {command.separator ?? string.Empty} ");
                }
                return stringBuilder.ToString();
            }
            return string.Empty;
        }

        private List<string> GetParentTables(string tableName)
        {
            //TODO: inject IOptions
            return _typeHelper.GetAllBaseTypes(_typeTransformer.TransformToTypeName(tableName)).Select(x => _typeTransformer.Transform(x.Name)).ToList();
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