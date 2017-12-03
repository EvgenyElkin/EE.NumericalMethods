namespace EE.NumericalMethods.Core.ExcerciseOne.Interfaces
{
    public interface IMathNet
    {
        double MaxX { get; }
        double MaxT { get; }
        double H { get; }
        double D { get; }
        int Width { get; }
        int Height { get; }
        void Set(int i, int j, double value);
        double Get(int i, int j);
    }
}