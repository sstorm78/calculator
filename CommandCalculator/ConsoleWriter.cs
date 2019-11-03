using System;

namespace CommandCalculator
{
    /// <summary>
    /// Delivers interaction results to the UI 
    /// </summary>
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
