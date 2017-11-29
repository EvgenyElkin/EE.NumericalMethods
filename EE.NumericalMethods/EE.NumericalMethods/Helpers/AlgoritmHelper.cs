using System.Diagnostics.CodeAnalysis;

namespace EE.NumericalMethods.Helpers
{
    public static class AlgoritmHelper
    {

        /// <summary>
        /// Трехдиагональная прогонка
        /// </summary>
        /// <param name="A">Матрица</param>
        /// <param name="B">Правая часть</param>
        /// <returns>Решение</returns>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static double[] TridiagonalMatrixAlgoritm(double[,] A, double[] B)
        {
            var N = B.Length - 1;
            var n = B.Length;
            var alpha = new double[N + 1];
            var beta = new double[N + 2];

            //Инициализация
            alpha[1] = -A[1, 0] / A[0, 0];
            beta[1] = B[0] / A[0, 0];

            //Прямой ход
            for (var i = 1; i <= N; i++)
            {
                var a = A[i - 1, i];
                var c = A[i, i];
                var f = B[i];

                if (i < N)
                {
                    var b = A[i + 1, i];
                    alpha[i+1] = -b / (c + alpha[i] * a);
                }
                beta[i+1] = (f - a * beta[i]) / (c + a * alpha[i]);
            }

            //Обратный ход
            var x = new double[n];
            x[N] = beta[N + 1];
            for (var i = N - 1; i >= 0; i--)
            {
                x[i] = alpha[i + 1] * x[i + 1] + beta[i + 1];
            }

            return x;
        }
    }
}