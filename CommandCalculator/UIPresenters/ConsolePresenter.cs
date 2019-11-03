using System;

namespace CommandCalculator.UIPresenters
{
    /// <summary>
    /// Delivers interaction results to the UI 
    /// </summary>
    public class ConsolePresenter : IUIPresenter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
