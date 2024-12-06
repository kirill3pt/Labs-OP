using System;

namespace Zadanie2
{
    class Zadanie2
    {
        static void Main()
        {
            double pi = 0, temp = 0;
            int step = 3, slagaemoe = 0;

            Console.Write("Введите количество слагаемых: ");
            slagaemoe = int.Parse(Console.ReadLine());

            for (int i = 0; i < slagaemoe; i++)
            {
                if (i == 0)
                {
                    temp = 1;
                }

                else temp = (i % 2 == 0) ? temp += 1.0 / step : temp -= 1.0 / step;
                Console.WriteLine($"{i} значение на промежутке (1/{step}) = {temp}");
                step += 2;
            }

            pi = 4 * temp;
            Console.WriteLine($"Число pi = {pi}");
            Console.ReadLine();
        }
    }
}
