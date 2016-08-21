using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public interface ILogEntryVisitor
    {
        void Visit(LogEntry logEntry);
        void Visit(ExceptionLogEntry exceptionLogEntry);
        void Visit(SimpleLogEntry simpleLogEntry);
    }

    public class DatabaseLogSaver : ILogEntryVisitor
    {
        public void SaveLogEntry(LogEntryBase logEntry)
        {
            logEntry.Accept(this);
        }

        void ILogEntryVisitor.Visit(ExceptionLogEntry exceptionLogEntry)
        {
            SaveException(exceptionLogEntry);
        }

        void ILogEntryVisitor.Visit(SimpleLogEntry simpleLogEntry)
        {
            SaveSimpleLogEntry(simpleLogEntry);
        }

        void ILogEntryVisitor.Visit(LogEntry logEntry)
        {
            SaveLogEntry(logEntry);
        }
        private void SaveSimpleLogEntry(SimpleLogEntry logEntry) { }
        private void SaveException(ExceptionLogEntry exceptionLogEntry) { }
        private void SaveLogEntry(LogEntry logEntry) { }

        #region Functional Observer
        public void SaveLogEntryBase(LogEntryBase logEntry) 
        { 
            logEntry.Match(
                ex => SaveException(ex), 
                simple => SaveSimpleLogEntry(simple)
                );
        }
        #endregion
    }

    public class ExceptionLogEntry :LogEntryBase
    {
        public override void Accept(ILogEntryVisitor logEntryVisitor)
        {
            logEntryVisitor.Visit(this);
        }
    }

    public class SimpleLogEntry :LogEntryBase
    {
        public override void Accept(ILogEntryVisitor logEntryVisitor)
        {
            logEntryVisitor.Visit(this);
        }
    }
}
