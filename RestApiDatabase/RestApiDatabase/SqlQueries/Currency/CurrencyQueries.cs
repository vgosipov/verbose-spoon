using RestApiDatabase.DatabaseObject;
using RestApiDatabase.Model;
using RestApiDatabase.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestApiDatabase.SqlQueries.Currency
{
    public class CurrencyQueries : QueryObject
    {
        public CurrencyQueries(DatabaseConnectionBase connection) : base(connection)
        {
        }

        public int InsertRow(CurrencyModel currency)
        {
            var parentId     = (currency.ParentId == null) ? "NULL" : currency.ParentId.ToString().ReplaceSingleQuote();
            var nameMulti    = string.IsNullOrEmpty(currency.NameMulti) ? "NULL" : currency.NameMulti.ReplaceSingleQuote();
            var nameBelMulti = string.IsNullOrEmpty(currency.NameBelMulti) ? "NULL" : currency.NameBelMulti.ReplaceSingleQuote();
            var nameEngMulti = string.IsNullOrEmpty(currency.NameEngMulti) ? "NULL" : currency.NameEngMulti.ReplaceSingleQuote();

            var query = File.ReadAllText("SqlQueries/Currency/Insert.sql", Encoding.UTF8);
            return connection.ExecuteQuery(query, new
            {
                Id = currency.Id,
                ParentId = parentId,
                Code            = currency.Code,
                Abbreviation    = currency.Abbreviation ,
                Name            = currency.Name         ,
                NameBel         = currency.NameBel      ,
                NameEng         = currency.NameEng      ,
                QuotName        = currency.QuotName     ,
                QuotNameBel     = currency.QuotNameBel  ,
                QuotNameEng     = currency.QuotNameEng  ,
                NameMulti       = nameMulti             ,
                NameBelMulti    = nameBelMulti          ,
                NameEngMulti    = nameEngMulti          ,
                Scale           = currency.Scale        ,
                Periodicity     = currency.Periodicity  ,
                DateStartFormat = Constants.DatabaseDateTimeFormat,
                DateEndFormat   = Constants.DatabaseDateTimeFormat,
                DateStart       = currency.DateStart.ToString(Constants.DateTimeFormat),
                DateEnd         = currency.DateEnd.ToString(Constants.DateTimeFormat)
            });
        }

        public int? IsEmpty()
        {
            var query = File.ReadAllText("SqlQueries/Currency/IsEmpty.sql", Encoding.UTF8);
            return connection.ExecuteQuery<int?>(query).SingleOrDefault();
        }

        public IEnumerable<CurrencyModel> SelectAll()
        {
            var query = File.ReadAllText("SqlQueries/Currency/SelectAll.sql", Encoding.UTF8);
            return connection.ExecuteQuery<CurrencyModel>(query);
        }
    }
}
