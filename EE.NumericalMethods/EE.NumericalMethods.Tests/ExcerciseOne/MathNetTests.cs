using EE.NumericalMethods.Core.Common.Builders.MathNet2;
using Xunit;

namespace EE.NumericalMethods.Tests.ExcerciseOne
{
    public class MathNetTests
    {
        #region Конструктор с вычислением размеров

        [Theory]
        [MemberData(nameof(Ctor_Success_TestCasesData))]
        public void Ctor_Success_TestCases(MathNetTestCase @case)
        {
            //Arrange and Act
            var net = new MathNet2(@case.MaxX, @case.MaxT, @case.H, @case.D);

            Assert.Equal(@case.Widht, net.Width);
            Assert.Equal(@case.Height, net.Height);
        }

        public static TheoryData<MathNetTestCase> Ctor_Success_TestCasesData()
        {
            return new TheoryData<MathNetTestCase>
            {
                //2х2
                new MathNetTestCase {MaxX = 3, MaxT = 3, H = 3, D = 3, Height = 1, Widht = 1},
                //7х7
                new MathNetTestCase {MaxX = 3, MaxT = 3, H = 0.5, D = 0.5, Height = 6, Widht = 6},
                //3x3 округление вверх
                new MathNetTestCase {MaxX = 3, MaxT = 3, H = 1.1, D = 1.1, Height = 2, Widht = 2}
            };
        }

        public class MathNetTestCase
        {
            public double MaxX { get; set; }
            public double MaxT { get; set; }
            public double H { get; set; }
            public double D { get; set; }
            public int Widht { get; set; }
            public int Height { get; set; }
        }

        #endregion
    }
}
