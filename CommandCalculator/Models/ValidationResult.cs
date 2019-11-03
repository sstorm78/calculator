namespace CommandCalculator.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        public ValidationResult Valid()
        {
            IsValid = true;
            return this;
        }

        public ValidationResult Invalid(string message)
        {
            IsValid = false;
            Message = message;
            return this;
        }


    }
}
