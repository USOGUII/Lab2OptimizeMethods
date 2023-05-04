using System;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;
using System.Xml.Resolvers;

namespace First
{
    class GradientDownComing
    {
        public GradientDownComing()
        {
            Console.WriteLine("Введите коэфициент перед x(1)^2:");
            double a = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите коэфициент перед x(1)*x(2):");
            double b = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите коэфициент перед x(2)^2:");
            double c = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите значение Epsilen(1):");
            double Accuracy1 = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите значение Epsilen(2):");
            double Accuracy2 = double.Parse(Console.ReadLine());
            double[] leftk = new double[20];
            double[] rightk = new double[20];
            Console.WriteLine("Введите значение левого края x(0):");
            double left = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите значение правого края x(0):");
            double right = double.Parse(Console.ReadLine());
            Console.WriteLine("Введите значение предельного числа итераций M:");
            int M = int.Parse(Console.ReadLine());

            leftk[0] = left;
            rightk[0] = right;
            double a2 = 2 * a;
            double c2 = 2 * c;

            int k;
            int flag = 0;
            double[] tk = new double[20];
            Console.WriteLine("Введите значение t(k), для целесообразности все остальные значения t(k) будут браться автоматически - для каждой новой итерации берется t(k)=t(k-1):");
            tk[0] = double.Parse(Console.ReadLine());
            tk[0] = 0.5;
            double[] xk = new double[20];
            double func1;
            double func;
            double rightforwork1;
            double rightforwork;
            Console.WriteLine("\n\n");

            for (k = 0; k <= M; k++, tk[k] = tk[k - 1])
            {
                Console.WriteLine($"Итерация {k}");
                xk[k] = Math.Abs(Math.Sqrt(Math.Pow((a2 * leftk[k] + rightk[k]), 2) + Math.Pow((leftk[k] + c2 * rightk[k]), 2)));

                rightforwork = a2 * leftk[k] + rightk[k];
                rightforwork1 = leftk[k] + c2 * rightk[k];
                leftk[k + 1] = leftk[k] - tk[k] * rightforwork;
                rightk[k + 1] = rightk[k] - tk[k] * rightforwork1;

                if (xk[k] < Accuracy1)
                {
                    Console.WriteLine($"Gradient f(x({k}) = {xk[k]} < {Accuracy1}");
                    Console.WriteLine($"t({k}) = {tk[k]}");
                    func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                    Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                    return;
                }
                Console.WriteLine($"Gradient f(x({k}) = {xk[k]} > {Accuracy1}");
                if (k >= M)
                {
                    Console.WriteLine($"t({k}) = {tk[k]}");
                    func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                    Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({leftk[k]}; {rightk[k]}), f(x) = ({Math.Round(func, 4)})");
                    return;
                }
            stepp7:

                rightforwork = a2 * leftk[k] + rightk[k];
                rightforwork1 = leftk[k] + c2 * rightk[k];

                leftk[k + 1] = leftk[k] - tk[k] * rightforwork;
                rightk[k + 1] = rightk[k] - tk[k] * rightforwork1;

                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                func1 = (a * Math.Pow(leftk[k + 1], 2) + b * leftk[k + 1] * rightk[k + 1] + c * Math.Pow(rightk[k + 1], 2));


                if (func1 - func > 0)
                {
                    Console.WriteLine($"f({k + 1}) - f({k}) = {func1 - func} > 0");
                    Console.WriteLine($"t({k}) = {tk[k]}");
                    tk[k] = tk[k] / 2;
                    goto stepp7;
                }

                Console.WriteLine($"f({k + 1}) - f({k}) = {func1 - func} < 0");
                Console.WriteLine($"t({k}) = {tk[k]}");

                double check = (Math.Abs(Math.Sqrt(Math.Pow((leftk[k + 1] - leftk[k]), 2) + Math.Pow((rightk[k + 1] - rightk[k]), 2))));
                double check1 = (Math.Abs((func1 - func)));

                if ((check < Accuracy2) && (Math.Abs((func1 - func)) < Accuracy2))
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)} < {Accuracy2} и |x({k + 1})-x({k})| = {check} < {Accuracy2}");
                    if (flag == 1)
                    {
                        Console.WriteLine($"Количество итераций - {k + 1}, X* искомое - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                        return;
                    }
                    flag++;
                }
                if (flag < 1)
                {
                    Console.WriteLine($"|f({k + 1}) - f({k})| = {Math.Abs(func1 - func)}; |x({k + 1})-x({k})| = {check}");
                }
                Console.WriteLine($"X*({k}) - ({Math.Round(leftk[k + 1], 4)}, {Math.Round(rightk[k + 1], 4)}), f(x) = ({Math.Round(func1, 4)})");
                Console.WriteLine("\n\n");
            }
        }
    }
}