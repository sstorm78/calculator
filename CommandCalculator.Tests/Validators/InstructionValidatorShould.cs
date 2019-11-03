using CommandCalculator.Validators;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Validators
{
    [TestFixture]
    public class InstructionValidatorShould
    {
        private readonly IInstructionValidator _sut;
        
        public InstructionValidatorShould()
        {
            _sut = new InstructionValidator();
        }

        [TestCase("add",10)]
        [TestCase("Add", 10)]

        [TestCase("divide", 10)]
        [TestCase("Divide", 10)]

        [TestCase("multiply", 10)]
        [TestCase("Multiply", 10)]

        [TestCase("substract", 10)]
        [TestCase("Substract", 10)]

        [TestCase("apply", 10)]
        [TestCase("Apply", 10)]
        public void IsValidShouldSuccessfullyValidateAValidInstruction(string action, double value)
        {
            var testInstruction = action + " " + value;
            
            var result = _sut.IsValid(testInstruction.Split(" "));

            result.IsValid.Should().BeTrue();
        }

        [TestCase(" ", "10", ExceptionMessages.InvalidInstructionActionExceptionMessage)]
        [TestCase("", "10", ExceptionMessages.InvalidInstructionActionExceptionMessage)]
        [TestCase(" ", "", ExceptionMessages.InvalidInstructionActionExceptionMessage)]
        [TestCase("add", "aa", ExceptionMessages.InvalidInstructionValueExceptionMessage)]
        [TestCase("add", "", ExceptionMessages.InvalidInstructionValueExceptionMessage)]
        [TestCase("add ", "10", ExceptionMessages.InvalidInstructionValueExceptionMessage)]
        [TestCase(" add", "10", ExceptionMessages.InvalidInstructionActionExceptionMessage)]
        [TestCase("divide", "0", ExceptionMessages.InvalidInstructionValueExceptionMessage)]

        public void IsValidShouldRetunrInvalidResult(string action, string value,string expectedValidationMessage)
        {
            var testInstruction = action + " " + value;

            var result = _sut.IsValid(testInstruction.Split(" "));

            result.IsValid.Should().BeFalse();
            result.Message.Should().Be(string.Format(expectedValidationMessage, testInstruction));
        }

        [Test]
        public void IsValidShouldReturnInvalidResultWhenNullIsPassed()
        {
            var result = _sut.IsValid(null);

            result.IsValid.Should().BeFalse();
            result.Message.Should().Be(string.Format(ExceptionMessages.InvalidInstructionExceptionMessage,"null"));
        }

        [Test]
        public void IsValidShouldReturnInvalidResultWhenEmptyInstructionPassed()
        {
            var result = _sut.IsValid(new []{""});

            result.IsValid.Should().BeFalse();
            result.Message.Should().Be(string.Format(ExceptionMessages.InvalidInstructionExceptionMessage,""));
        }
    }
}
