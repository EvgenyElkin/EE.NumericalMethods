using System;
using System.Collections.Generic;
using EE.NumericalMethods.Core.ExcerciseOne.Interfaces;

namespace EE.NumericalMethods.Core.Common.Builders.MathNet2
{
    /// <summary>
    /// Класс, позволяет сконструировать объект запускающий эксперименты
    /// Является ДКА с состояниями конструирования объекта
    /// </summary>
    public class MathNet2Builder : IEmptyBuilder,
        IBuilderWithArea,
        IBuilderWithInitialCondition,
        ICompleteBuilder
    {
        private MathNet2Builder()
        { }

        private double _maxX;
        private double _maxT;
        private Func<double, double> _initialCondition;
        private Func<double, double> _leftBorder;
        private Func<double, double> _rightBorder;
        private List<Tuple<double, double>> _steps;

        public static IEmptyBuilder Create()
        {
            return new MathNet2Builder
            {
                _steps = new List<Tuple<double, double>>()
            };
        }

        public IBuilderWithArea SetArea(double maxX, double maxT)
        {
            _maxX = maxX;
            _maxT = maxT;
            return this;
        }

        public IBuilderWithInitialCondition SetInitialCondition(Func<double, double> initialFunction)
        {
            _initialCondition = initialFunction;
            return this;
        }

        public IBuilderWithBorder SetBorder(Func<double, double> leftBorderFunction, Func<double, double> rightBorderFunction)
        {
            _leftBorder = leftBorderFunction;
            _rightBorder = rightBorderFunction;
            return this;
        }

        public ICompleteBuilder WithNet(double h, double d)
        {
            _steps.Add(Tuple.Create(h, d));
            return this;
        }

        public IEnumerable<MathNet2> Build()
        {
            //Для каждого эксперимента создаем сетку
            foreach (var step in _steps)
            {
                //Инициализируем сетку
                var net = new MathNet2(_maxX, _maxT, step.Item1, step.Item2);
                //Заполняем узлы по начальным условиям, при t = 0
                for (var i = 0; i <= net.Width; i++)
                {
                    var initialValue = _initialCondition(i * net.H);
                    net.Set(i, 0, initialValue);
                }
                //Заполняем узлы на границах, при x = 0, и x = T
                for (var j = 0; j <= net.Height; j++)
                {
                    var leftBorderValue = _leftBorder(j * net.D);
                    var rightBorderValue = _rightBorder(j * net.D);
                    net.Set(0, j, leftBorderValue);
                    net.Set(net.Width, j, rightBorderValue);
                }
                //Возвращаем заполненую сетку
                yield return net;
            }
        }
    }
}