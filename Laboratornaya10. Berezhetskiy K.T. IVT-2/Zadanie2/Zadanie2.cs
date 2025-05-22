using System;

namespace Zadanie2
{
    class Zadanie2
    {
        static void Main()
        {
            // допустимые номиналы купюр
            int[] nominal = { 1, 2, 5, 10, 20, 50, 100 };

            // массив из 100 случайных купюр
            int[] money = new int[100];
            Random rnd = new Random();
            for (int i = 0; i < money.Length; i++)
            {
                money[i] = nominal[rnd.Next(nominal.Length)];

            }

            Console.WriteLine("исходный массив купюр:");
            print(money);

            // сортировка подсчетом
            int[] sorted = sort(money, nominal);

            Console.WriteLine("\nотсортированный массив купюр:");
            print(sorted);
            Console.ReadLine();
        }

        // сортировка подсчётом (учитывает только заданные номиналы)
        static int[] sort(int[] input, int[] values)
        {
            // создаём словарь для подсчёта каждого номинала
            int[] count = new int[values.Length];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if (input[i] == values[j])
                    {
                        count[j]++;
                        break;
                    }
                }
            }

            // формируем отсортированный массив
            int[] output = new int[input.Length];
            int index = 0;
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < count[i]; j++)
                {
                    output[index++] = values[i];
                }
            }

            return output;
        }

        static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i],4}");
                if ((i + 1) % 20 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}