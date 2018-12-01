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
            SqlCommand command = new SqlCommand($"select * from {tableName} @innerJoin @outerJoin where {tableName}.Id=@id");
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
            var objType = _typeTransformer.Transform(obj.GetType().Name);
            var baseTypes = GetParentTables(objType);
            baseTypes.Reverse();
            baseTypes.Add(objType);

            var builder = new StringBuilder();

            foreach (var table in baseTypes)
            {
                builder.AppendLine($"update {table} set ");
                var tableProperties = _typeHelper.GetProperties(_typeTransformer.TransformToTypeName(table));
                var idProperty = tableProperties.FirstOrDefault(x => x.Name == "Id");
                foreach (var property in tableProperties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        builder.AppendLine($"{property.Name}='{property.GetValue(obj).ToString()}', ");
                    }
                    else
                    {
                        builder.AppendLine($"{property.Name}={property.GetValue(obj).ToString()}, ");
                    }
                }
                builder.Remove(builder.ToString().LastIndexOf(","), 1);
                builder.Append($"where Id={idProperty.GetValue(obj)};");
            }

            return new SqlCommand(builder.ToString());
        }

        public SqlCommand Insert<T>(T obj, int id)
        {
            var objType = _typeTransformer.Transform(obj.GetType().Name);
            var baseTypes = GetParentTables(objType);
            baseTypes.Reverse();
            baseTypes.Add(objType);

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < baseTypes.Count; i++)
            {
                var fields = _typeHelper.GetConcreteProperties(_typeTransformer.TransformToTypeName(baseTypes[i]));

                fields = fields.Except(fields.Where(x => x.Name == "Id"));

                var fieldNames = fields.Select(x => x.Name).Except(fields.Where(x => x.Name == "Id").Select(x => x.Name));

                var values = GenerateValueRow(obj, fields);

                stringBuilder.AppendLine($"insert into {baseTypes[i]} (Id,{string.Join(',', fieldNames)}) values ({id},{values});");
            }

            return new SqlCommand(stringBuilder.ToString());
        }

        private string GenerateValueRow<T>(T obj, IEnumerable<PropertyInfo> fields)
        {
            if (!fields.Any())
            {
                return null;
            }
            var result = "";
            foreach (var field in fields)
            {
                if (field.PropertyType == typeof(string))
                {
                    result += $"'{field.GetValue(obj)}',";
                }
                else
                {
                    result += $"{field.GetValue(obj).ToString()},";
                }
            }
            return result.TrimEnd(',');
        }

        public SqlCommand GetTableIdentitytValue(string tableName)
        {
            return new SqlCommand($"select MAX(\"Id\") from {tableName}");
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