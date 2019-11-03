using CommandCalculator.Services;
using CommandCalculator.Validators;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace CommandCalculator.Tests
{
    [TestFixture]
    public class Program
    {
        private readonly Mock<IConsoleWriter> _consoleWriterMock;

        public Program()
        {
            _consoleWriterMock = new Mock<IConsoleWriter>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(_consoleWriterMock)
                .AddSingleton<IInstructionFileReader, InstructionFileReader>()
                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .AddSingleton<ICalculatorService, CalculatorService>()
                .BuildServiceProvider();

            CommandCalculator.Program.ServiceProvider = serviceProvider;
            CommandCalculator.Program.ConsoleWriter = _consoleWriterMock.Object;
        }

        [Test]
        public void ReadInstructionsShouldSuccessfullyProcessInstructionsAndWrite15()
        {
            CommandCalculator.Program.ReadInstructions("ex1.txt");

            _consoleWriterMock.Verify(i => i.WriteLine("15"), Times.Once);
        }

        [Test]
        public void ReadInstructionsShouldWriteAFileNotFoundExceptionMessageWhenFileNotFound()
        {
            CommandCalculator.Program.ReadInstructions("xxx.txt");

            _consoleWriterMock.Verify(i => i.WriteLine(It.Is<string>(str => str.Contains("Could not find file"))), Times.Once);
        }
    }
}
