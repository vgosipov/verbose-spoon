using RestApiDatabase.Model;
using RestApiDatabase.Objects;
using System;

namespace RestApiDatabase.Api
{
    public class NbrbApi : ApiBase
    {
        public NbrbApi(string baseUrl) : base(new Uri(baseUrl))
        {
        }

        public CurrencyModel[] GetCurrencies()
        {
            var uri = $"/api/exrates/currencies";
            return Get<CurrencyModel[]>(uri);
        }

        public CurrencyModel GetCurrency(int currencyId)
        {
            var uri = $"/api/exrates/currencies/{currencyId}";
            return Get<CurrencyModel>(uri);
        }

        public RateShortModel[] GetRateShortModel(int currencyId, DateTime startDate, DateTime endDate)
        {
            var strStartDate = startDate.ToString("D", System.Globalization.CultureInfo.InvariantCulture);
            var strEndDate = endDate.ToString("D", System.Globalization.CultureInfo.InvariantCulture);
            var uri = $"/api/exrates/rates/dynamics/{currencyId}?startDate={strStartDate}&endDate={strEndDate}";
            return Get<RateShortModel[]>(uri);
        }
    }
}
