using System;

namespace Zadanie3
{
    class Zadanie3
    {
        static void Main()
        {
            int chislo, a1 = 1, b1 = 1, sum = 0, count = 0;

            Console.WriteLine("Введите ограничения для ряда чисел Фиббоначчи: ");
            chislo = int.Parse(Console.ReadLine());
            Console.WriteLine("Числа фиббоначчи в пределах заданного ограничения: ");

            while (chislo >= sum)
            {
                sum = a1 + b1;
                Console.Write(sum + " ");
                a1 = b1;
                b1 = sum;

                if (sum > 999 && sum < 9999)
                {
                    count++;
                }
            }
            Console.WriteLine($"\nВ ряде фиббоначчи, заданного с ограничением ({chislo}), {count} четырёхзначных числа");
            Console.ReadLine();


        }
    }
}
