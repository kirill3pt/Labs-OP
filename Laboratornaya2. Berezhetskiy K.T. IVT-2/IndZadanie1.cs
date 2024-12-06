using System;

namespace IndZadanie1
{
    class IndZadanie1
    {
        static void Main()
        {
            int a, b;
            double koren;
            Console.Write("Введите число a: ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Введите число b: ");
            b = int.Parse(Console.ReadLine());

            if (a < 0 || b < 0)
            {
                Console.WriteLine("Error!");
            }
            else
            {
                koren = a < b ? Math.Sqrt(a) : Math.Sqrt(b);
                Console.WriteLine($"Корень наименьшего числа = {koren}");
            }
            Console.ReadLine();
        }
    }
}
