using System;
using System.Collections.Generic;

namespace Zadanie1
{
    internal class Zadanie1
    {
        static void Main()
        {
            Console.WriteLine("Введите предложение, завершите ввод точкой");
            string predlozhenie = Console.ReadLine();
            if (predlozhenie.EndsWith("."))//тут проверили что ввод заканчивается точкой, по условию
            {
                Console.WriteLine("Ввод корректен.");
            }
            else
            {
                Console.WriteLine("Ввод неправильный. Программа завершена. Необходимо ввести предложение и закончить ввод точкой\n" +
                    "нажмите 'Enter' чтобы выйти");
                Console.ReadLine();
                return;
            }
            predlozhenie = predlozhenie.Replace(",", "").Replace(".", "").Replace("-", ""); //заменяем на пробел, чтобы в вывод не шли запятые и точка, которые могут встретиться в предложении
            Console.WriteLine("\nЧерез обработку массива символов:");
            ThroughArray(predlozhenie);
            Console.WriteLine("\nЧерез методы string:");
            ThroughString(predlozhenie);
            Console.ReadLine();
        }
        static void ThroughArray(string text)//метод обработки строки как массив
        {
            text = text.ToLower();
            char[] massivSimvolov = text.ToCharArray();//переводим текст в массив символов
            Dictionary<char, int> counter = new Dictionary<char, int>(); //создаем новый словарь, который считает и сохраняет в себя место символа и этот символ

            foreach (char c in massivSimvolov)
            {
                if (counter.ContainsKey(c)) //если этот счётчик уже содержит эту букву, то тогда увеличиваем этот счетчик (counter[c]++)
                {
                    counter[c]++; 
                }
                else
                {
                    counter[c] = 1;//иначе записываем символ в словарь со значением 1
                }
            }

            foreach (var item in counter)//вывод символов
            {
                if (item.Key != ' ' && item.Value == 1) //если символ записан в словарь со значением 1, и это не пробел, тогда выводим эти символы через пробле
                {
                    Console.Write(item.Key + " ");
                }
            }

        }
        static void ThroughString(string text)//метод обработки c помощью методов класса string
        {
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != ' ' && text.IndexOf(text[i]) == text.LastIndexOf(text[i]))//indexOf проверяет индекс символа в строке первый раз, а
                                                                       //LastIndexOf проверяет индекс последующего появления символа в строке
                                                                       //и если эти индексы совпадут - тогда символ уникальный. иначе - нет.
                    Console.Write(text[i] + " ");//вывод таких уникальных символов если условие выше соблюдено
            }
        }
    }
}
