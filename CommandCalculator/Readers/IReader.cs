namespace CommandCalculator.Readers
{
    public interface IReader
    {
        string[] ReadAsStringLines(string filepath);
    }
}