using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public abstract class LogEntryVisitorBase:ILogEntryVisitor
    {
        public virtual void Visit(ExceptionLogEntry exceptionLogEntry)
        { }

        public virtual void Visit(SimpleLogEntry simpleLogEntry)
        { }

        public virtual void Visit(LogEntry logEntry)
        {
            
        }
    }

    public class DatabaseExceptionLogEntrySaver
    {
        public void SaveLogEntry(LogEntry logEntry)
        {
            logEntry.Accept(new ExceptionLogEntryVisitor(this));
        }

        private void SaveException(ExceptionLogEntry exceptionLogEntry) { }

        private class ExceptionLogEntryVisitor : LogEntryVisitorBase
        {
            private readonly DatabaseExceptionLogEntrySaver _parent;
            
            public ExceptionLogEntryVisitor(DatabaseExceptionLogEntrySaver parent)
            {
                _parent = parent;
            }

            public override void Visit(ExceptionLogEntry exceptionLogEntry)
            {
                _parent.SaveException(exceptionLogEntry);
            }
        }
    }
}
