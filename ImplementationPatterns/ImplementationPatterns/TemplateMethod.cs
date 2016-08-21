using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public abstract class LogReader
    {
        private int _currentPosition;

        //ReadLogEntry 
        public IEnumerable<LogEntry> ReadLogEntry()
        {
            return ReadEntries(ref _currentPosition).Select(ParseLogEntry);
        }

        //Прочитать новые записи с места последнего чтения
        protected abstract IEnumerable<string> ReadEntries(ref int position);

        //Разобрать их и вернуть вызывающему коду.
        protected abstract LogEntry ParseLogEntry(string stringEntry);
    }

    //Шаблонный метод на основе методов расширения
    public static class LogEntryEx
    {
        public static string GetText(this LogEntry logEntry)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("[{0}]", logEntry.DateTime)
                .AppendFormat("[{0}]", logEntry.Severity)
                .AppendLine(logEntry.Message)
                .AppendLine(logEntry.AdditionalInformation);

            return sb.ToString();
        }
    }

    public interface ILogParser
    {
        LogEntry ParseLogEntry(string stringEntry);
    }

    public abstract class LogFileReaderBase : LogReader, IDisposable
    {
        private readonly Lazy<Stream> _stream;
        private readonly ILogParser _logParser;

        protected LogFileReaderBase(string fileName)
        {
            _stream = new Lazy<Stream>(() => new FileStream(fileName, FileMode.Open));            
        }

        protected LogFileReaderBase(string fileName, ILogParser logParser)
        {
            _stream = new Lazy<Stream>(() => new FileStream(fileName, FileMode.Open));
            _logParser = logParser;
        }

        public void Dispose()
        {
            if (_stream.IsValueCreated)
            {
                _stream.Value.Close();
            }
        }

        protected override sealed IEnumerable<string> ReadEntries(ref int position)
        {
            Contract.Assert(_stream.Value.CanSeek);

            if (_stream.Value.Position != position)
                _stream.Value.Seek(position, SeekOrigin.Begin);

            return ReadLineByLine(_stream.Value, ref position);
        }

        protected override LogEntry ParseLogEntry(string stringEntry)
        {
            return _logParser.ParseLogEntry(stringEntry);
        }

        private IEnumerable<string> ReadLineByLine(Stream stream, ref int position)
        {
            // Построчное чтение из потока ввода/вывода
            return new List<string>();
        }

        protected virtual Stream OpenFileStream(string fileName)
        {
            return new FileStream(fileName, FileMode.Open);
        }
    }
}
