using System;
using System.Linq;
using EE.NumericalMethods.Core.Common.Builders.MathNet2;
using EE.NumericalMethods.Core.Helpers;

namespace EE.NumericalMethods.Core.ExcerciseThree
{
    public class NonLinearCrankNicolsonMethod
    {
        private readonly Func<double, double, double> _heterogeniousFunction;
        private readonly Func<double, double> _differenceFunction;
        private readonly Func<double, double> _differenceFunctionDerivative;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="heterogeniousFunction">Неоднородность</param>
        /// <param name="differenceFunction">фи</param>
        /// <param name="differenceFunctionDerivative">производная фи</param>
        public NonLinearCrankNicolsonMethod(Func<double, double,  double> heterogeniousFunction,
            Func<double, double> differenceFunction,
            Func<double, double> differenceFunctionDerivative)
        {
            _heterogeniousFunction = heterogeniousFunction;
            _differenceFunction = differenceFunction;
            _differenceFunctionDerivative = differenceFunctionDerivative;
        }

        public string Name => "Нелинейный аналог схемы Кранка-Никольсона";

        private const double NewtonEpsilon = 5e-2;

        public void Compute(MathNet2 net)
        {
            //Вычисляем по слоям по времени
            for (var j = 0; j < net.Height; j++)
            {
                var approximation = net.GetRow(j);
                var newtonIsComplete = false;
                while (!newtonIsComplete)
                {
                    //Получаем новое преближение с помощью метода ньютона
                    var nextAproximation = GetNextApproximation(net, approximation, j);
                    //Вычисление ошибки 
                    var error = nextAproximation
                        .Zip(approximation, (e1, e2) => Math.Abs(e1 - e2))
                        .Max();
                    newtonIsComplete = error < NewtonEpsilon;
                    approximation = nextAproximation;
                }
                //Подставляем преближение в сетку с результатом
                for (var i = 1; i < net.Width; i++)
                {
                    net.Set(i, j + 1, approximation[i]);
                }
            }
        }
        
        public double[] GetNextApproximation(MathNet2 net, double[] approximation, int j)
        {
            //Коэфиценты для построения матрицы
            double a, b;
            a = b = - net.D / (2 * net.H * net.H);
            var t = net.D * j;

            //Подготавливаем трехдиагональную матрицу c правыми частями
            var n = net.Width - 1;
            var matrix = new double[n, n];
            var values = new double[n];


            for (var i = 0; i < n; i++)
            {
                //Заполняем матрицу
                if (i > 0)
                {
                    matrix[i - 1, i] = a;
                }
                matrix[i, i] = _differenceFunctionDerivative(approximation[i]) + net.D / (net.H * net.H);
                if (i < n - 1)
                {
                    matrix[i + 1, i] = b;
                }

                //Заполняем правую часть
                var x = i * net.H;
                values[i] = -(_differenceFunction(approximation[i])
                              - _differenceFunction(net.Get(i, j))
                              - _differenceFunctionDerivative(approximation[i]) * approximation[i]
                              - net.D * _heterogeniousFunction(x, t))
                            + net.D / (2 * net.H * net.H) * (approximation[i + 2] - 2 * approximation[i + 1] + approximation[i]);
            }

            //Коректируем с помощью граничных условий
            values[0] -= net.Get(0, j);
            values[n - 1] -= net.Get(net.Width, j);
            var tdma = AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
            //Заполняем граничные условия
            var result = new double[net.Width + 1];
            result[0] = net.Get(0, j);
            for (var i = 1; i < tdma.Length; i++) result[i] = tdma[i];
            result[net.Width] = net.Get(net.Width, j);
            return result;
        }
    }
}
