using System;
using EE.NumericalMethods.Core.Common;

namespace EE.NumericalMethods.Core.ExcerciseTwo
{
    public class ExcerciseTwoExperiment : IExperiment
    {
        private readonly IExcerciseTwoOptions _options;

        public ExcerciseTwoExperiment(IExcerciseTwoOptions options)
        {
            _options = options;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}