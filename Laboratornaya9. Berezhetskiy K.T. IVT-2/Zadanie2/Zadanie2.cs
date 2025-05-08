using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите математическое выражение для проверки на корректность скобок:");
        string expression = Console.ReadLine();  // Считываем ввод пользователя

        // проверяем корректность выражения
        if (Check(expression))
        {
            Console.WriteLine("Выражение корректно.");
        }
        else
        {
            Console.WriteLine("Выражение некорректно.");
        }
        Console.ReadLine();
    }
    // метод для проверки корректности скобок
    static bool Check(string expression)
    {
        Stack<char> stack = new Stack<char>();  // стек для хранения открывающих скобок

        foreach (char c in expression)
        {
            if (c == '(')  // если открывающая скобка, добавляем в стек
            {
                stack.Push(c);
            }
            else if (c == ')')  // если закрывающая скобка
            {
                if (stack.Count == 0)  // если стек пуст, то скобки не сбалансированы
                {
                    return false;
                }
                stack.Pop();  // убираем последнюю открывающую скобку из стека
            }
        }

        // если стек пуст, то все скобки сбалансированы
        return stack.Count == 0;
    }
}