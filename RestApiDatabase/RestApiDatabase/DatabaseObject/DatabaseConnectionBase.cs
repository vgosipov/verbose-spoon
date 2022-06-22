using Dapper;
using RestApiDatabase.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace RestApiDatabase.DatabaseObject
{
    public abstract class DatabaseConnectionBase
    {
        protected readonly IConfiguration configuration;
        protected string connectionString;

        public DatabaseConnectionBase(IConfiguration configuration)
        {
            this.configuration = configuration;
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

        public void SetTypeMapByColumnAttr<T>()
        {
            SqlMapper.SetTypeMap(typeof(T), new CustomPropertyTypeMap(
                typeof(T), (type, columnName) => type.GetProperties().FirstOrDefault(prop =>
                    prop.GetCustomAttributes(false).OfType<ColumnAttribute>().Any(attr
                        => attr.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase)) || prop.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase))));
        }

        protected abstract IDbConnection CreateConnection();
    }
}
