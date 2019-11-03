using CommandCalculator.Models;

namespace CommandCalculator.Validators
{
    public interface IInstructionValidator
    {
        ValidationResult IsValid(string[] instructionLineDetails);
    }
}