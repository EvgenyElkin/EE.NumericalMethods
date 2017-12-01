using EE.NumericalMethods.Core.ExcerciseOne.Builders;

namespace EE.NumericalMethods.Core.ExcerciseOne.Interfaces
{
    public interface IMethod
    {
        /// <summary>
        /// Название метода
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Функция вычисляющая значения на сетке
        /// </summary>
        /// <param name="net">Сетка</param>
        void Compute(MathNet net);
    }
}