using System;
using EE.NumericalMethods.Core.ExcerciseOne.Builders;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;
using EE.NumericalMethods.Core.Helpers;

namespace EE.NumericalMethods.Core.ExcerciseOne.Methods
{
    public class ImplicitMethod : MethodBase
    {
        public ImplicitMethod(Func<double, double, double> function) : base(function)
        {
        }

        public override string Name => "Неявный";

        public override void Compute(IMathNet net)
        {
            for (var j = 1; j <= net.Height; j++)
            {
                var tdmaResult = ComputeTriagonalMatrix(net, j);
                for (var i = 1; i < net.Width; i++)
                {
                    net.Set(i, j, tdmaResult[i]);
                }
            }
        }

        public double[] ComputeTriagonalMatrix(IMathNet net, int j)
        {
            //Коэфиценты для построения матрицы
            double a, b, c;
            a = b = net.D;
            c = -(net.H * net.H + 2 * net.D);
            var t = net.D * (j - 1);

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
                matrix[i, i] = c;
                if (i < n - 1)
                {
                    matrix[i + 1, i] = b;
                }

                //Заполняем правую часть
                var x = (i + 1) * net.H;
                values[i] = -((Function(x, t) * net.D + net.Get(i + 1, j - 1)) * net.H * net.H);
            }

            //Коректируем с помощью граничных условий
            values[0] -= net.Get(0, j) * a;
            values[n - 1] -= net.Get(net.Width, j) * a;
            var tdma = AlgoritmContext.Current.TridiagonalMatrixAlgoritm(matrix, values);
            //Заполняем граничные условия
            var result = new double[net.Width + 1];
            result[0] = net.Get(0, j);
            for (var i = 0; i < tdma.Length; i++) result[i + 1] = tdma[i];
            result[net.Width] = net.Get(net.Width, j);
            return result;
        }
    }
}