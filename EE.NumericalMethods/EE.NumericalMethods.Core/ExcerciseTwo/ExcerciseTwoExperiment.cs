using System;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.Common.Builders.MathNet3;

namespace EE.NumericalMethods.Core.ExcerciseTwo
{
    public class ExcerciseTwoExperiment : IExperiment
    {
        public void Run()
        {
            //Получаем сетки с задаными параметрами
            var nets = MathNet3Builder.Create()
                .SetArea(2 * Math.PI, 2 * Math.PI, 10)
                .SetInitialCondition((x, y) => Math.Sin(x + y))
                .SetBorder((x, t) => Math.Sin(x) + Math.Log(t * t + 1), (y, t) => Math.Sin(y) + Math.Log(t * t + 1))
                .WithNet(Math.PI / 5, 0.25)
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
                var error = net.GetError((x, y, t) => Math.Sin(x + y) + Math.Log(t * t + 1));

                Console.WriteLine("({0};{1})={2:g4}", net.SizeX, net.Height, error);
            };
        }
    }
}