using System;
using EE.NumericalMethods.Core.Common.Builders.MathNet2;

namespace EE.NumericalMethods.Core.ExcerciseOne.Methods
{
    public abstract class MethodBase
    {
        public abstract string Name { get; }

        protected readonly Func<double, double, double> Function;

        protected MethodBase(Func<double, double, double> function)
        {
            Function = function;
        }

        public abstract void Compute(MathNet2 net);
    }
}