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

        public CurrencyModel[] GetCurrencies(int? currencyId = null)
        {
            var curId = (currencyId == null) ? null : $"/{currencyId}";
            var uri = $"/api/exrates/currencies{curId}";
            return Get<CurrencyModel[]>(uri);
        }
    }
}
