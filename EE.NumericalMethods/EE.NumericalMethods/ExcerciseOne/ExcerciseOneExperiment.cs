using System;
using EE.NumericalMethods.Common;
using EE.NumericalMethods.ExcerciseOne.Methods;
using EE.NumericalMethods.ExcerciseOne.RunnerBuilders;

namespace EE.NumericalMethods.ExcerciseOne
{
    public class ExcerciseOneExperiment : IExperiment
    {
        private readonly ExcerciseOneOptions _options;

        public ExcerciseOneExperiment(ExcerciseOneOptions options)
        {
            _options = options;
        }
        
        public void Run()
        {
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

            var method = GetMethod(_options.MethodType);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Метод \"{0}\":", method.Name);
            Console.ResetColor();
            //Определяем ошибку для каждого эксперимента
            foreach (var net in nets)
            {
                method.Compute(net);
                var error = GetError(net, (x, t) => Math.Sin(x) + Math.Log(t * t + 1));

                Console.WriteLine("({0};{1})={2:g4}", net.Width, net.Height, error);
            }
        }

        private IMethod GetMethod(MethodTypes methodType)
        {
            switch (methodType)
            {
                case MethodTypes.Explicit:
                    return new ExplicitMethod();
                case MethodTypes.Implicit:
                    return new ImplicitMethod();
                case MethodTypes.CrankNicolson:
                    return new CrankNicolsonMethod();
                default:
                    throw new NotSupportedException($"Метод {methodType} не поддерживается");
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
