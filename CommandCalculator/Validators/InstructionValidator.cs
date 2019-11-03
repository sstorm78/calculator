using System.Linq;
using CommandCalculator.Models;

namespace CommandCalculator.Validators
{
    public class InstructionValidator : IInstructionValidator
    {
        public ValidationResult IsValid(string[] instructionLineDetails)
        {
            if (instructionLineDetails == null)
            {
                return new ValidationResult().Invalid(string.Format(ExceptionMessages.InvalidInstructionExceptionMessage, "null"));
            }

            if (instructionLineDetails.Length < 2)
            {
                return new ValidationResult().Invalid(string.Format(ExceptionMessages.InvalidInstructionExceptionMessage, string.Join(" ", instructionLineDetails)));
            }
            
            var action = instructionLineDetails.First().ToLowerInvariant().TrimStart().TrimEnd();

            if (Actions.AvailableActions.ContainsKey(action) == false)
            {
                return new ValidationResult().Invalid(string.Format(ExceptionMessages.InvalidInstructionActionExceptionMessage, string.Join(" ", instructionLineDetails)));
            }
            
            if (double.TryParse(instructionLineDetails[1].TrimStart().TrimEnd(), out double value) == false)
            {
                return new ValidationResult().Invalid(string.Format(ExceptionMessages.InvalidInstructionValueExceptionMessage, string.Join(" ", instructionLineDetails)));
            }

            if (action == "divide" && (int)value == 0)
            {
                return new ValidationResult().Invalid(string.Format(ExceptionMessages.InvalidInstructionValueExceptionMessage, string.Join(" ", instructionLineDetails)));
            }

            return new ValidationResult().Valid();
        }

    }
}
