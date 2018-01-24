using System;
using EE.NumericalMethods.Core.Common.Builders.MathNet3;
using EE.NumericalMethods.Core.Helpers;

namespace EE.NumericalMethods.Core.ExcerciseTwo
{
    public class PeacemanRachfordMethod
    {
        private readonly Func<double, double, double, double> _function;

        public PeacemanRachfordMethod(Func<double, double, double, double> function)
        {
            _function = function;
        }

        public string Name => "Метод Писмена-Рекфорда(переменных направлений)";

        public void Compute(MathNet3 net)
        {
            //Цикл по времени
            for (var k = 1; k <= net.Height; k++)
            {
                //Делаем N-1 прогонку по X
                for (var i = 1; i < net.SizeX; i++)
                {
                    var column = ComputeColumn(net, i, k);
                    for (var j = 1; j < net.SizeY; j++)
                    {
                        net.Set(i, j, k, column[j]);
                    }
                }
                ////Делаем N-1 прогонку по Y
                for (var j = 1; j < net.SizeY; j++)
                {
                    var row = ComputeRow(net, j, k);
                    for (var i = 1; i < net.SizeX; i++)
                    {
                        net.Set(i, j, k, row[i]);
                    }
                }
            }
        }

        private double[] ComputeColumn(MathNet3 net, int i, int k)
        {
            var dh2 = net.D / (net.H * net.H);
            var a = -dh2 / 2;
            var b = 1 + dh2;
            var c = a;
            var aj = dh2 / 2;
            var bj = 1 - dh2;
            var cj = aj;

            double Fj(int j)
            {
                return aj * net.Get(i, j - 1, k - 1) + bj * net.Get(i, j, k - 1) + cj * net.Get(i, j + 1, k - 1) +
                       net.D / 2 * _function(i * net.H, j * net.H, (k - 0.5) * net.D);
            }

            var n = net.SizeY + 1;
            var matrix = new double[n, n];
            var values = new double[n];

            matrix[0, 0] = 1;
            values[0] = net.Get(i, 0, k);
            for (var j = 1; j < n - 1; j++)
            {
                matrix[j - 1, j] = a;
                matrix[j, j] = b;
                matrix[j + 1, j] = c;
                values[j] = Fj(j);
            }

            matrix[n - 1, n - 1] = 1;
            values[n - 1] = net.Get(i, net.SizeY, k);

            return AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
        }

        private double[] ComputeRow(MathNet3 net, int j, int k)
        {
            var dh2 = net.D / (net.H * net.H);
            var a = -dh2 / 2;
            var b = 1 + dh2;
            var c = a;
            var ai = dh2 / 2;
            var bi = 1 - dh2;
            var ci = ai;

            double Fi(int i)
            {
                return ai * net.Get(i - 1, j, k) + bi * net.Get(i, j, k) + ci * net.Get(i + 1, j, k) +
                       net.D / 2 * _function(i * net.H, j * net.H, (k - 0.5) * net.D);
            }
            
            var n = net.SizeX + 1;
            var matrix = new double[n, n];
            var values = new double[n];

            matrix[0, 0] = 1;
            values[0] = net.Get(0, j, k);
            for (var i = 1; i < n-1; i++)
            {
                matrix[i - 1, i] = a;
                matrix[i, i] = b;
                matrix[i + 1, i] = c;
                values[i] = Fi(i);
            }

            matrix[n - 1, n - 1] = 1;
            values[n - 1] = net.Get(net.SizeX, j, k);

            return AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
        }
    }
}
