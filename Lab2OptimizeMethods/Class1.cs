using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second
{
    class GradientDownComingFast
    {
        public GradientDownComingFast()
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
                xk[k] = (Math.Sqrt(Math.Pow((a2 * leftk[k] + rightk[k]), 2) + Math.Pow((leftk[k] + c2 * rightk[k]), 2)));

                rightforwork = a2 * leftk[k] + rightk[k];
                rightforwork1 = leftk[k] + c2 * rightk[k];
                leftk[k + 1] = leftk[k] - tk[k] * rightforwork;
                rightk[k + 1] = rightk[k] - tk[k] * rightforwork1;

                if (Math.Abs(xk[k]) < Accuracy1)
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

                tk[k] = ActualTk(leftk[k], rightk[k], a, b, c);

                rightforwork = a2 * leftk[k] + rightk[k];
                rightforwork1 = leftk[k] + c2 * rightk[k];

                leftk[k + 1] = leftk[k] - tk[k] * rightforwork;
                rightk[k + 1] = rightk[k] - tk[k] * rightforwork1;


                func = (a * Math.Pow(leftk[k], 2) + b * leftk[k] * rightk[k] + c * Math.Pow(rightk[k], 2));
                func1 = (a * Math.Pow(leftk[k + 1], 2) + b * leftk[k + 1] * rightk[k + 1] + c * Math.Pow(rightk[k + 1], 2));

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
        public static double ActualTk(double x, double y, double a, double b, double c) //Подсчет значения min tk(возвращаю tk как сумму квадратов под корнем)
        {
            int situation = 0;
            double exp1 = 2 * a * b * b * x * x + 4 * a * b * c * x * y + b * b * b * x * y + 2 * b * b * c * y * y;
            double exp2 = b * x * x + 4 * b * c * x * y + 4 * c * c * y * y;
            double exp3 = -2 * b * b * c * x * x - 8 * b * c * c * x * y - 8 * c * c * c * y * y;
            double v1 = 8 * a * a * a * x * x + 8 * a * a * b * x * y + 2 * a * b * b * y * y;
            double v2 = 2 * a * b * b * x * x + 4 * a * b * c * x * y + b * b * b * x * y + 2 * b * b * c * y * y;
            double v3 = 4 * a * a * x * x + 4 * a * b * x * y + b * b * y * y;
            if (situation == 1)
            {
                double t1 = (v3 + (exp2 * v2) / exp3) / (v1 + (exp1 * v2) / exp3);
                double t2 = (t1 * exp1 - exp2) / exp3;
                return Math.Sqrt(Math.Pow(t1, 2) + Math.Pow(t2, 2));
            }
            double proverca1 = -4 * a * a * x * x + 4 * a * b * x * y + b * b * x * x + 4 * b * c * x * y + b * b * y * y + 4 * c * y * y;
            double proverca2 = v1 + 4 * a * b * b * x * x + 8 * a * b * c * x * y + 2 * b * b * b * x * y + 4 * b * b * c * y * y - 4 * c * y * y - exp3;
            return (Math.Sqrt(Math.Pow(proverca1 / proverca2, 2) + Math.Pow(proverca1 / proverca2, 2)));
        }
    }
}
