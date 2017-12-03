using System;
using EE.NumericalMethods.Core.ExcerciseOne.Builders;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;

namespace EE.NumericalMethods.Core.ExcerciseOne.Methods
{
    public abstract class MethodBase : IMethod
    {
        public abstract string Name { get; }

        protected readonly Func<double, double, double> Function;

        protected MethodBase(Func<double, double, double> function)
        {
            Function = function;
        }

        public abstract void Compute(IMathNet net);
    }
}