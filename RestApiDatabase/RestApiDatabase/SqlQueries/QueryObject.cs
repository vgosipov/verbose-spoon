using RestApiDatabase.DatabaseObject;

namespace RestApiDatabase.SqlQueries
{
    public class QueryObject
    {
        protected DatabaseConnectionBase connection;

        public QueryObject(DatabaseConnectionBase connection)
        {
            this.connection = connection;
        }
    }
}
