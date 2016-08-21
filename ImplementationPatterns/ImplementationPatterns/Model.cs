using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public class LogEntry : LogEntryBase
    {
        public static LogEntry Parse(string line)
        {
            return new LogEntry()
            {
                DateTime = DateTime.Now,
                Severity = "",
                Message = line
            };
        }
        public override void Accept(ILogEntryVisitor logEntryVisitor)
        {
            logEntryVisitor.Visit(this);
        }
    }

    public abstract class LogEntryBase
    {
        public DateTime DateTime { get; internal set; }
        public string Severity { get; internal set; }
        public string Message { get; internal set; }

        // ExceptionLogEntry будет возвращать информацию об исключении
        public string AdditionalInformation { get; internal set; }
        public abstract void Accept(ILogEntryVisitor logEntryVisitor);
        public void Match(
            Action<ExceptionLogEntry> exceptionEntryMatch,
            Action<SimpleLogEntry> simpleEntryMatch)
        {
            var exceptionLogEntry = this as ExceptionLogEntry;
            if (exceptionLogEntry != null)
            {
                exceptionEntryMatch(exceptionLogEntry);
                return;
            }

            var simpleLogEntry = this as SimpleLogEntry;
            if (simpleLogEntry != null)
            {
                simpleEntryMatch(simpleLogEntry);
                return;
            }

            throw new InvalidOperationException("Unknow LogEntry type");
        }
    }
}
