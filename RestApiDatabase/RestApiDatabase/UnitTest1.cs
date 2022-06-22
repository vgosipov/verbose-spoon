using NUnit.Framework;
using RestApiDatabase.Objects;
using System.IO;
using RestApiDatabase.Api;
using RestApiDatabase.Model;
using RestApiDatabase.DatabaseObject;

namespace RestApiDatabase
{
    public class Tests
    {
        private DatabaseConnectionBase connection;
        private IConfiguration configuration;
        private NbrbApi nbrbApi;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var jsontext = File.ReadAllText("Configuration.json");
            configuration = Configuration.Instance.ParseJson(jsontext);
            connection = new OracleDatabaseConnection(configuration);
            nbrbApi = new NbrbApi(configuration.ApiBaseUrl);
            connection.SetTypeMapByColumnAttr<CurrencyModel>();
            connection.SetTypeMapByColumnAttr<RateShortModel>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var filler = new DatabaseFiller(connection, nbrbApi);
            var currencies = filler.FillCurrencyTable();
            CollectionAssert.IsNotEmpty(currencies, "Currency shouldn't be empty");

            var succeeded = filler.FillDynamicRatesTable(currencies);
            Assert.IsTrue(succeeded, "Error occured during filling DynamicRates table");
        }


    }
}