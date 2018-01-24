using EE.NumericalMethods.Core.Common.Builders.MathNet2;
using EE.NumericalMethods.Core.ExcerciseOne.Methods;
using Xunit;

namespace EE.NumericalMethods.Tests.ExcerciseOne.Methods
{
    public class ExplicitMethodTests
    {
        [Fact(Skip = "Починить")]
        public void Compute_Success()
        {
            //Arrange
            //Устанавливаем сетку 3x3 с единичными начальными и граничными условиями
            //1 0 1
            //1 0 1
            //1 1 1
            var net = new MathNet2(2,2,1,1);
            //Начальные и граничные условия условия
            for (var i = 0; i <= net.Width; i++)
            {
                net.Set(i, 0, 1);
                net.Set(0, i, 1);
                net.Set(net.Width, i, 1);
            }
            
            double Func(double x, double t) => x + t * 10;
            var method = new ExplicitMethod(Func);

            //Act
            method.Compute(net);

            //Assert
            Assert.Equal(2, net.Get(1,1));
            Assert.Equal(11, net.Get(1,2));
        }
    }
}
