using System;

namespace RestApiDatabase.Objects
{
    public interface IConfiguration
    {
        public string DatabaseServer { get; set; }

        public string DatabasePort { get; set; }

        public TimeSpan DatabaseConnectionTimeout { get; set; }

        public string DatabaseName { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string ApiBaseUrl { get; set; }

        public string ConnectionString { get; }
    }
}
