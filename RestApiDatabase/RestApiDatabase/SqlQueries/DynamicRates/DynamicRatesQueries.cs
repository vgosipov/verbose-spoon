using RestApiDatabase.DatabaseObject;
using RestApiDatabase.Model;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace RestApiDatabase.SqlQueries.DynamicRates
{
    public class DynamicRatesQueries : QueryObject
    {
        public DynamicRatesQueries(DatabaseConnectionBase connection) : base(connection)
        {
        }

        public int InsertRow(RateShortModel rateShortModel)
        {
            var query = File.ReadAllText("SqlQueries/DynamicRates/Insert.sql", Encoding.UTF8);
            return connection.ExecuteQuery(query, new
            {
                CurId        = rateShortModel.CurId,
                Date         = rateShortModel.Date.ToString(Constants.DateTimeFormat),
                DateFormat   = Constants.DatabaseDateTimeFormat,
                OfficialRate = rateShortModel.OfficialRate
            });
        }

        public DateTime? GetMaxDate()
        {
            var query = File.ReadAllText("SqlQueries/DynamicRates/MaxDate.sql", Encoding.UTF8);
            return connection.ExecuteQuery<DateTime?>(query).SingleOrDefault();
        }
    }
}
