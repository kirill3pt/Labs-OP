using System;

namespace Zadanie4
{
    class Zadanie4
    {
        static void Main()
        {
            int factorial(int x)
            {
                int result = 1, i = 1;

                do
                {
                    result *= i;
                    i++;
                }
                while (i <= x);

                return result;
            }

            double q, temp = 0;
            int x1, step = 2;

            Console.WriteLine("Введите x (целочисленный тип): ");
            x1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите q (вещественный тип): ");
            q = double.Parse(Console.ReadLine());

            while (true)
            {
                if (step == 2)
                {
                    temp = 1;
                }
                else temp = step % 4 != 0 ? temp - (Math.Pow(x1, step) / factorial(step)) : temp + (Math.Pow(x1, step) / factorial(step));

                Console.WriteLine($"Промежуток = ({x1}^2/{step}!), вычисление: {temp} > {q}");
                step += 2;

                if (temp < q)
                {
                    Console.WriteLine("Последнее выражение и последующее < q. Выход из программы.");
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
