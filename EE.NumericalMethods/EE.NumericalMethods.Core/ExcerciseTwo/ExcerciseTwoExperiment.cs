using System;
using System.Collections.Generic;
using System.Linq;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.ExcerciseTwo.Builders;
using EE.NumericalMethods.Core.ExcerciseTwo.Interfaces;
using EE.NumericalMethods.Core.ExcerciseTwo.Methods;

namespace EE.NumericalMethods.Core.ExcerciseTwo
{
    public class ExcerciseTwoExperiment : IExperiment
    {
        private readonly IExcerciseTwoOptions _options;

        public ExcerciseTwoExperiment(IExcerciseTwoOptions options)
        {
            _options = options;
        }

        public void Run()
        {
            //Получаем сетки с задаными параметрами
            var nets = NetBuilder.Create()
                .SetArea(2 * Math.PI, 2 * Math.PI, 10)
                .SetInitialCondition((x, y) => Math.Sin(x + y))
                .SetBorder((x, t) => Math.Sin(x) + Math.Log(t * t + 1), (y, t) => Math.Sin(y) + Math.Log(t * t + 1))
                .WithNet(Math.PI / 10, 0.25)
                .WithNet(Math.PI / 5, 0.125)
                .WithNet(Math.PI / 5, 0.0625)
                .WithNet(Math.PI / 10, 0.125)
                .WithNet(Math.PI / 20, Math.Pow(2, -4))
                .WithNet(Math.PI / 40, Math.Pow(2, -5))
                .WithNet(Math.PI / 80, Math.Pow(2, -6))
                .WithNet(Math.PI / 160, Math.Pow(2, -7))
                .Build();

            //Получаем класс с логикой метода для вычисления
            var method = new PeacemanRachfordMethod((x,y,t) => 2 * Math.Sin(x + y) + 2*t/(t*t+1));

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
                var error = GetError(net, (x, y, t) => Math.Sin(x + y) + Math.Log(t * t + 1));

                Console.WriteLine("({0};{1})={2:g4}", net.SizeX, net.Height, error);
            };
        }

        /// <summary>
        /// Функция вычисления ошибки
        /// </summary>
        /// <param name="net">Сетка</param>
        /// <param name="expected">Реальное значение</param>
        /// <returns></returns>
        public static double GetError(IMathNet3 net, Func<double, double, double, double> expected)
        {
            var result = 0d;
            for (var k = 0; k <= net.Height; k++)
            for (var j = 0; j <= net.SizeY; j++)
            for (var i = 0; i <= net.SizeX; i++)
            {
                var x = net.H * i;
                var y = net.H * j;
                var t = net.D * k;
                var error = Math.Abs(expected(x, y, t) - net.Get(i, j, k));
                if (error > result)
                {
                    result = error;
                }
            }
            return result;
        }
    }
}