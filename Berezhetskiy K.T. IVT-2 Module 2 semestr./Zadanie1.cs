using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    internal class Zadanie1
    {
        static void Main()
        {
            int[] array = { 2, 3, 10, 5, 7, 6, 4, 12, 20, 9, 11, 22, 24, 25 };
            Console.WriteLine("Исходный массив:");
            Console.WriteLine(string.Join(" ", array));
            InsertionSort(array);
            Console.WriteLine("\nОтсортированный массив:");
            Console.WriteLine(string.Join(" ", array));
            Console.ReadLine();
        }
        static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = key;
            }
            /*//Трассировка первых 10 шагов:
            1) Вставка 3.
            2 3 10 5 7 6 4 12 20 9 11 22 24 25
            2) Вставка 10.
            2 3 10 5 7 6 4 12 20 9 11 22 24 25
            3)Вставка 5, 5 вставляется на место 10.
            2 3 5 10 7 6 4 12 20 9 11 22 24 25
            4) Вставка 7, 7 вставляется на место 10.
            2 3 5 7 10 6 4 12 20 9 11 22 24 25
            5) Вставка 6, 7 → 10, 6 вставляется на место 7.
            2 3 5 6 7 10 4 12 20 9 11 22 24 25
            6) Вставка 4, 7 → 10, 6 → 7, 5 → 6, 4 вставляется на место 5.
            2 3 4 5 6 7 10 12 20 9 11 22 24 25
            7) Вставка 12.
            2 3 4 5 6 7 10 12 20 9 11 22 24 25
            8) Вставка 20.
            2 3 4 5 6 7 10 12 20 9 11 22 24 25
            9) Вставка 9, 9 вставляется на место 10.
            2 3 4 5 6 7 9 10 12 20 11 22 24 25
            10) Вставка 11, 11 вставляется на место 12.
            2 3 4 5 6 7 9 10 11 12 20 22 24 25
            */
        }
    }
}