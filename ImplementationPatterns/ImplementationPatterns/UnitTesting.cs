using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace ImplementationPatterns
{
    class FakeLogFileReader:LogFileReaderBase
    {
        private readonly MemoryStream _mockStream;

        public FakeLogFileReader(MemoryStream mockStream)
            : base(string.Empty)
        {
            _mockStream = mockStream;
        }

        protected override Stream OpenFileStream(string fileName)
        {
            return _mockStream;
        }

    }

    [TestFixture]
    public class TestFakedMemoryStream
    {
        
        [Test]
        public void TestFakedMemoryStreamProvidedOneElement()
        { 
            //Arrange
            LogFileReaderBase cut = new FakeLogFileReader(GetMemoryStreamWithOneElement());

            //Act
            var logEntries = cut.ReadLogEntry();

            //Assert
            Assert.That(logEntries.Count(), Is.EqualTo(1));
        }

        private MemoryStream GetMemoryStreamWithOneElement()
        {
            return new MemoryStream();
        }
    }
}
