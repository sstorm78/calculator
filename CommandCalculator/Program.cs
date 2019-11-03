using System;
using System.Globalization;
using CommandCalculator.Services;
using CommandCalculator.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CommandCalculator
{
    public class Program
    {
        public static IServiceProvider ServiceProvider;
        public static IConsoleWriter ConsoleWriter;

        static void Main(string[] args)
        {
            ServiceProvider = RegisterServices();
            
            ConsoleWriter = ServiceProvider.GetService<IConsoleWriter>();

            DisplayIntro();

            var command = "";

            while (command != InputCommands.Exit)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }

                if (input.ToLowerInvariant() == InputCommands.Exit)
                {
                    command = InputCommands.Exit;
                    continue;
                }

                ReadInstructions(input);

                ConsoleWriter.WriteLine("Enter a file name or exit");
            }

            DisposeServices();
        }

        public static void ReadInstructions(string filename)
        {
            var instructionFileReader = ServiceProvider.GetService<IInstructionFileReader>();
            var calculatorService = ServiceProvider.GetService<ICalculatorService>();

            try
            {
                var instructions = instructionFileReader.ReadFileAsListOfInstructions(filename);

                var result = calculatorService.Calculate(instructions);

                ConsoleWriter.WriteLine(result.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                ConsoleWriter.WriteLine(ex.Message);
            }
        }

        private static void DisplayIntro()
        {
            ConsoleWriter.WriteLine("Welcome to Sergey Storm's calculator");
            ConsoleWriter.WriteLine("To load a file with instructions, enter the file name and press ENTER");
            ConsoleWriter.WriteLine("For the test, use ex1.txt or ex2.txt");
            ConsoleWriter.WriteLine("Type \"exit\" to exit the application");
            ConsoleWriter.WriteLine("");
            ConsoleWriter.WriteLine("Enter a file name or exit");
        }

        private static ServiceProvider RegisterServices()
        {
            return new ServiceCollection()
                .AddSingleton<IConsoleWriter, ConsoleWriter>()
                .AddSingleton<IInstructionFileReader, InstructionFileReader>()
                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .AddSingleton<ICalculatorService, CalculatorService>()
                .BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }
            if (ServiceProvider is IDisposable)
            {
                ((IDisposable)ServiceProvider).Dispose();
            }
        }
    }
}
