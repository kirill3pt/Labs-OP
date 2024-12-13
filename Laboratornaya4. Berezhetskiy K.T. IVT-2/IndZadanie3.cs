using System;
using System.Text.RegularExpressions;

namespace IndZadanie3
{
    internal class IndZadanie3
    {
        static void Main()
        {
            Console.WriteLine("Введите строку для поиска подстроки времени формата '23:15:59");
            string text = Console.ReadLine();

            Regex regex = new Regex(@"\b(\d{2}):(\d{2}):(\d{2})\b");//тут используем регулярное выражение по шаблону:
            //граница слова - 2 cимвола десятичной цифры:2 cимвола десятичной цифры:2 cимвола десятичной цифры - граница слова
            //тут : выступает в качестве разделителя
            MatchCollection matches = regex.Matches(text);

            Console.WriteLine("Найденные подстроки времени: ");
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);//выводим для каждого совпадение значение
            }
            Console.WriteLine($"Всего найдено: {matches.Count} ");//тут просто выводим общее количество
            Console.ReadLine();
        }
    }
}
