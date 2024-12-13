using System;
using System.Text.RegularExpressions;

namespace Zadanie6
{
    internal class Zadanie6
    {
        static void Main()
        {
            Console.WriteLine("Введите строку вида {x + y = z}: ");
            string text = Console.ReadLine();
            
            Regex regex = new Regex(@"\s*(-?\d+)\s*\+\s*(-?\d+)\s*=\s*(-?\d+)\s*");//тут создаем регулярное выражение, где:
                                                                                   //между операндами может быть произвольное кол-во пробелов
                                                                                   //числа, которые могут быть положительными или отрицательными
                                                                                   //обозначаем символы, между которыми могут быть пробелы
            Match match = regex.Match(text);

            if (match.Success)
            {
                int op1 = int.Parse(match.Groups[1].Value); //получаем значение 1 операнда
                int op2 = int.Parse(match.Groups[2].Value); //получаем значение 2 операнда
                int sum = int.Parse(match.Groups[3].Value); //получаем значение суммы

                Console.WriteLine($"Первое число: {op1}"); //тут просто идет вывод
                Console.WriteLine($"Второе число: {op2}");
                Console.WriteLine($"Сумма: {sum}");
                if (sum == op1 + op2)
                {
                    Console.WriteLine("Сумма верна.");
                }
                else
                {
                    Console.WriteLine("Сумма неправильная");
                }
            }
            else //это случай, если вдруг строка была введена не верно.
            {
                Console.WriteLine("Строка не соответствует op1 + op2 = res. Нажмите 'Enter' для выхода");
                Console.ReadLine();
                return;
            }
            Console.ReadLine();
        }
    }
}
