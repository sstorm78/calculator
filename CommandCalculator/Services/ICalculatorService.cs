using System.Collections.Generic;
using CommandCalculator.Models;

namespace CommandCalculator.Services
{
    public interface ICalculatorService
    {
        double Calculate(IList<Instruction> instructions);
    }
}