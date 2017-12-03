using EE.NumericalMethods.Core.Helpers;
using EE.NumericalMethods.Tests.Comparers;
using Xunit;

namespace EE.NumericalMethods.Tests.Helpers
{
    public class AlgoritmContextTests
    {
        [Fact]
        public void TridiagonalMatrixAlgoritm_Success_Test()
        {
            //4x1 + 3x2 = 4;
            //x1 + 2x2 - x3 = 2;
            //x2 + 4x3 + x4 = 7,5;
            //x3 + 2x4 - x5 = 1;
            //x4 + 2x5 = -3;
            //Arrange
            var a = new double[,]
            {
                { 2, -3,  0,  0,  0},
                {-1,  8, -5,  0,  0},
                { 0, -1, 12, -6,  0},
                { 0,  0,  2, 18, -5},
                { 0,  0,  0, -4, 10}
            };

            var b = new double[] {-25, 72, -69, -156, 20};

            //Act
            var result = AlgoritmContext.Current.TridiagonalMatrixAlgoritm(a, b);

            var expected = new[] {-10, 4.999999, -2.00081, -10.001042, -3};
            //Assert
            Assert.Equal(expected, result, new EpsilonComparer(3));
        }
    }
}
