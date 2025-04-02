using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    internal class Zadanie3
    {
        static void Main()
        {
            int[] array = { 2, 3, 10, 5, 7, 6, 4, 12, 20, 9, 11, 22, 24, 25 };
            Console.WriteLine("Исходный массив: " + string.Join(" ", array));
            SelectionSort(array);
            Console.WriteLine("Отсортированный массив: " + string.Join(" ", array));
            Console.ReadLine();
        }
        static void SelectionSort(int[] array)
        {
            int n = array.Length;
            int swapCount = 0;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < n; j++)
                {
                    if (array[j] < array[minIndex])
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    (array[i], array[minIndex]) = (array[minIndex], array[i]);
                    swapCount++;
                    if (swapCount == 10)
                    {
                        return;
                    }
                }
                /*Трассировка:
                1) Вставка 10 - 4:
                2 3 4 5 7 6 10 12 20 9 11 22 24 25
                2) Вставка 7 - 6:
                2 3 4 5 6 7 10 12 20 9 11 22 24 25
                3) Вставка 20 - 9:
                2 3 4 5 6 7 10 12 9 20 11 22 24 25
                4) Вставка 20 - 11:
                2 3 4 5 6 7 10 12 9 11 20 22 24 25
                5) Вставка 12 - 9:
                2 3 4 5 6 7 10 9 12 11 20 22 24 25
                6) Вставка 10 - 9:
                2 3 4 5 6 7 9 10 12 11 20 22 24 25
                7) Вставка 12 - 11:
                2 3 4 5 6 7 9 10 11 12 20 22 24 25
                8) Вставка не нужна
                2 3 4 5 6 7 9 10 11 12 20 22 24 25
                9) Вставка не нужна
                2 3 4 5 6 7 9 10 11 12 20 22 24 25
                10) Вставка не нужна
                2 3 4 5 6 7 9 10 11 12 20 22 24 25*/
            }
        }
    }
}
