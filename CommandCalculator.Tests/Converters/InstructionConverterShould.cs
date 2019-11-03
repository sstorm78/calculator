using System;
using CommandCalculator.Converters;
using CommandCalculator.Models;
using CommandCalculator.Readers;
using CommandCalculator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Converters
{
    [TestFixture]
    public class InstructionConverterShould
    {
        private readonly IInstructionConverter _sut;
        private readonly IReader _reader;

        public InstructionConverterShould()
        {
            var validator = new InstructionValidator();
            _reader = new FileReader();

            _sut = new InstructionConverter(validator);
        }

        [Test]
        public void ConvertIntoListOfInstructionsShouldReturnValidListOfInstructions()
        {
            var testFilename = "./testinstructionfiles/valid.txt";

            var content = _reader.ReadAsStringLines(testFilename);

            var result = _sut.ConvertIntoListOfInstructions(content);

            result[0].Action.Should().Be(InstructionActions.Add);
            result[0].Value.Should().Be(2);

            result[1].Action.Should().Be(InstructionActions.Multiply);
            result[1].Value.Should().Be(3);

            result[2].Action.Should().Be(InstructionActions.Apply);
            result[2].Value.Should().Be(3);
        }

        [Test]
        public void ConvertIntoListOfInstructionsShouldThrowAnExceptionWhenFileIsEmpty()
        {
            var testFilename = "testinstructionfiles/invalid_empty.txt";

            var content = _reader.ReadAsStringLines(testFilename);

            var ex = Assert.Throws<Exception>(() => _sut.ConvertIntoListOfInstructions(content));

            ex.Message.Should().Be(ExceptionMessages.FileIsInvalidOrEmptyExceptionMessage);
        }

        [Test]
        public void ConvertIntoListOfInstructionsShouldThrowAnExceptionWhenFileIsMalformed()
        {
            var testFilename = "testinstructionfiles/invalid_malformed.txt";

            var content = _reader.ReadAsStringLines(testFilename);

            var ex = Assert.Throws<Exception>(() => _sut.ConvertIntoListOfInstructions(content));

            ex.Message.Should().Be(string.Format(ExceptionMessages.ValidationMessageHeader, "Invalid instruction was found: xxx"));
        }

        [Test]
        public void ConvertIntoListOfInstructionsShouldThrowAnExceptionFileDoesNotContainApplyInstruction()
        {
            var testFilename = "testinstructionfiles/invalid_no_apply.txt";

            var content = _reader.ReadAsStringLines(testFilename);

            var ex = Assert.Throws<Exception>(() => _sut.ConvertIntoListOfInstructions(content));

            ex.Message.Should().Be(ExceptionMessages.FileMustContainApplyInstructionExceptionMessage);
        }

    }
}
