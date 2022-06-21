using NUnit.Framework;
using RestApiDatabase.Objects;
using System.IO;
using RestApiDatabase.Api;
using System.Text;
using System;

namespace RestApiDatabase
{
    public class Tests
    {
        private DatabaseConnection connection;
        private IConfiguration configuration;
        private NbrbApi nbrbApi;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var jsontext = File.ReadAllText("Configuration.json");
            configuration = Configuration.Instance.ParseJson(jsontext);
            connection = new DatabaseConnection(configuration);
            nbrbApi = new NbrbApi(configuration.ApiBaseUrl);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            var currencies = nbrbApi.GetCurrencies();
            var query = "INSERT INTO currency " +
                "(Cur_ID, Cur_ParentID, Cur_Code, Cur_Abbreviation, Cur_Name, Cur_Name_Bel, " +
                "Cur_Name_Eng, Cur_QuotName, Cur_QuotName_Bel, Cur_QuotName_Eng, Cur_NameMulti, " +
                "Cur_Name_BelMulti, Cur_Name_EngMulti, Cur_Scale, Cur_Periodicity, Cur_DateStart, Cur_DateEnd) " +
                "VALUES ";

            foreach (var currency in currencies)
            {
                var parentId = (currency.ParentId == null) ? "NULL" : currency.ParentId.ToString();
                var nameBelMulti = (currency.NameBelMulti == null) ? "NULL" : currency.NameBelMulti.ToString();
                var nameEngMulti = (currency.NameEngMulti == null) ? "NULL" : currency.NameEngMulti.ToString();

                var q = query + Environment.NewLine + $"('{currency.Id}', '{parentId}', '{currency.Code}', " +
                    $"'{currency.Abbreviation}', '{currency.Name}', '{currency.NameBel}', " +
                    $"'{currency.NameEng}', '{currency.QuotName}', '{currency.QuotNameBel}', '{currency.QuotNameEng}', " +
                    $"'{currency.NameMulti}', '{nameBelMulti}', '{nameEngMulti}', '{currency.Scale}', " +
                    $"'{currency.Periodicity}', '{currency.DateStart:yyyy-MM-dd HH:mm:ss}', '{currency.DateEnd:yyyy-MM-dd HH:mm:ss}')";
                
                int affectedRows = connection.ExecuteQuery(q);
                Assert.That(affectedRows, Is.GreaterThan(0), "Should insert some rows to database");
            }
        }
    }
}