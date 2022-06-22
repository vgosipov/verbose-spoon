using System.Collections.Generic;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace RestApiDatabase.Objects
{
    public class MySqlDatabaseConnection
    {
        private readonly string connectionString;

        public MySqlDatabaseConnection(IConfiguration configuration)
        {
            connectionString = configuration.ConnectionString;
        }

        public IEnumerable<T> ExecuteQuery<T>(string query, object param = null, int? commandTimeout = null)
        {
            using var connection = CreateConnection();
            connection.Open();
            return connection.Query<T>(query, param, commandTimeout: commandTimeout);
        }

        public int ExecuteQuery(string query, object param = null, int? commandTimeout = null)
        {
            using var connection = CreateConnection();
            connection.Open();
            return connection.Execute(query, param, commandTimeout: commandTimeout);
        }

        private IDbConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
