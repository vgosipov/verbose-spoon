using Oracle.ManagedDataAccess.Client;
using RestApiDatabase.Objects;
using System.Data;

namespace RestApiDatabase.DatabaseObject
{
    public class OracleDatabaseConnection : DatabaseConnectionBase
    {
        public OracleDatabaseConnection(IConfiguration configuration) : base(configuration)
        {
            var connectionStringBuilder = new OracleConnectionStringBuilder
            {
                Password = configuration.Password,
                UserID = configuration.UserId,
                DataSource = $"{configuration.DatabaseServer}:{configuration.DatabasePort}/{configuration.DatabaseName}",
                ConnectionTimeout = (int)configuration.DatabaseConnectionTimeout.TotalSeconds
            };
            connectionString = connectionStringBuilder.ConnectionString;
        }

        protected override IDbConnection CreateConnection()
        {
            return new OracleConnection(connectionString);
        }

    }
}
