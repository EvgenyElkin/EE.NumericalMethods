namespace EE.NumericalMethods.Core.ExcerciseTwo.Interfaces
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
        void Compute(IMathNet3 net);
    }
}