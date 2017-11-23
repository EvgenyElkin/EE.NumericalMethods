using System;
using EE.NumericalMethods.Methods;
using EE.NumericalMethods.RunnerBuilders;

namespace EE.NumericalMethods
{
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        public static void Main()
        {
            //Experiment(new ExplicitMethod());
            Experiment(new ImplicitMethod());
            Console.ReadKey();
        }

        /// <summary>
        /// Метод которое выполняет эксперименты с заданым методом
        /// </summary>
        /// <param name="method"></param>
        public static void Experiment(IMethod method)
        {
            //Конструируем runner
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
            
            //Определяем ошибку для каждого эксперимента
            foreach (var net in nets)
            {
                method.Compute(net);
                var error = GetError(net, (x, t) => Math.Sin(x) + Math.Log(t * t + 1));
                Console.WriteLine(error.ToString("f5"));
            }
        }

        public static double GetError(MathNet net, Func<double, double, double> expected)
        {
            var result = 0d;
            for (var j = 0; j <= net.Height; j++)
            {
                for (var i = 0; i <= net.Width; i++)
                {
                    var x = net.H * i;
                    var t = net.D * j;
                    var error = Math.Abs(expected(x, t) - net.Get(i,j));
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
