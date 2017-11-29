using CommandLine;

namespace EE.NumericalMethods.ExcerciseOne
{
    [Verb("ex-1", HelpText = "Задание 1: Решить уравнение теплопроводности")]
    public class ExcerciseOneOptions
    {
        [Option('m', "method", HelpText = "Укажите метод для решения уравнения теплопроводности", Required = true)]
        public MethodTypes MethodType { get; set; }
    }
}
