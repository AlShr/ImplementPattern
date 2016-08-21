using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public class LogSaver
    {
        private readonly DbProviderFactory _factory;

        public LogSaver(DbProviderFactory factroy)
        {
            _factory = factroy;
        }

        public void Save(IEnumerable<LogEntry> logEntries)
        {
            using (var connection = _factory.CreateConnection())
            {
                SetConnectionString(connection);
                using (var command = _factory.CreateCommand())
                {
                    SetCommandArguments(logEntries);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SetConnectionString(DbConnection connection)
        { }

        private void SetCommandArguments(IEnumerable<LogEntry> logEntries)
        { }
    }
}
