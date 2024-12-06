using System;

namespace Zadanie6
{
    internal class Zadanie6
    {
        static void Main()
        {
        repeat:
            int n;
            string god = "год";
            string goda = "года";
            string leto = "лет";
            char quit;

            Console.Write("Введите число N от 1 до 100: ");
            n = int.Parse(Console.ReadLine());

            if (n > 100)
            {
                Console.WriteLine("Число > 100.");
            }
            else if (n % 10 == 1 && n != 11)
            {
                Console.WriteLine($"{n} {god}");
            }
            else if (n % 10 >= 2 && n % 10 <= 4 && n >= 22 || n <= 4)
            {
                Console.WriteLine($"{n} {goda}");
            }
            else
            {
                Console.WriteLine($"{n} {leto}");
            }

            Console.Write("Хотите продолжить? y/n ");
            quit = char.Parse(Console.ReadLine());
            if (quit == 'y')
            {
                goto repeat;
            }
            else if (quit == 'n')
            {
                Console.WriteLine("Выход из программы.");
            }

            Console.ReadLine();
        }
    }
}
