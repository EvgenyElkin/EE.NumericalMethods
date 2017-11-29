namespace EE.NumericalMethods.ExcerciseOne.Methods
{
    public interface IMethod
    {
        string Name { get; }
        void Compute(MathNet net);
    }
}