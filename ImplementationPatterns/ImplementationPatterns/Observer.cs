using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public class LogEntryEventArgs:EventArgs
    {
        public LogEntryEventArgs(string logEntry)
        {
            _logEntry = logEntry;
        }
        private string _logEntry;
        public string LogEntry { get; internal set; }
    }

    public class LogFileReader2 
    {
        private readonly string _logFileName;

        public LogFileReader2(string logFileName)
        {
            _logFileName = logFileName;
        }

        public event EventHandler<LogEntryEventArgs> OnNewLogEntry;

        private void CheckFile()
        {
            foreach (var logEntry in ReadNewLogEntries())
            {
                RaiseNewLogEntry(logEntry);
            }
        }

        private void RaiseNewLogEntry(string logEntry)
        {
            var handler = OnNewLogEntry;
            if (handler != null)
            {
                handler(this, new LogEntryEventArgs(logEntry));
            }
        }

        #region Pattern Iterator

        private IEnumerable<string> ReadNewLogEntries()
        {
            foreach (var line in File.ReadAllLines(_logFileName))
            {
                yield return line;
            }
        }

        #endregion
     }
}
