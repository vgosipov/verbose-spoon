using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace RestApiDatabase.Objects
{
    public class OracleDatabaseConnection
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public OracleDatabaseConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionStringBuilder = new OracleConnectionStringBuilder
            {
                Password = configuration.Password,
                UserID = configuration.UserId,
                DataSource = $"{configuration.DatabaseServer}:{configuration.DatabasePort}/{configuration.DatabaseName}",
                ConnectionTimeout = (int)configuration.DatabaseConnectionTimeout.TotalSeconds
            };
            connectionString = connectionStringBuilder.ConnectionString;
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

        private OracleConnection CreateConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}
