using System;
using System.Collections.Generic;
using System.Linq;
using CommandCalculator.Models;
using CommandCalculator.Validators;

namespace CommandCalculator.Converters
{
    public class InstructionConverter : IInstructionConverter
    {
        private readonly IInstructionValidator _instructionValidator;

        public InstructionConverter(
            IInstructionValidator instructionValidator)
        {
            _instructionValidator = instructionValidator;
        }

        public List<Instruction> ConvertIntoListOfInstructions(string[] fileLines)
        {
            var result = new List<Instruction>();
            var validationMessages = new List<string>();

            if (fileLines == null || fileLines.Length == 0)
            {
                throw new Exception(ExceptionMessages.FileIsInvalidOrEmptyExceptionMessage);
            }

            foreach (var line in fileLines)
            {
                var instructionDetails = line.Split(" ");

                var validationResult = _instructionValidator.IsValid(instructionDetails);

                if (!validationResult.IsValid)
                {
                    validationMessages.Add(validationResult.Message);
                    continue;
                }

                var action = Actions.AvailableActions[instructionDetails.First().ToLowerInvariant()];
                var value = double.Parse(instructionDetails[1]);

                result.Add(new Instruction(action, value));
            }

            if (validationMessages.Any())
            {
                throw new Exception(string.Format(ExceptionMessages.ValidationMessageHeader, string.Join(Environment.NewLine, validationMessages)));
            }

            if (result.Any() && result.Last().Action != InstructionActions.Apply)
            {
                throw new Exception(ExceptionMessages.FileMustContainApplyInstructionExceptionMessage);
            }

            return result;
        }
    }
}
