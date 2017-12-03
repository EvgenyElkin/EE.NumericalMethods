using System;
using EE.NumericalMethods.Core.ExcerciseTwo.Interfaces;
using EE.NumericalMethods.Core.Helpers;
using IMethod = EE.NumericalMethods.Core.ExcerciseTwo.Interfaces.IMethod;

namespace EE.NumericalMethods.Core.ExcerciseTwo.Methods
{
    public class PeacemanRachfordMethod : IMethod
    {
        private readonly Func<double, double, double, double> _function;

        public PeacemanRachfordMethod(Func<double, double, double, double> function)
        {
            _function = function;
        }

        public string Name => "Метод Писмена-Рекфорда(переменных направлений)";

        public void Compute(IMathNet3 net)
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

        private double[] ComputeColumn(IMathNet3 net, int i, int k)
        {
            var dh2 = net.D / (net.H * net.H);
            var a = -dh2 / 2;
            var b = 1 + dh2;
            var c = a;

            double Fj(int j)
            {
                var aj = dh2 / 2;
                var bj = 1 - dh2;
                var cj = aj;
                return aj * net.Get(i, j - 1, k - 1) + bj * net.Get(i, j, k - 1) + cj * net.Get(i, j + 1, k - 1) +
                       net.D / 2 * _function(i * net.H, (j - 1) * net.H, (k - 0.5) * net.D);
            }

            var n = net.SizeY - 1;
            var matrix = new double[n, n];
            var values = new double[n];

            for (var j = 0; j < n; j++)
            {
                //Заполняем матрицу
                if (j > 0)
                {
                    matrix[j - 1, j] = a;
                }
                matrix[j, j] = b;
                if (j < n - 1)
                {
                    matrix[j + 1, j] = c;
                }

                //Заполняем правую часть
                values[j] = Fj(j + 1);
            }

            //Коректируем с помощью граничных условий
            values[0] -= net.Get(i, 0, k) * a;
            values[n - 1] -= net.Get(i, net.SizeY, k) * c;
            //Запускаем прогонку на N-1xN-1 матрице
            var tdma = AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
            //Формируем результатирующую строку матрицы
            var result = new double[net.SizeY + 1];
            for (var j = 0; j < tdma.Length; j++) result[j+1] = tdma[j];
            return result;
        }

        private double[] ComputeRow(IMathNet3 net, int j, int k)
        {
            var dh2 = net.D / (net.H * net.H);
            var a = dh2 / 2;
            var b = 1 - dh2;
            var c = a;

            double Fij(int i)
            {
                var ai = dh2 / 2;
                var bi = 1 - dh2;
                var ci = ai;
                return ai * net.Get(i - 1, j, k) + bi * net.Get(i, j, k) + ci * net.Get(i + 1, j, k) +
                       net.D / 2 * _function((i - 1) * net.H, j * net.H, (k - 0.5) * net.D);
            }
            
            var n = net.SizeX - 1;
            var matrix = new double[n, n];
            var values = new double[n];

            for (var i = 0; i < n; i++)
            {
                //Заполняем матрицу
                if (i > 0)
                {
                    matrix[i - 1, i] = a;
                }
                matrix[i, i] = b;
                if (i < n - 1)
                {
                    matrix[i + 1, i] = c;
                }

                //Заполняем правую часть
                values[i] = Fij(i + 1);
            }

            values[0] -= net.Get(0, j, k) * a;
            values[n - 1] -= net.Get(net.SizeX, j, k) * c;

            var tdma = AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
            //Формируем результатирующую строку матрицы
            var result = new double[net.SizeX + 1];
            for (var i = 0; i < tdma.Length; i++) result[i + 1] = tdma[i];
            return result;
        }
    }
}
