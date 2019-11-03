using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommandCalculator.Models;
using CommandCalculator.Validators;

namespace CommandCalculator.Services
{
    public class InstructionFileReader : IInstructionFileReader
    {
        private readonly IInstructionValidator _instructionValidator;

        public InstructionFileReader(IInstructionValidator instructionValidator)
        {
            _instructionValidator = instructionValidator;
        }

        public List<Instruction> ReadFileAsListOfInstructions(string filepath)
        {
            var result = new List<Instruction>();
            var validationMessages = new List<string>();
            
            var fileLines = File.ReadAllLines(filepath, Encoding.UTF8);

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

            return result;
        }
    }
}
