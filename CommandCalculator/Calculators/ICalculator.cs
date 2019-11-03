using System.Collections.Generic;
using CommandCalculator.Models;

namespace CommandCalculator.Calculators
{
    public interface ICalculator
    {
        double Calculate(IList<Instruction> instructions);
    }
}