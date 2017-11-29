using System;

namespace EE.NumericalMethods.ExcerciseOne.Methods
{
    public class ExplicitMethod : IMethod
    {
        private static Func<double,double,double> F => (x,t) => Math.Sin(x) + (2 * t) / (t * t + 1);

        public string Name => "Явный";

        public void Compute(MathNet net)
        {
            for (var j = 1; j <= net.Height; j++)
            {
                for (var i = 1; i < net.Width; i++)
                {
                    var x = net.H * i;
                    var t = net.D * (j - 1);
                    var value = net.Get(i, j - 1) + net.D *
                                ((net.Get(i + 1, j - 1) - 2 * net.Get(i, j - 1) + net.Get(i - 1, j - 1)) 
                                 / (net.H * net.H) + F(x, t));

                    net.Set(i,j, value);
                }
            }
        }
    }
}