using System.Collections.Generic;
using CommandCalculator.Models;

namespace CommandCalculator.Converters
{
    public interface IInstructionConverter
    {
        List<Instruction> ConvertIntoListOfInstructions(string[] fileLines);
    }
}