using System;
using System.Collections.Generic;

namespace EE.NumericalMethods.Core.Common.Builders.MathNet3
{
    /// <summary>
    /// Класс, позволяет сконструировать объект запускающий эксперименты
    /// Является ДКА с состояниями конструирования объекта
    /// </summary>
    public class MathNet3Builder : IEmptyBuilder,
        IBuilderWithArea,
        IBuilderWithInitialCondition,
        ICompleteBuilder
    {
        private MathNet3Builder()
        { }

        private double _maxX;
        private double _maxY;
        private double _maxT;
        private Func<double, double, double> _initialCondition;
        private Func<double, double, double> _yBorder;
        private Func<double, double, double> _xBorder;
        private List<Tuple<double, double>> _steps;

        public static IEmptyBuilder Create()
        {
            return new MathNet3Builder
            {
                _steps = new List<Tuple<double, double>>()
            };
        }

        public IBuilderWithArea SetArea(double maxX, double maxY, double maxT)
        {
            _maxX = maxX;
            _maxY = maxY;
            _maxT = maxT;
            return this;
        }

        public IBuilderWithInitialCondition SetInitialCondition(Func<double, double, double> initialFunction)
        {
            _initialCondition = initialFunction;
            return this;
        }

        public IBuilderWithBorder SetBorder(Func<double, double, double> xFunction, Func<double, double, double> yFunction)
        {
            _xBorder = xFunction;
            _yBorder = yFunction;
            return this;
        }

        public ICompleteBuilder WithNet(double h, double d)
        {
            _steps.Add(Tuple.Create(h, d));
            return this;
        }

        public IEnumerable<MathNet3> Build()
        {
            //Для каждого эксперимента создаем сетку
            foreach (var step in _steps)
            {
                //Инициализируем сетку
                var net = new MathNet3(_maxX, _maxY, _maxT, step.Item1, step.Item2);
                //Заполняем узлы по начальным условиям, при t = 0
                for (var j = 0; j <= net.SizeY; j++)
                for (var i = 0; i <= net.SizeX; i++)
                {
                    var initialValue = _initialCondition(i * net.H, j * net.H);
                    net.Set(i, j, 0, initialValue);
                }
                //Заполняем узлы на границах, при x = 0, x = T
                
                for (var k = 0; k <= net.Height; k++)
                {
                    for (var j = 0; j <= net.SizeY; j++)
                    {
                        var value = _yBorder(j * net.H, k * net.D);
                        net.Set(0, j, k, value);
                        net.Set(net.SizeX, j, k, value);
                    }
                    for (var i = 0; i <= net.SizeX; i++)
                    {
                        var value = _xBorder(i * net.H, k * net.D);
                        net.Set(i, 0, k, value);
                        net.Set(i, net.SizeY, k, value);
                    }
                }
                //Возвращаем заполненую сетку
                yield return net;
            }
        }
    }
}