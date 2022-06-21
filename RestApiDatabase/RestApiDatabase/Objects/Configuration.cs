using Newtonsoft.Json;
using System;

namespace RestApiDatabase.Objects
{
    public class Configuration : IConfiguration
    {
        private Configuration() { }

        public static Configuration Instance { get; private set; } = new Configuration();

        public string DatabaseServer { get; set; }

        public string DatabasePort { get; set; }

        public TimeSpan DatabaseConnectionTimeout { get; set; }

        public string DatabaseName { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string ApiBaseUrl { get; set; }

        public string ConnectionString => $"Server={DatabaseServer};Port={DatabasePort};database={DatabaseName};uid={UserId};Password={Password}";

        public Configuration ParseJson(string jsonText)
        {
            Instance = JsonConvert.DeserializeObject<Configuration>(jsonText);
            return Instance;
        }
    }
}
