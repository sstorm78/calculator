using System.Collections.Generic;
using System.Linq;
using CommandCalculator.Models;

namespace CommandCalculator.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double Calculate(IList<Instruction> instructions)
        {
            var result = instructions.Last().Value;

            foreach (var instruction in instructions.SkipLast(1))
            {
                switch (instruction.Action)
                {
                    case InstructionActions.Add:
                        result = result + instruction.Value;
                        break;
                    case InstructionActions.Divide:
                        result = result / instruction.Value;
                        break;
                    case InstructionActions.Multiply:
                        result = result * instruction.Value;
                        break;
                    case InstructionActions.Substract:
                        result = result - instruction.Value;
                        break;
                }
            }

            return result;
        }
    }
}
