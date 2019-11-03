using System.Collections.Generic;
using CommandCalculator.Models;

namespace CommandCalculator.Services
{
    public interface IInstructionFileReader
    {
        List<Instruction> ReadFileAsListOfInstructions(string filepath);
    }
}