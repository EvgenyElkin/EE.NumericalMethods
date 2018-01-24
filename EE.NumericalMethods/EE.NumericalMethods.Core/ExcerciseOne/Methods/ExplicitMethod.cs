using System;
using EE.NumericalMethods.Core.Common.Builders.MathNet2;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;

namespace EE.NumericalMethods.Core.ExcerciseOne.Methods
{
    public class ExplicitMethod : MethodBase
    {
        public ExplicitMethod(Func<double, double, double> function) : base(function)
        {
        }
        
        public override string Name => "Явный";

        public override void Compute(MathNet2 net)
        {
            for (var j = 1; j <= net.Height; j++)
            {
                for (var i = 1; i < net.Width; i++)
                {
                    var x = net.H * i;
                    var t = net.D * (j - 1);
                    var value = net.Get(i, j - 1) + net.D *
                                ((net.Get(i + 1, j - 1) - 2 * net.Get(i, j - 1) + net.Get(i - 1, j - 1)) 
                                 / (net.H * net.H) + Function(x, t));

                    net.Set(i,j, value);
                }
            }
        }
    }
}