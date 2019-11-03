using CommandCalculator.Calculators;
using CommandCalculator.Converters;
using CommandCalculator.Readers;
using CommandCalculator.UIPresenters;
using CommandCalculator.Validators;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace CommandCalculator.Tests
{
    [TestFixture]
    public class Program
    {
        private readonly Mock<IUIPresenter> _consoleWriterMock;

        public Program()
        {
            _consoleWriterMock = new Mock<IUIPresenter>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(_consoleWriterMock)
                .AddSingleton<IInstructionConverter, InstructionConverter>()
                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .AddSingleton<IReader, FileReader>()
                .AddSingleton<ICalculator, SimpleCalculator>()
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
