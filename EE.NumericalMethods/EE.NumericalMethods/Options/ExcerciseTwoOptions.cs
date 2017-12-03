using CommandLine;
using EE.NumericalMethods.Core.ExcerciseTwo;
using EE.NumericalMethods.Core.ExcerciseTwo.Interfaces;

namespace EE.NumericalMethods.Options
{
    /// <summary>
    /// Опции для запуска второго упражнения
    /// </summary>
    [Verb("ex-2", HelpText = "Задание 2: Решить уравнение второго порядка")]
    public class ExcerciseTwoOptions :  IExcerciseTwoOptions
    {

    }
}
