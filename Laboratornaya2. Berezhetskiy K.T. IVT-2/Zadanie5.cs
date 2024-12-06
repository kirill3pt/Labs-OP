using System;

namespace Zadanie5
{
    class Zadanie5
    {
        static void Main()
        {
            int n, x, y, z;
            double sum;

            bool combination = false;

            Console.WriteLine("Введите число N, для показа кол-ва возможных комбинаций x^3+y^3+z^3 = N");
            n = int.Parse(Console.ReadLine());

            for (x = 0; x < n; x++)
            {
                for (y = 0; y < n; y++)
                {
                    for (z = 0; z < n; z++)
                    {
                        sum = Math.Pow(x, 3) + Math.Pow(y, 3) + Math.Pow(z, 3);
                        if (sum == n)
                        {
                            Console.WriteLine($"{x}^3 + {y}^3 + {z}^3 = {n}");
                            combination = true;

                        }
                    }
                }
            }
            if (!combination)
            {
                Console.WriteLine("No Such Combinations!");
            }

            Console.ReadLine();
        }
    }
}
