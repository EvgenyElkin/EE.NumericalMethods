using CommandLine;
using EE.NumericalMethods.Core.ExcerciseOne;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;

namespace EE.NumericalMethods.Options
{
    /// <summary>
    /// Опции для запуска первого упражнения
    /// </summary>
    [Verb("ex-1", HelpText = "Задание 1: Решить уравнение теплопроводности")]
    public class ExcerciseOneOptions : IExcerciseOneOptions
    {
        /// <summary>
        /// Выбор метода для запуска
        /// </summary>
        [Option('m', "method", HelpText = "Укажите метод для решения уравнения теплопроводности", Required = true)]
        public MethodTypes MethodType { get; set; }
    }
}
