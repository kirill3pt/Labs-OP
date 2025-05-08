using System;
using System.Collections.Generic;

class Zadanie7
{
    static void Main()
    {
        // Ввод первого списка
        Console.WriteLine("Введите элементы первого списка, разделенные пробелом:");
        string input1 = Console.ReadLine();
        List<int> list1 = ConvertToList(input1);

        // Ввод второго списка
        Console.WriteLine("Введите элементы второго списка, разделенные пробелом:");
        string input2 = Console.ReadLine();
        List<int> list2 = ConvertToList(input2);

        // Проверка на равенство
        bool equal = Check(list1, list2);

        if (equal)
        {
            Console.WriteLine("Списки равны");
        }
        else
        {
            Console.WriteLine("Списки не равны");
        }

        Console.ReadLine();
    }

    static List<int> ConvertToList(string input)
    {
        string[] elements = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        List<int> list = new List<int>();

        foreach (var element in elements)
        {
            if (int.TryParse(element, out int num))
            {
                list.Add(num);
            }
            else
            {
                Console.WriteLine($"Некорректное значение: {element}. Пропускаем.");
            }
        }

        return list;
    }

    static bool Check(List<int> a, List<int> b)
    {
        if (a.Count != b.Count)
        {
            return false;
        }

        var dictA = new Dictionary<int, int>();
        var dictB = new Dictionary<int, int>();

        // считаем количество каждого элемента
        foreach (int num in a)
        {
            if (dictA.ContainsKey(num))
            {
                dictA[num]++;
            }
            else
            {
                dictA[num] = 1;
            }
        }

        foreach (int num in b)
        {
            if (dictB.ContainsKey(num))
            {
                dictB[num]++;
            }
            else
            {
                dictB[num] = 1;
            }
        }

        // сравниваем словари
        foreach (var pair in dictA)
        {
            if (!dictB.ContainsKey(pair.Key) || dictB[pair.Key] != pair.Value)
            {
                return false;
            }
        }

        return true;
    }
}
