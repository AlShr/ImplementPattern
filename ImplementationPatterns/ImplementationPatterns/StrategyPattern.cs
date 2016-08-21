using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;





namespace ImplementationPatterns
{
    public class LogProccesor
    {
        //принимает делегат который соответствует сигнатуре метода стратегии
        private readonly Func<IEnumerable, List<LogEntry>> _logImporter;
        public LogProccesor(Func<IEnumerable, List<LogEntry>> logImporter)
        {
            _logImporter = logImporter;
        }

        public void ProcessLogs(IEnumerable sequence)
        {
            foreach (var logEntry in _logImporter.Invoke(sequence))
            {
                SaveLogEntry(logEntry);
            }
        }

        public void SaveLogEntry(LogEntry logEntry)
        {
 
        }
    }

    public interface ILogReader
    {
        List<LogEntry> Read(IEnumerable sequence);
    }

    public class LogFileReader : ILogReader, IDisposable
    {
        private readonly string _logFileName;
        private readonly Action<string> _logEntrySubscriber;
        private readonly static TimeSpan CheckFileInterval = TimeSpan.FromSeconds(5);
        private readonly Timer _timer;

        public LogFileReader()
        {
 
        }
        
        #region Pattern Observer
        public LogFileReader(string logFileName, Action<string> logEntrySubscriber)
        {         
            Contract.Requires(File.Exists(logFileName));
            _logFileName = logFileName;
            _logEntrySubscriber = logEntrySubscriber;
            _timer = new Timer(new TimerCallback(CheckFile), null, CheckFileInterval, CheckFileInterval);
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        private void CheckFile(object obj)
        {
            foreach (var logEntry in ReadNewLogEntries())
            {
                _logEntrySubscriber(logEntry);
            }
        }

        #endregion

        #region Pattern Iterator
       
        private IEnumerable<string> ReadNewLogEntries()
        {
            foreach(var line in File.ReadAllLines(_logFileName))
            {
                yield return line;
            }
        }

        #endregion

        public List<LogEntry> Read(IEnumerable sequence)
        {
            IEnumerator enumerator = sequence.GetEnumerator();
            object current = null;
            try
            {
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    logs.Add(current as LogEntry);
                }

                return logs;
            }
            finally
            {
                IDisposable disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        List<LogEntry> logs = new List<LogEntry>();      
    }

    public class WindowsEventLogReader : ILogReader
    {
        public List<LogEntry> Read(IEnumerable sequence)
        {
            return new List<LogEntry>();
        }
    }
}
