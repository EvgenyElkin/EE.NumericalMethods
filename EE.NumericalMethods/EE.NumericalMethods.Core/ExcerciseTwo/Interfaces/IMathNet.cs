namespace EE.NumericalMethods.Core.ExcerciseTwo.Interfaces
{
    public interface IMathNet3
    {
        double MaxX { get; }
        double MaxY { get; }
        double MaxT { get; }
        double H { get; }
        double D { get; }
        int SizeX { get; }
        int SizeY { get; }
        int Height { get; }
        void Set(int i, int j, int k, double value);
        double Get(int i, int j, int k);
    }
}