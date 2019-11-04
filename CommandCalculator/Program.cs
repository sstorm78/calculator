using System;
using System.Globalization;
using CommandCalculator.Calculators;
using CommandCalculator.Converters;
using CommandCalculator.Readers;
using CommandCalculator.UIPresenters;
using CommandCalculator.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace CommandCalculator
{
    public class Program
    {
        public static IServiceProvider ServiceProvider;
        public static IUIPresenter ConsoleWriter;

        static void Main(string[] args)
        {
            ServiceProvider = RegisterServices();
            
            ConsoleWriter = ServiceProvider.GetService<IUIPresenter>();

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

                ConsoleWriter.WriteLine("");
                ConsoleWriter.WriteLine("Enter a file name or exit");
            }

            DisposeServices();
        }

        public static void ReadInstructions(string filename)
        {
            var converter = ServiceProvider.GetService<IInstructionConverter>();
            var calculator = ServiceProvider.GetService<ICalculator>();
            var reader = ServiceProvider.GetService<IReader>();

            try
            {
                var rawFileContent = reader.ReadAsStringLines(filename);

                var listOfinstructions = converter.ConvertIntoListOfInstructions(rawFileContent);

                var result = calculator.Calculate(listOfinstructions);

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
                .AddSingleton<IUIPresenter, ConsolePresenter>()
                .AddSingleton<IInstructionConverter, InstructionConverter>()
                .AddSingleton<IInstructionValidator, InstructionValidator>()
                .AddSingleton<IReader, FileReader>()
                .AddSingleton<ICalculator, SimpleCalculator>()
                .BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (ServiceProvider == null)
            {
                return;
            }

            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
