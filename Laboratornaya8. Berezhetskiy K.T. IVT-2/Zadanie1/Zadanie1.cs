using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Zadanie1
{
    class Program
    {
        static void Main()
        {
            // путь к бинарному файлу, откуда берём отсортированные числа
            string path = @"C:\Users\Kirill\source\repos\LAB7\Zadanie2\bin\Debug\sorted.dat";
            int[] data = ReadData(path); // читаем и загружаем данные

            // просим пользователя ввести число
            Console.Write("Введите искомое число: ");
            int chislo = int.Parse(Console.ReadLine());

            // вызываем все 3 поиска по очереди
            Console.WriteLine("\nЛинейный поиск:");
            Linear(data, chislo);

            Console.WriteLine("\nБинарный поиск:");
            Binary(data, chislo);

            Console.WriteLine("\nИнтерполяционный поиск:");
            Interpolation(data, chislo);

            Console.ReadLine(); // чтобы консоль не закрывалась сразу
        }

        // читаем данные из бинарного файла
        static int[] ReadData(string path)
        {
            List<int> data = new List<int>();
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                // читаем все числа пока не конец файла
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    data.Add(reader.ReadInt32());
                }
            }
            data.Sort(); // сортируем на всякий случай
            return data.ToArray(); // возвращаем как массив
        }

        // обычный линейный поиск — по одному проверяем каждый элемент
        static void Linear(int[] data, int target)
        {
            int comparisons = 0;
            Stopwatch sw = Stopwatch.StartNew(); // включаем таймер

            for (int i = 0; i < data.Length; i++)
            {
                comparisons++; // считаем сравнение
                if (data[i] == target) // если нашли
                {
                    sw.Stop(); // выключаем таймер
                    results(i, comparisons, sw.Elapsed); // выводим результат
                    return;
                }
            }

            sw.Stop(); // если не нашли, тоже выключаем таймер
            results(-1, comparisons, sw.Elapsed); // выводим "не найдено"
        }

        // бинарный поиск — делим массив пополам каждый раз
        static void Binary(int[] data, int target) // O(logN)
        {
            int left = 0, right = data.Length - 1;
            int comparisons = 0;
            Stopwatch sw = Stopwatch.StartNew(); // стартуем таймер

            while (left <= right)
            {
                int mid = (left + right) / 2; // находим середину
                comparisons++; // считаем сравнение

                if (data[mid] == target)
                {
                    sw.Stop();
                    results(mid, comparisons, sw.Elapsed);
                    return;
                }
                if (data[mid] < target)
                    left = mid + 1; // ищем в правой части
                else
                    right = mid - 1; // ищем в левой части
            }

            sw.Stop();
            results(-1, comparisons, sw.Elapsed);
        }

        // интерполяционный поиск — оцениваем примерную позицию
        static void Interpolation(int[] data, int target)
        {
            int comparisons = 0;
            int low = 0, high = data.Length - 1;
            Stopwatch sw = Stopwatch.StartNew();

            // проверка на диапазон (обязательна)
            while (low <= high && target >= data[low] && target <= data[high])
            {
                comparisons++;

                // если остался 1 элемент
                if (low == high)
                {
                    if (data[low] == target)
                    {
                        sw.Stop();
                        results(low, comparisons, sw.Elapsed);
                        return;
                    }
                    break;
                }

                // формула интерполяции (оценка позиции)
                int pos = low + ((target - data[low]) * (high - low)) / (data[high] - data[low]);
                if (pos < 0 || pos >= data.Length) break;

                comparisons++;
                if (data[pos] == target)
                {
                    sw.Stop();
                    results(pos, comparisons, sw.Elapsed);
                    return;
                }
                if (data[pos] < target)
                    low = pos + 1;
                else
                    high = pos - 1;
            }

            sw.Stop();
            results(-1, comparisons, sw.Elapsed);
        }

        // выводим результат поиска
        static void results(int index, int comparisons, TimeSpan time)
        {
            if (index >= 0)
                Console.WriteLine($"Найдено на позиции: {index}");
            else
                Console.WriteLine("Не найдено");

            Console.WriteLine($"Сравнений: {comparisons}");
            if (time.TotalMilliseconds > 1)
            {
                Console.WriteLine($"Время: {time.Seconds} с : {time.Milliseconds} мс");
            }
            else
            {
                Console.WriteLine($"Время: {time.TotalMilliseconds:F3} мс");
            }
        }
    }
}