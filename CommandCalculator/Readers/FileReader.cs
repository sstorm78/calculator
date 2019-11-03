using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommandCalculator.Readers
{
    public class FileReader : IReader
    {
        public string[] ReadAsStringLines(string filepath)
        {
            return File.ReadAllLines(filepath, Encoding.UTF8);
        }
    }
}
