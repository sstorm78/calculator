using System.IO;
using System.Linq;
using CommandCalculator.Readers;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Readers
{
    [TestFixture]
    public class FileReaderShould
    {
        private readonly IReader _sut;

        public FileReaderShould()
        {
            _sut = new FileReader();
        }

        [Test]
        public void ReadAsStringLinesShouldReturnFileContent()
        {
            var testFilename = "./testinstructionfiles/reader_valid.txt";

            var result = _sut.ReadAsStringLines(testFilename);

            result.Length.Should().Be(1);
            result.First().Should().Be("ok");
        }

        [Test]
        public void ReadAsStringLinesShouldThrowAnExceptionWhenFileNotFound()
        {
            var testFilename = "./testinstructionfiles/ddd.txt";

            var ex = Assert.Throws<FileNotFoundException>(() => _sut.ReadAsStringLines(testFilename));

            ex.Message.Should().Contain("Could not find file");
        }
    }
}
