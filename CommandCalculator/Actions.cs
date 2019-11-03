using System.Collections.Generic;
using CommandCalculator.Models;

namespace CommandCalculator
{
    public static class Actions
    {
        public static Dictionary<string, InstructionActions> AvailableActions = new Dictionary<string, InstructionActions>
                                                                                {
                                                                                    {"add", InstructionActions.Add},
                                                                                    {"substract", InstructionActions.Substract},
                                                                                    {"multiply", InstructionActions.Multiply},
                                                                                    {"divide", InstructionActions.Divide},
                                                                                    {"apply", InstructionActions.Apply}
                                                                                };
    }
}
