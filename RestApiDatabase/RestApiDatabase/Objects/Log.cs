using NLog;

namespace RestApiDatabase.Objects
{
    public static class Log
    {
        public static readonly Logger TestLogger = LogManager.GetLogger("Logger");
    }
}
