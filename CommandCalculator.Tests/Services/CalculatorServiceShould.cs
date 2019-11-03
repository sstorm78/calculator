using System.Collections.Generic;
using CommandCalculator.Models;
using CommandCalculator.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CommandCalculator.Tests.Services
{
    [TestFixture]
    public class CalculatorServiceShould
    {
        private readonly ICalculatorService _sut;

        public CalculatorServiceShould()
        {
            _sut = new CalculatorService();
        }

        [Test]
        public void CalculateShouldReturn15()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Add, 2),
                                       new Instruction(InstructionActions.Multiply, 3),
                                       new Instruction(InstructionActions.Apply, 3)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(15);
        }

        [Test]
        public void CalculateShouldReturn45()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Multiply, 9),
                                       new Instruction(InstructionActions.Apply, 5)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(45);
        }

        [Test]
        public void CalculateShouldAddAndReturn20()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Add, 10),
                                       new Instruction(InstructionActions.Apply, 10)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(20);
        }

        [Test]
        public void CalculateShouldSubstractAndReturn30()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Substract, 20),
                                       new Instruction(InstructionActions.Apply, 50)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(30);
        }

        [Test]
        public void CalculateShouldDivideAndReturn10()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Divide, 2),
                                       new Instruction(InstructionActions.Apply, 20)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(10);
        }

        [Test]
        public void CalculateShouldMultiplyAndReturn40()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Multiply, 2),
                                       new Instruction(InstructionActions.Apply, 20)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(40);
        }

        [Test]
        public void CalculateShouldReturn5()
        {
            var testInstructions = new List<Instruction>
                                   {
                                       new Instruction(InstructionActions.Apply, 5)
                                   };

            var result = _sut.Calculate(testInstructions);

            result.Should().Be(5);
        }
    }
}
