using System;


namespace IndZadanie2
{
    class IndZadanie2
    {
        static void Main()
        {
            int a;
            Console.WriteLine("Введите число a (от 1 до 10000): ");
            a = int.Parse(Console.ReadLine());

            Console.WriteLine("Нечетные делители числа:");
            for (int delitel = 1; delitel <= a; delitel++)
            {
                if (a % delitel == 0 && delitel % 2 != 0)
                {
                    Console.WriteLine(delitel);
                }
            }
            Console.ReadLine();
        }
    }
}
