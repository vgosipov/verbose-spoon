using System.Data;
using MySql.Data.MySqlClient;
using RestApiDatabase.Objects;

namespace RestApiDatabase.DatabaseObject
{
    public class MySqlDatabaseConnection : DatabaseConnectionBase
    {
        public MySqlDatabaseConnection(IConfiguration configuration) : base(configuration)
        {
            connectionString = configuration.ConnectionString;
        }

        protected override IDbConnection CreateConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
