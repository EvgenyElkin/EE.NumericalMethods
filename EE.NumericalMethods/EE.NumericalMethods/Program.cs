using System;
using CommandLine;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.ExcerciseOne;
using EE.NumericalMethods.Core.ExcerciseThree;
using EE.NumericalMethods.Core.ExcerciseTwo;
using EE.NumericalMethods.Options;

namespace EE.NumericalMethods
{
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        public static void Main(string[] args)
        {
            //try
            //{
                //Разбор входных параметров и определения выполняемого блока
                var experiment = Parser.Default.ParseArguments<ExcerciseOneOptions, ExcerciseTwoOptions, ExcerciseThreeOptions>(args)
                    .MapResult(
                        (ExcerciseOneOptions opt) => new ExcerciseOneExperiment(opt.MethodType),
                        (ExcerciseTwoOptions opt) => new ExcerciseTwoExperiment(),
                        (ExcerciseThreeOptions opt) => new ExcerciseThreeExperiment(),
                        errors => (IExperiment)null);

                //Запуск приложения
                experiment?.Run();
            //}
            //catch (Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Произошла ошибка:");
            //    Console.WriteLine(ex.Message);
            //    Console.ResetColor();
            //}
            
            Console.WriteLine("Нажимите клавишу чтобы продолжить...");
            Console.ReadKey();
        }
    }
}
