namespace EE.NumericalMethods.Core.Helpers
{
    public interface IAlgoritmContext
    {
        double[] TridiagonalMatrixAlgoritm(double[,] b, double[] a);
    }
}