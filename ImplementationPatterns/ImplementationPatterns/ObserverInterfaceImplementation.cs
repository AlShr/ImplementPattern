using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public interface ILogFileReaderObserver
    {
        void NewLogEntry(string logEntry);
        void FileWasRolled(string oldLogFile, string newLogFile);
    }

    public class LogFileReader3
    {
        private readonly ILogFileReaderObserver _observer;
        private readonly string _logFileName;

        public LogFileReader3(string logFileName, ILogFileReaderObserver observer)
        {
            _logFileName = logFileName;
            _observer = observer;
        }

        private void DetectThatNewFileWasCreated()
        {
            if (NewLogFileWasCreated())
                _observer.FileWasRolled(_logFileName, GetNewLogFileName()); 
        }

        private string GetNewLogFileName()
        {
            return String.Empty;
        }

        private bool NewLogFileWasCreated()
        {
            return true;
        }
    }
}
