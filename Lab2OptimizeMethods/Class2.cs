using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using First;
using Second;

void main()
{
    Console.WriteLine("Выберите один из 2 способов для решения уравнения:\n1.Метод градиентного спуска\n2.Метод наискорейшего градиентного спуска");
    int n = int.Parse(Console.ReadLine());
    switch (n)
    {
        case 1:
            GradientDownComing GradDownCome = new GradientDownComing();
            break;
        case 2:
            GradientDownComingFast GradDownComeFast = new GradientDownComingFast();
            break;
        default:
            Console.WriteLine("Некорректный ввод");
            break;
    }
}
main();

