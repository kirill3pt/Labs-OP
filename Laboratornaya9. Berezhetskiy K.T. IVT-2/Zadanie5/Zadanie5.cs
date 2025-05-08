using System;
using System.Collections.Generic;
using System.IO;

namespace Zadanie5
{
    internal class Zadanie5
    {
        static void Main()
        {
            Console.Write("Введите путь к текстовому файлу: ");
            string filePath = Console.ReadLine();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            string text = File.ReadAllText(filePath);
            char[] separators = {' '};
            string[] words = text.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word] = wordCount[word] + 1;
                }
                else
                {
                    wordCount[word] = 1;
                }
            }

            // Переводим словарь в список для ручной сортировки
            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
            foreach (KeyValuePair<string, int> pair in wordCount)
            {
                list.Add(pair);
            }

            // Сортируем список: сначала по убыванию количества, потом по алфавиту
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j].Value > list[i].Value ||
                        (list[j].Value == list[i].Value && string.Compare(list[j].Key, list[i].Key) < 0))
                    {
                        // меняем местами
                        KeyValuePair<string, int> temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }

            Console.WriteLine("\n10 наиболее встречаемых слов:");
            for (int i = 0; i < 10 && i < list.Count; i++)
            {
                string word = list[i].Key;
                int count = list[i].Value;
                string form = GetForm(count);
                Console.WriteLine(word + " — " + count + " " + form);
            }

            Console.ReadLine();
        }

        static string GetForm(int n)
        {
            if (n % 10 == 1 && n % 100 != 11)
            {
                return "раз";
            }
            else if (n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20))
            {
                return "раза";
            }
            else
            { 
                return "раз";
            }
        }
    }
}
