using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    internal class Zadanie4
    {
        static void Main()
        {
            int[] array = { 2, 3, 10, 5, 7, 6, 4, 12, 20, 9, 11, 22, 24, 25 };
            int twenty = 20;
            Array.Sort(array);//опять же, как и в задании 2 - сортируем массив, т.к.
                              //интерполяционный поиск - улучшенный вид бинарного поиска
            Console.WriteLine("Исходный массив: " + string.Join(" ", array));
            int index = InterpolationSearch(array, twenty);

            if (index != -1)
            {
                Console.WriteLine($"Элемент {twenty} найден в позиции {index}");
            }
            else
            {
                Console.WriteLine($"Элемент {twenty} не найден");
            }
            Console.ReadLine();
        }
        static int InterpolationSearch(int[] array, int chislo)
        {
            int left = 0, right = array.Length - 1;

            while (left <= right && chislo >= array[left] && chislo <= array[right])
            {
                if (left == right)
                {
                    if (array[left] == chislo) return left;
                    return -1;
                }
                int pos = left + ((chislo - array[left]) * (right - left) / (array[right] - array[left]));
                Console.WriteLine($"Проверяем позицию: {pos}, значение: {array[pos]}");
                if (array[pos] == chislo)
                {
                    return pos;
                }
                if (array[pos] < chislo)
                {
                    left = pos + 1;
                }
                else
                {
                    right = pos - 1;
                }
            }
            return -1;
        }
        /*Трассировка:
        Исходный массив: 2 3 4 5 6 7 9 10 11 12 20 22 24 25 (отсортирован)
        1 шаг: left = 0; right = 13; тогда int pos = left + ((chislo - array[left]) * (right - left) / (array[right] - array[left])); =  0 + ((20 - 0) * (13 - 0) / (13 - 0)) . значение 10 элемента массива = 20.
        Число найдено с первого шага.*/
    }
}
