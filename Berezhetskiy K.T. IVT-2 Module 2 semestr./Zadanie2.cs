using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie2
{
    class Zadanie2
    {
        static void Main()
        {
            int[] array = { 2, 3, 10, 5, 7, 6, 4, 12, 20, 9, 11, 22, 24, 25 };

            int twenty = 20;
            Array.Sort(array); //т.к. бинарный поиск работает только с отсортированными массивами,
                               //то надо его изначально перед выполнением отсортировать
            int index = BinarySearch(array, twenty);

            if (index != -1)
            {
                Console.WriteLine($"Число {twenty} найдено на позиции {index}.");
            }
            Console.ReadLine();

        }
        static int BinarySearch(int[] array, int chislo)

        {
            int left = 0, right = array.Length - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (array[mid] == chislo)
                {
                    return mid;
                }
                else if (array[mid] < chislo)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return -1;
        }
        /*Трассировка поиска числа 20:
        Сначала определяем середину массива: 
        int left = 0, right = array.Length - 1;
        int mid = left + (right - left) / 2; тут mid = (0 + 13) / 2 = 6. 6 элемент отсортированного массива - это 9. 
        значит, не подходит.идем дальше
        выполняем условие:

        else if (array[mid] < chislo)
                {
                    left = mid + 1;
                }
        тогда left = 7, right = 13.
         mid = left + (right - left) / 2 = 10; 10 элемент массива - это 20. элемент найден.*/
    }
}
