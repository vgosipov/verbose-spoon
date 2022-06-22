using NUnit.Framework;
using RestApiDatabase.Objects;
using System.IO;
using RestApiDatabase.Api;
using System.Text;
using System;
using RestApiDatabase.Utils;

namespace RestApiDatabase
{
    public class Tests
    {
        private OracleDatabaseConnection connection;
        private IConfiguration configuration;
        private NbrbApi nbrbApi;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var jsontext = File.ReadAllText("Configuration.json");
            configuration = Configuration.Instance.ParseJson(jsontext);
            connection = new OracleDatabaseConnection(configuration);
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
                var parentId = (currency.ParentId == null) ? "NULL" : currency.ParentId.ToString().ReplaceSingleQuote();
                var nameMulti = string.IsNullOrEmpty(currency.NameMulti) ? "NULL" : currency.NameMulti.ReplaceSingleQuote();
                var nameBelMulti = string.IsNullOrEmpty(currency.NameBelMulti) ? "NULL" : currency.NameBelMulti.ReplaceSingleQuote();
                var nameEngMulti = string.IsNullOrEmpty(currency.NameEngMulti) ? "NULL" : currency.NameEngMulti.ReplaceSingleQuote();

                var q = query + Environment.NewLine + $"('{currency.Id}', '{parentId.ReplaceSingleQuote()}', '{currency.Code.ReplaceSingleQuote()}', " +
                    $"'{currency.Abbreviation.ReplaceSingleQuote()}', '{currency.Name.ReplaceSingleQuote()}', '{currency.NameBel.ReplaceSingleQuote()}', " +
                    $"'{currency.NameEng.ReplaceSingleQuote()}', '{currency.QuotName.ReplaceSingleQuote()}', '{currency.QuotNameBel.ReplaceSingleQuote()}', '{currency.QuotNameEng.ReplaceSingleQuote()}', " +
                    $"'{nameMulti}', '{nameBelMulti}', '{nameEngMulti}', '{currency.Scale}', " +
                    $"'{currency.Periodicity}', TO_DATE('{currency.DateStart:yyyy-MM-dd HH:mm:ss}', 'yyyy-mm-dd hh24:mi:ss'), TO_DATE('{currency.DateEnd:yyyy-MM-dd HH:mm:ss}', 'yyyy-mm-dd hh24:mi:ss'))";
                
                int affectedRows = connection.ExecuteQuery(q);
                Assert.That(affectedRows, Is.GreaterThan(0), "Should insert some rows to database");
            }
        }
    }
}