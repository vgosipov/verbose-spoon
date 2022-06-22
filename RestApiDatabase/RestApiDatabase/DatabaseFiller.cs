using RestApiDatabase.Api;
using RestApiDatabase.DatabaseObject;
using RestApiDatabase.Model;
using RestApiDatabase.SqlQueries.Currency;
using RestApiDatabase.SqlQueries.DynamicRates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestApiDatabase
{
    public class DatabaseFiller
    {
        private readonly DatabaseConnectionBase connection;
        private readonly NbrbApi nbrbApi;
        private readonly CurrencyQueries currencyQueries;
        private readonly DynamicRatesQueries dynamicRatesQueries;

        public DatabaseFiller(DatabaseConnectionBase connection, NbrbApi nbrbApi)
        {
            this.connection = connection;
            this.nbrbApi = nbrbApi;
            this.currencyQueries = new CurrencyQueries(connection);
            this.dynamicRatesQueries = new DynamicRatesQueries(connection);
        }

        public IEnumerable<CurrencyModel> FillCurrencyTable()
        {
            var dbRows = currencyQueries.SelectAll().ToList();
            if (dbRows.Count > 0)
                return dbRows;

            var currencies = nbrbApi.GetCurrencies();

            foreach (var currency in currencies)
            {
                var affectedRows = currencyQueries.InsertRow(currency);
                if (affectedRows == 0) return null;
            }
            return currencies;
        }

        public bool FillDynamicRatesTable(IEnumerable<CurrencyModel> currencies)
        {
            var defaultStartTime  = DateTime.Today.AddDays(-30);
            var lastCurrencySent  = dynamicRatesQueries.GetMaxDate();
            var startDate         = (lastCurrencySent is null || lastCurrencySent.Value < defaultStartTime) ? defaultStartTime : lastCurrencySent.Value;
            if (startDate >= DateTime.Today)
                return true;

            foreach (var currency in currencies)
            {
                var rates = nbrbApi.GetRateShortModel(currency.Id, startDate, DateTime.Today);
                foreach (var rate in rates)
                {
                    int affectedRows = dynamicRatesQueries.InsertRow(rate);
                    if (affectedRows == 0) return false;
                }
            }
            return true;
        }
    }
}
