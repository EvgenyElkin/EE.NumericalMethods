using System;
using System.Collections.Generic;

namespace EE.NumericalMethods.Core.Common.Builders.MathNet3
{
    public interface IEmptyBuilder
    {
        /// <summary>
        /// Установить область
        /// </summary>
        /// <param name="maxX">Максиальное значение по 1 пространству</param>
        /// <param name="maxY">Максиальное значение по 2 пространству</param>
        /// <param name="maxT">Максиальное значение по времени</param>
        /// <returns>Билдер с областью</returns>
        IBuilderWithArea SetArea(double maxX, double maxY, double maxT);
    }

    public interface IBuilderWithArea
    {
        /// <summary>
        /// Определить начальное условие, 
        /// </summary>
        /// <param name="initialFunction">функция от одной переменной по пространству</param>
        /// <returns>Билдер с начальным условием</returns>
        IBuilderWithInitialCondition SetInitialCondition(Func<double, double, double> initialFunction);
    }

    public interface IBuilderWithInitialCondition
    {
        /// <summary>
        /// Определить граничные условия
        /// </summary>
        /// <param name="xBorderFunction">Левая граница, функция по времени</param>
        /// <param name="yBorderFunction">Правая граница, функция по времени</param>
        /// <returns>Билдер с граничными условиями</returns>
        IBuilderWithBorder SetBorder(Func<double, double, double> xBorderFunction, Func<double, double, double> yBorderFunction);
    }

    public interface IBuilderWithBorder
    {
        /// <summary>
        /// Установить шаги по сетке
        /// </summary>
        /// <param name="h">Шаг по пространству</param>
        /// <param name="d">Шаг по времени</param>
        /// <returns>Терминальный билдер, можно установить еще сетки</returns>
        ICompleteBuilder WithNet(double h, double d);
    }

    public interface ICompleteBuilder : IBuilderWithBorder
    {
        /// <summary>
        /// Сконструировать класс ответственный за запуск эксперементов
        /// </summary>
        /// <returns></returns>
        IEnumerable<MathNet3> Build();
    }
}