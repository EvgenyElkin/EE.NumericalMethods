using System;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.Common.Builders.MathNet2;
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
        /// Мотод
        /// </summary>
        private readonly MethodTypes _method;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="method">Метод вычисления</param>
        public ExcerciseOneExperiment(MethodTypes method)
        {
            _method = method;
        }
        
        /// <summary>
        /// Запуск эксперимента
        /// </summary>
        public void Run()
        {
            //Получаем сетки с задаными параметрами
            var nets = MathNet2Builder.Create()
                .SetArea(Math.PI, 10)
                .SetInitialCondition(Math.Sin)
                .SetBorder(t => Math.Log(t * t + 1), t => Math.Log(t * t + 1))
                .WithNet(Math.PI / 5 , Math.Pow(2,-2))
                .WithNet(Math.PI / 10, Math.Pow(2,-2))
                .WithNet(Math.PI / 5 , Math.Pow(2,-3))
                .WithNet(Math.PI / 5 , Math.Pow(2,-4))
                .WithNet(Math.PI / 10, Math.Pow(2,-3))
                .WithNet(Math.PI / 20, Math.Pow(2,-4))
                .WithNet(Math.PI / 40, Math.Pow(2,-5))
                .WithNet(Math.PI / 80, Math.Pow(2,-6))
                .Build();

            //Получаем класс с логикой метода для вычисления
            var method = GetMethod(_method, (x, t) => Math.Sin(x) + (2 * t) / (t * t + 1));

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
                var error = net.GetError((x, t) => Math.Sin(x) + Math.Log(t * t + 1));

                Console.WriteLine("({0};{1})={2:g4}", net.Width, net.Height, error);
            }
        }

        /// <summary>
        /// Фабричный метод для получения метода
        /// </summary>
        /// <param name="methodType">Метод</param>
        /// <param name="func">Правая часть</param>
        /// <returns></returns>
        private MethodBase GetMethod(MethodTypes methodType, Func<double,double, double> func)
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
    }
}
