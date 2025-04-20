using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        // читаем текст из файла text.txt в переменную text
        string text = File.ReadAllText("TestTextRU.txt");

        // просим пользователя ввести подстроку, которую будем искать
        Console.Write("введите подстроку для поиска: ");
        string understring = Console.ReadLine();

        // вызываем простой алгоритм поиска подстроки
        Console.WriteLine("\nПростой поиск:");
        simple(text, understring);

        // вызываем алгоритм Кнута-Морриса-Пратта (КМП)
        Console.WriteLine("\nПоиск Кнутта-Мориса-Пратта:");
        KMP(text, understring);

        // вызываем алгоритм Бойера-Мура (БМ)
        Console.WriteLine("\nПоиск Бойера-Мура");
        BM(text, understring);

        Console.ReadLine();
    }

    // простой алгоритм поиска: проверяет каждую позицию в тексте
    static void simple(string text, string understring)
    {
        int comparisons = 0; // счётчик сравнений
        Stopwatch sw = Stopwatch.StartNew(); // запускаем таймер

        // пробегаемся по каждой возможной позиции в тексте, где может начаться подстрока
        for (int i = 0; i <= text.Length - understring.Length; i++)
        {
            int j;
            // сравниваем символы текста и подстроки
            for (j = 0; j < understring.Length; j++)
            {
                comparisons++; // увеличиваем счётчик сравнений
                if (text[i + j] != understring[j]) // если не совпадает
                    break; // прерываем внутренний цикл
            }

            // если дошли до конца подстроки, значит она найдена
            if (j == understring.Length)
            {
                sw.Stop(); // останавливаем таймер
                result(i, comparisons, sw.Elapsed, text, understring.Length); // выводим результат
                return;
            }
        }

        sw.Stop(); // если не нашли — останавливаем таймер
        result(-1, comparisons, sw.Elapsed, text, understring.Length); // и выводим "не найдено"
    }

    // алгоритм КМП: использует префикс-функцию для пропуска повторяющихся символов
    static void KMP(string text, string understring)
    {
        int[] prefixes = PREFIXES(understring); // строим префикс-массив (массив lps)
        int i = 0, j = 0; // i — индекс в тексте, j — индекс в подстроке
        int comparisons = 0; // счётчик сравнений
        Stopwatch sw = Stopwatch.StartNew(); // запускаем таймер

        // пока не дошли до конца текста
        while (i < text.Length)
        {
            comparisons++; // увеличиваем счётчик сравнений

            if (text[i] == understring[j]) // если символы совпадают
            {
                i++; // двигаемся по тексту
                j++; // и по подстроке
            }

            // если вся подстрока совпала
            if (j == understring.Length)
            {
                sw.Stop(); // останавливаем таймер
                result(i - j, comparisons, sw.Elapsed, text, understring.Length); // выводим результат
                return;
            }
            // если символы не совпали
            else if (i < text.Length && text[i] != understring[j])
            {
                if (j != 0)
                    j = prefixes[j - 1]; // откатываемся по prefixes (пропускаем повторяющиеся символы)
                else
                    i++; // иначе двигаемся вперёд по тексту
            }
        }

        sw.Stop(); // если не нашли
        result(-1, comparisons, sw.Elapsed, text, understring.Length); // выводим "не найдено"
    }

    // строим массив префиксов для КМП
    static int[] PREFIXES(string understring)
    {
        int[] prefixes = new int[understring.Length]; // создаём массив
        int len = 0; // длина текущего совпадающего префикса и суффикса
        int i = 1; // начинаем со второго символа

        while (i < understring.Length)
        {
            // если символы совпадают
            if (understring[i] == understring[len])
            {
                len++; // увеличиваем длину совпадения
                prefixes[i] = len; // записываем её в массив
                i++;
            }
            else
            {
                if (len != 0)
                    len = prefixes[len - 1]; // откатываемся назад по lps
                else
                {
                    prefixes[i] = 0; // совпадений нет — ставим 0
                    i++;
                }
            }
        }
        return prefixes; // возвращаем массив префиксов
    }
    // алгоритм Бойера-Мура: использует таблицу плохих символов
    static void BM(string text, string understring)
    {
        int comparisons = 0;
        Dictionary<char, int> badChar = BADCHAR(understring); // строим таблицу смещений
        int shift = 0;
        Stopwatch sw = Stopwatch.StartNew();

        while (shift <= text.Length - understring.Length)
        {
            int j = understring.Length - 1;

            // сравниваем символы с конца подстроки
            while (j >= 0 && understring[j] == text[shift + j])
            {
                comparisons++;
                j--;
            }

            if (j < 0)
            {
                sw.Stop();
                result(shift, comparisons, sw.Elapsed, text, understring.Length);
                return;
            }
            else
            {
                comparisons++;
                char mismatchChar = text[shift + j];
                int badIndex = badChar.ContainsKey(mismatchChar) ? badChar[mismatchChar] : -1;
                shift += Math.Max(1, j - badIndex);
            }
        }

        sw.Stop();
        result(-1, comparisons, sw.Elapsed, text, understring.Length);
    }

    // строим таблицу плохих символов для БМ
    static Dictionary<char, int> BADCHAR(string symbol)
    {
        var table = new Dictionary<char, int>();

        // записываем индекс последнего появления каждого символа
        for (int i = 0; i < symbol.Length; i++)
            table[symbol[i]] = i;

        return table;
    }

    // выводит результат поиска + контекст
    static void result(int index, int comparisons, TimeSpan time, string text, int patternLength)
    {
        if (index >= 0)
        {
            Console.WriteLine($"найдено на позиции: {index}");
            Console.WriteLine("контекст: " + TextAround(text, index, 20));
        }
        else
            Console.WriteLine("не найдено");

        Console.WriteLine($"сравнений: {comparisons}");
        if (time.TotalMilliseconds > 1)
            {
                Console.WriteLine($"время: {time.Seconds} с : {time.Milliseconds} мс");
            }
            else
            {
                Console.WriteLine($"время: {time.TotalMilliseconds:F3} мс");
            }
    }

    // возвращает подстроку вокруг найденного фрагмента
    static string TextAround(string text, int position, int radius)
    {
        int start = Math.Max(0, position - radius);
        int end = Math.Min(text.Length, position + radius);
        return text.Substring(start, end - start);
    }
}
