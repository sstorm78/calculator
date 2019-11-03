using CommandCalculator.Models;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Models
{
    public class Tests
    {
        [Test]
        public void ConstructorShouldPopulateInstanceValues()
        {
            var instruction = new Instruction(InstructionActions.Apply, 3);

            instruction.Action.Should().Be(InstructionActions.Apply);
            instruction.Value.Should().Be(3);
        }
    }
}