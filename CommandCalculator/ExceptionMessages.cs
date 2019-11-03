namespace CommandCalculator
{
    public static class ExceptionMessages
    {
        public const string InvalidInstructionExceptionMessage = "Invalid instruction was found: {0}";
        public const string InvalidInstructionActionExceptionMessage = "Invalid instruction action was found: {0}";
        public const string InvalidInstructionValueExceptionMessage = "Invalid instruction value was found: {0}";

        public const string FileIsInvalidOrEmptyExceptionMessage = "The file is in invalid format or empty";
        public const string FileMustContainApplyInstructionExceptionMessage = "The last file instruction should be Apply, please fix and try again";

        public const string ValidationMessageHeader = "Please check the file instructions, we have found the following errors:\r\n{0}";

    }
}
