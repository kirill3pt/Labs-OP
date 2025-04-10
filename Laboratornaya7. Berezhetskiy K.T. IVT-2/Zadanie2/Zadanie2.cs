using System;
using System.Diagnostics;
using System.IO;

namespace Zadanie2
{
    class Zadanie2
    {
        // константы для настроек программы
        const string OutputFile = "sorted.dat"; // имя файла для сохранения результатов
        const int ArraySize = 100000; // размер тестового массива
        const int MaxValue = 100000; // максимальное значение элемента массива
        static int totalTests = 0;
        static int passedTests = 0;

        // делегат для методов сортировки
        delegate void SortMethod(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time);
        static void Main()
        {
            // генерация тестовых данных
            var random = new Random();
            int[] randomArray = new int[ArraySize]; // случайный массив
            for (int i = 0; i < ArraySize; i++)
            {
                randomArray[i] = random.Next(MaxValue);
            }

            // создание отсортированных версий массива
            int[] upArray = (int[])randomArray.Clone(); // по возрастанию
            Array.Sort(upArray);

            int[] downArray = (int[])randomArray.Clone(); // по убыванию
            Array.Sort(downArray);
            Array.Reverse(downArray);

            // тестирование на разных типах массивов
            Console.WriteLine("РЕЗУЛЬТАТЫ");
            Test("Случайный массив", randomArray);
            Test("Отсортированный массив по возрастанию", upArray);
            Test("Обратно отсортированный массив по убыванию", downArray);
            PrintCheck();
            Console.WriteLine("\nВыход?");
            Console.ReadLine();
        }

        // функция тестирования всех алгоритмов для конкретного массива
        static void Test(string caseName, int[] baseArray)
        {
            Console.WriteLine($"\n{caseName}:");
            Output("Выбором:", baseArray, SelectionSort);
            Output("Вставками:", baseArray, InsertionSort);
            Output("Пузырьком:", baseArray, BubbleSort);
            Output("Шейкерная:", baseArray, ShakerSort);
            Output("Шелла:", baseArray, ShellSort);
        }

        static void Output(string sortName, int[] baseArray, SortMethod sortMethod)
        {
            int[] arr = (int[])baseArray.Clone();
            sortMethod(arr, true, out long comparisons, out long swaps, out TimeSpan time);
            Console.WriteLine($"{sortName} {time.Seconds}.{time.Milliseconds:D2} сек | " +
                             $"{comparisons} сравнений | " +
                             $"{swaps} перестановок");

            
            Write(arr);
            bool isSorted = Check();
            totalTests++;
            if (isSorted)
            {
                passedTests++;
            }
        }
        static void PrintCheck()
        {
            Console.WriteLine("\nПРОВЕРКА СОРТИРОВКИ:");
            Console.WriteLine($"Всего тестов: {totalTests}");
            Console.WriteLine($"Успешно отсортировано: {passedTests}");

            if (totalTests == passedTests)
            {
                Console.WriteLine("Все файлы корректно отсортированы");
            }
            else
            {
                Console.WriteLine($"Обнаружены ошибки в {totalTests - passedTests} случаях");
            }
        }

        // алгоритм сортировки выбором
        static void SelectionSort(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0;
            swaps = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int extreme = i; // индекс минимального/максимального элемента

                // поиск экстремального элемента в неотсортированной части
                for (int j = i + 1; j < arr.Length; j++)
                {
                    comparisons++;
                    if (upORdown ? arr[j] < arr[extreme] : arr[j] > arr[extreme])
                        extreme = j;
                }

                // обмен элементов, если нашли новый экстремум
                if (extreme != i)
                {
                    swaps++;
                    (arr[i], arr[extreme]) = (arr[extreme], arr[i]); // обмен значениями
                }
            }

            sw.Stop();
            time = sw.Elapsed;
        }

        // алгоритм сортировки вставками
        static void InsertionSort(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0;
            swaps = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 1; i < arr.Length; i++)
            {
                // вставка элемента в отсортированную часть
                for (int j = i; j > 0; j--)
                {
                    comparisons++;
                    if (upORdown ? arr[j - 1] <= arr[j] : arr[j - 1] >= arr[j])
                        break;

                    swaps++;
                    (arr[j - 1], arr[j]) = (arr[j], arr[j - 1]); // обмен значениями
                }
            }

            sw.Stop();
            time = sw.Elapsed;
        }

        // алгоритм пузырьковой сортировки
        static void BubbleSort(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0;
            swaps = 0;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                // проход по неотсортированной части
                for (int j = arr.Length - 1; j > i; j--)
                {
                    comparisons++;
                    if (upORdown ? arr[j - 1] > arr[j] : arr[j - 1] < arr[j])
                    {
                        swaps++;
                        (arr[j - 1], arr[j]) = (arr[j], arr[j - 1]); // обмен значениями
                    }
                }
            }

            sw.Stop();
            time = sw.Elapsed;
        }

        // алгоритм шейкерной сортировки (усовершенствованная пузырьковая)
        static void ShakerSort(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0;
            swaps = 0;
            var sw = Stopwatch.StartNew();

            int left = 0, right = arr.Length - 1;
            while (left <= right)
            {
                // проход слева направо
                for (int i = left; i < right; i++)
                {
                    comparisons++;
                    if (upORdown ? arr[i] > arr[i + 1] : arr[i] < arr[i + 1])
                    {
                        swaps++;
                        (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                    }
                }
                right--; // уменьшаем правую границу

                // проход справа налево
                for (int i = right; i > left; i--)
                {
                    comparisons++;
                    if (upORdown ? arr[i - 1] > arr[i] : arr[i - 1] < arr[i])
                    {
                        swaps++;
                        (arr[i - 1], arr[i]) = (arr[i], arr[i - 1]);
                    }
                }
                left++; // увеличиваем левую границу
            }

            sw.Stop();
            time = sw.Elapsed;
        }

        // алгоритм сортировки шелла (с убывающим шагом)
        static void ShellSort(int[] arr, bool upORdown, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0;
            swaps = 0;
            var sw = Stopwatch.StartNew();

            // последовательность шагов
            int[] steps = { 57, 23, 10, 4, 1 };

            foreach (int step in steps)
            {
                for (int i = step; i < arr.Length; i++)
                {
                    int temp = arr[i], j = i;
                    // сортировка вставками с заданным шагом
                    while (j >= step)
                    {
                        comparisons++;
                        if (upORdown ? arr[j - step] <= temp : arr[j - step] >= temp)
                            break;

                        swaps++;
                        arr[j] = arr[j - step];
                        j -= step;
                    }
                    arr[j] = temp;
                }
            }

            sw.Stop();
            time = sw.Elapsed;
        }

        // запись массива в бинарный файл
        static void Write(int[] arr)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile, FileMode.Create)))
            {
                foreach (var item in arr)
                {
                    writer.Write(item); // запись каждого элемента
                }
            }
        }

        // проверка отсортированности данных в файле
        static bool Check()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(OutputFile, FileMode.Open)))
            {
                if (reader.BaseStream.Length == 0)
                {
                    return false; // пустой файл
                }

                int prev = int.MinValue;
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int current = reader.ReadInt32();
                    if (current < prev)
                    {
                        return false; // нарушен порядок сортировки
                    }
                    prev = current;
                }
            }
            return true; // данные отсортированы
        }
    }
}