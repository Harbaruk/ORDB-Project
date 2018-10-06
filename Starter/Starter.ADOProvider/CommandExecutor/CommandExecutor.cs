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

        public IEnumerable<T> Execute(string command)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                var sqlcommand = new SqlCommand(command, connection);
                var reader = sqlcommand.ExecuteReader();
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

        public void ExecuteNonQuery(string command)
        {
            using (var connection = new SqlConnection(_options.ConnectionString))
            {
                var sqlcommand = new SqlCommand(command, connection);
                sqlcommand.ExecuteNonQuery();
            }
        }
    }
}