using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementationPatterns
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeProvider, ITradeParser tradeParser,
            ITradeStorage tradeStorage)
        {
            this.tradeProvider = tradeProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public void ProcessTrades()
        {
            var lines = tradeProvider.GetTradeData();
            var trades = tradeParser.Parse(lines);
            tradeStorage.Persist(trades);
        }
        private readonly ITradeDataProvider tradeProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }

    public interface ITradeDataProvider
    {
        IEnumerable<string> GetTradeData();
    }

    public interface ITradeParser
    {
        string[] Parse(IEnumerable<string> lines);
    }

    public interface ITradeStorage
    {
        void Persist(string[] trades);
    }

    public interface ITradeValidator
    { }

    public interface ITradeMapper
    { }

    public class StreamTradeDataProvider : ITradeDataProvider
    {
        public StreamTradeDataProvider(Stream stream)
        {
            this.stream = stream;
        }

        public IEnumerable<string> GetTradeData()
        {
            var tradeData = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tradeData.Add(line);
                }

            }
            return tradeData;
        }
        private readonly Stream stream;
    }

    public class SimpleTradeParser : ITradeParser
    {

        public string[] Parse(IEnumerable<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
