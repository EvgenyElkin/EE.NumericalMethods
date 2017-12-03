using System;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.ExcerciseOne.Builders;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;
using EE.NumericalMethods.Core.ExcerciseOne.Methods;

namespace EE.NumericalMethods.Core.ExcerciseOne
{
    /// <summary>
    /// Первое упражнение
    /// </summary>
    public class ExcerciseOneExperiment : IExperiment
    {
        /// <summary>
        /// Параметры
        /// </summary>
        private readonly IExcerciseOneOptions _options;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Параметры запуска</param>
        public ExcerciseOneExperiment(IExcerciseOneOptions options)
        {
            _options = options;
        }
        
        /// <summary>
        /// Запуск эксперимента
        /// </summary>
        public void Run()
        {
            //Получаем сетки с задаными параметрами
            var nets = NetBuilder.Create()
                .SetArea(Math.PI, 10)
                .SetInitialCondition(Math.Sin)
                .SetBorder(t => Math.Log(t * t + 1), t => Math.Log(t * t + 1))
                .WithNet(Math.PI / 5, 0.25)
                .WithNet(Math.PI / 10, 0.25)
                .WithNet(Math.PI / 5, 0.125)
                .WithNet(Math.PI / 5, 0.0625)
                .WithNet(Math.PI / 10, 0.125)
                .WithNet(Math.PI / 20, 0.0625)
                .Build();

            //Получаем класс с логикой метода для вычисления
            var method = GetMethod(_options.MethodType, (x, t) => Math.Sin(x) + (2 * t) / (t * t + 1));

            //Вывод данных
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Метод \"{0}\":", method.Name);
            Console.ResetColor();
            //Определяем ошибку для каждого эксперимента
            foreach (var net in nets)
            {
                //Вычисляем функцию на сетке, с помощью метода
                method.Compute(net);
                //Вычисляем ошибку, как максимум разности функций на точках сетки
                var error = GetError(net, (x, t) => Math.Sin(x) + Math.Log(t * t + 1));

                Console.WriteLine("({0};{1})={2:g4}", net.Width, net.Height, error);
            }
        }

        /// <summary>
        /// Фабричный метод для получения метода
        /// </summary>
        /// <param name="methodType">Метод</param>
        /// <param name="func">Правая часть</param>
        /// <returns></returns>
        private IMethod GetMethod(MethodTypes methodType, Func<double,double, double> func)
        {
            switch (methodType)
            {
                case MethodTypes.Explicit:
                    return new ExplicitMethod(func);
                case MethodTypes.Implicit:
                    return new ImplicitMethod(func);
                case MethodTypes.CrankNicolson:
                    return new CrankNicolsonMethod(func);
                default:
                    throw new NotSupportedException($"Метод {methodType} не поддерживается");
            }
        }

        /// <summary>
        /// Функция вычисления ошибки
        /// </summary>
        /// <param name="net">Сетка</param>
        /// <param name="expected">Реальное значение</param>
        /// <returns></returns>
        public static double GetError(MathNet net, Func<double, double, double> expected)
        {
            var result = 0d;
            for (var j = 0; j <= net.Height; j++)
            {
                for (var i = 0; i <= net.Width; i++)
                {
                    var x = net.H * i;
                    var t = net.D * j;
                    var error = Math.Abs(expected(x, t) - net.Get(i, j));
                    if (error > result)
                    {
                        result = error;
                    }
                }
            }
            return result;
        }
    }
}
