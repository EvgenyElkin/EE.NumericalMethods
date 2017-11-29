using System;
using CommandLine;
using EE.NumericalMethods.Common;
using EE.NumericalMethods.ExcerciseOne;
using EE.NumericalMethods.ExcerciseTwo;

namespace EE.NumericalMethods
{
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                var experiment = Parser.Default.ParseArguments<ExcerciseOneOptions, ExcerciseTwoOptions>(args)
                    .MapResult(
                        (ExcerciseOneOptions opt) => new ExcerciseOneExperiment(opt),
                        (ExcerciseTwoOptions opt) => new ExcerciseTwoExperiment(opt),
                        errors => (IExperiment)null);

                experiment?.Run();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Произошла ошибка:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            
            Console.WriteLine("Нажимите клавишу что бы продолжить...");
            Console.ReadKey();
        }
    }
}
