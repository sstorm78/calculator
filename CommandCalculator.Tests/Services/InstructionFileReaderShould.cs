using System;
using System.IO;
using CommandCalculator.Models;
using CommandCalculator.Services;
using CommandCalculator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Services
{
    [TestFixture]
    public class InstructionFileReaderShould
    {
        private readonly IInstructionFileReader _sut;

        public InstructionFileReaderShould()
        {
            var validator = new InstructionValidator();
            _sut = new InstructionFileReader(validator);
        }

        [Test]
        public void ReadFileAsListOfInstructionsShouldReturnValidListOfInstructions()
        {
            var testFilename = "testinstructionfiles/valid.txt";

            var result = _sut.ReadFileAsListOfInstructions(testFilename);

            result[0].Action.Should().Be(InstructionActions.Add);
            result[0].Value.Should().Be(2);

            result[1].Action.Should().Be(InstructionActions.Multiply);
            result[1].Value.Should().Be(3);

            result[2].Action.Should().Be(InstructionActions.Apply);
            result[2].Value.Should().Be(3);
        }

        [Test]
        public void ReadFileAsListOfInstructionsShouldThrowAnExceptionWhenFileIsEmpty()
        {
            var testFilename = "testinstructionfiles/invalid_empty.txt";

            var ex = Assert.Throws<Exception>(() => _sut.ReadFileAsListOfInstructions(testFilename));

            ex.Message.Should().Be(ExceptionMessages.FileIsInvalidOrEmptyExceptionMessage);
        }

        [Test]
        public void ReadFileAsListOfInstructionsShouldThrowAnExceptionWhenFileIsMalformed()
        {
            var testFilename = "testinstructionfiles/invalid_malformed.txt";

            var ex = Assert.Throws<Exception>(() => _sut.ReadFileAsListOfInstructions(testFilename));

            ex.Message.Should().Be(string.Format(ExceptionMessages.ValidationMessageHeader, "Invalid instruction was found: xxx"));
        }

        [Test]
        public void ReadFileAsListOfInstructionsShouldThrowAnExceptionFileDoesNotContainApplyInstruction()
        {
            var testFilename = "testinstructionfiles/invalid_no_apply.txt";

            var ex = Assert.Throws<Exception>(() => _sut.ReadFileAsListOfInstructions(testFilename));

            ex.Message.Should().Be(ExceptionMessages.FileMustContainApplyInstructionExceptionMessage);
        }

        [Test]
        public void ReadFileAsListOfInstructionsShouldThrowAnExceptionWhenFileNotFound()
        {
            var testFilename = "testinstructionfiles/ddd.txt";

            var ex = Assert.Throws<FileNotFoundException>(() => _sut.ReadFileAsListOfInstructions(testFilename));

            ex.Message.Should().Contain("Could not find file");
        }
    }
}
