using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Options;
using Starter.Common.TypeActivator;

namespace Starter.ADOProvider.CommandExecutor
{
    public class CommandExecutor<T> : ICommandExecutor<T> where T : new()
    {
        private readonly ADOConnectionOptions _options;
        private readonly ITypeActivator<T> _typeActivator;

        public CommandExecutor(ITypeActivator<T> typeActivator, IOptions<ADOConnectionOptions> options)
        {
            _options = options.Value;
            _typeActivator = typeActivator;
        }

        public IEnumerable<T> Execute(SqlCommand command)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                using (reader)
                {
                    List<string> fieldNames = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fieldNames.Add(reader.GetName(i));
                    }

                    while (reader.Read())
                    {
                        var itemsToActivate = new List<KeyValuePair<string, object>>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            itemsToActivate.Add(new KeyValuePair<string, object>(fieldNames[i], reader[i]));
                        }
                        result.Add(_typeActivator.CreateType(itemsToActivate));
                    }
                }
                return result;
            }
        }

        public int ExecuteScalar(SqlCommand command)
        {
            var tableName = command.Parameters["@tableName"];
            var newId = GetTableIdentity(tableName);

            command.Parameters.Add(new SqlParameter("@id", newId));
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                command.Connection = connection;
                return (int)command.ExecuteScalar();
            }
        }

        private int GetTableIdentity(SqlParameter tableName)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                var command = new SqlCommand($"select count(*) from {tableName.Value.ToString()}", connection);
                return (int)command.ExecuteScalar();
            }
        }
    }
}