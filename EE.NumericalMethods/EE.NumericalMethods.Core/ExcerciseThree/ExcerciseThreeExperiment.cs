using System;
using EE.NumericalMethods.Core.Common;
using EE.NumericalMethods.Core.Common.Builders.MathNet2;

namespace EE.NumericalMethods.Core.ExcerciseThree
{
    public class ExcerciseThreeExperiment : IExperiment
    {
        public void Run()
        {
            //�������� ����� � �������� �����������
            var nets = MathNet2Builder.Create()                                     
                .SetArea(Math.PI, 8)
                .SetInitialCondition(Math.Sin)
                .SetBorder(t => 0, t => 0)
                .WithNet(Math.PI / 5, Math.Pow(2, -2))
                .WithNet(Math.PI / 10, Math.Pow(2, -2))
                .WithNet(Math.PI / 10, Math.Pow(2, -3))
                .WithNet(Math.PI / 20, Math.Pow(2, -4))
                .WithNet(Math.PI / 40, Math.Pow(2, -5))
                .WithNet(Math.PI / 80, Math.Pow(2, -6))
                .WithNet(Math.PI / 160, Math.Pow(2, -7))
                .WithNet(Math.PI / 320, Math.Pow(2, -8))
                .WithNet(Math.PI / 640, Math.Pow(2, -9))
                .WithNet(Math.PI / 1280, Math.Pow(2, -10))
                .Build();

            //�������� ����� � ������� ������ ��� ����������
            var method = new NonLinearCrankNicolsonMethod((x,t) => 
                -Math.Exp(Math.Sin(x) * Math.Cos(t)) * Math.Sin(x) * Math.Sin(t) + Math.Sin(x) * Math.Cos(t),
                Math.Exp,
                Math.Exp);

            //����� ������
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("����� \"{0}\":", method.Name);
            Console.ResetColor();
            //���������� ������ ��� ������� ������������
            foreach (var net in nets)
            {
                //��������� ������� �� �����, � ������� ������
                method.Compute(net);
                //��������� ������, ��� �������� �������� ������� �� ������ �����
                var error = net.GetError((x, t) => Math.Sin(x) * Math.Cos(t));

                Console.WriteLine("({0};{1})={2:g4}", net.Width, net.Height, error);
            };
        }
    }
}