namespace CommandCalculator.Models
{
    public class Instruction
    {
        public InstructionActions Action { get; }
        public double Value { get; }

        public Instruction(InstructionActions action, double value)
        {
            Action = action;
            Value = value;
        }
    }
}
