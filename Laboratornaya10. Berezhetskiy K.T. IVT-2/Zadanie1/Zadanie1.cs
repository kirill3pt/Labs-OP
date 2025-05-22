using System;
using System.Diagnostics;
using System.IO;

namespace Zadanie1
{
    class Zadanie1
    {
        const int ArraySize = 100000;
        const int MaxValue = 100000;
        const string OutputFile = "sorted.dat";

        static void Main()
        {
            var random = new Random();
            int[] randomArray = new int[ArraySize];
            for (int i = 0; i < ArraySize; i++)
                randomArray[i] = random.Next(MaxValue);

            int[] sortedAsc = (int[])randomArray.Clone();
            Array.Sort(sortedAsc);

            int[] sortedDesc = (int[])sortedAsc.Clone();
            Array.Reverse(sortedDesc);

            Console.WriteLine("== РЕЗУЛЬТАТЫ СОРТИРОВКИ ==\n");

            TestAll("Случайный", randomArray);
            TestAll("Отсортированный ↑", sortedAsc);
            TestAll("Отсортированный ↓", sortedDesc);

            Console.ReadLine();
        }

        static void TestAll(string label, int[] baseArray)
        {
            Console.WriteLine($"\n[{label} массив]");
            Test("Слиянием", baseArray, MergeSort);
            Test("Пирамидальная", baseArray, HeapSort);
            Test("Быстрая", baseArray, QuickSort);
        }

        delegate void SortMethod(int[] arr, out long comparisons, out long swaps, out TimeSpan time);

        static void Test(string name, int[] original, SortMethod method)
        {
            int[] arr = (int[])original.Clone();
            method(arr, out long cmp, out long swp, out TimeSpan time);

            Console.WriteLine($"{name,-15} | Время: {time.Seconds}.{time.Milliseconds:D3} сек | Сравнения: {cmp,-10} | Перестановки: {swp}");

            WriteToFile(arr);
            bool ok = CheckSortedFile();
            Console.WriteLine($"Проверка: {(ok ? "ОК" : "Ошибка")}");
        }

        static void MergeSort(int[] arr, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0; swaps = 0;
            var sw = Stopwatch.StartNew();
            MergeSortRecursive(arr, 0, arr.Length - 1, ref comparisons, ref swaps);
            sw.Stop(); time = sw.Elapsed;
        }

        static void MergeSortRecursive(int[] arr, int left, int right, ref long comparisons, ref long swaps)
        {
            if (left >= right) return;
            int mid = (left + right) / 2;
            MergeSortRecursive(arr, left, mid, ref comparisons, ref swaps);
            MergeSortRecursive(arr, mid + 1, right, ref comparisons, ref swaps);
            Merge(arr, left, mid, right, ref comparisons, ref swaps);
        }

        static void Merge(int[] arr, int left, int mid, int right, ref long comparisons, ref long swaps)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            while (i <= mid && j <= right)
            {
                comparisons++;
                if (arr[i] <= arr[j])
                    temp[k++] = arr[i++];
                else
                    temp[k++] = arr[j++];
                swaps++;
            }

            while (i <= mid) temp[k++] = arr[i++];
            while (j <= right) temp[k++] = arr[j++];

            for (int l = 0; l < temp.Length; l++) arr[left + l] = temp[l];
        }

        static void HeapSort(int[] arr, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0; swaps = 0;
            var sw = Stopwatch.StartNew();

            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i, ref comparisons, ref swaps);

            for (int i = n - 1; i > 0; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]); swaps++;
                Heapify(arr, i, 0, ref comparisons, ref swaps);
            }

            sw.Stop(); time = sw.Elapsed;
        }

        static void Heapify(int[] arr, int n, int i, ref long comparisons, ref long swaps)
        {
            int largest = i;
            int l = 2 * i + 1, r = 2 * i + 2;

            if (l < n) { comparisons++; if (arr[l] > arr[largest]) largest = l; }
            if (r < n) { comparisons++; if (arr[r] > arr[largest]) largest = r; }

            if (largest != i)
            {
                (arr[i], arr[largest]) = (arr[largest], arr[i]); swaps++;
                Heapify(arr, n, largest, ref comparisons, ref swaps);
            }
        }

        static void QuickSort(int[] arr, out long comparisons, out long swaps, out TimeSpan time)
        {
            comparisons = 0; swaps = 0;
            var sw = Stopwatch.StartNew();
            QuickSortRecursive(arr, 0, arr.Length - 1, ref comparisons, ref swaps);
            sw.Stop(); time = sw.Elapsed;
        }

        static void QuickSortRecursive(int[] arr, int low, int high, ref long comparisons, ref long swaps)
        {
            if (low < high)
            {
                int pi = Partition(arr, low, high, ref comparisons, ref swaps);
                QuickSortRecursive(arr, low, pi - 1, ref comparisons, ref swaps);
                QuickSortRecursive(arr, pi + 1, high, ref comparisons, ref swaps);
            }
        }

        static int Partition(int[] arr, int low, int high, ref long comparisons, ref long swaps)
        {
            Random rand = new Random();
            int pivotIndex = rand.Next(low, high + 1);
            (arr[pivotIndex], arr[high]) = (arr[high], arr[pivotIndex]);
            swaps++;

            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                comparisons++;
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[i], arr[j]) = (arr[j], arr[i]);
                    swaps++;
                }
            }

            (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
            swaps++;
            return i + 1;
        }

        static void WriteToFile(int[] arr)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile, FileMode.Create)))
                foreach (var x in arr) writer.Write(x);
        }

        static bool CheckSortedFile()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(OutputFile, FileMode.Open)))
            {
                if (reader.BaseStream.Length == 0) return false;
                int prev = reader.ReadInt32();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int curr = reader.ReadInt32();
                    if (curr < prev) return false;
                    prev = curr;
                }
            }
            return true;
        }
    }
}
