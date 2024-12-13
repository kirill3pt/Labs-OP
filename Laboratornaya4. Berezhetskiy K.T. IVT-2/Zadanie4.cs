using System;
using System.Text.RegularExpressions;

class Zadanie4
{
    static void Main()
    {
        //тут инициализируем массив и заполняем его строками
        string[] lines = new string[7];
        Console.WriteLine("Введите 7 строк:");
        for (int i = 0; i < 7; i++)
        {
            lines[i] = Console.ReadLine();
        }

        Console.WriteLine("\nЧерез обработку массива символов:");
        FindProbelAndComTHROUGHArray(lines);
        Console.WriteLine("\nЧерез методы string:");
        FindProbelAndComTHROUGHString(lines);
        Console.ReadLine();
    }

    static void FindProbelAndComTHROUGHArray(string[] lines)
    {
        int minSpaces = int.MaxValue;
        int lineIndex = -1;
        //это были переменные для поиска минимального количества пробелов
        for (int i = 0; i < lines.Length; i++) 
        {
            char[] chars = lines[i].ToCharArray(); //разбиваем массив слов на циклы
            string word = "";//значение для слов
            bool tochkacom = false;//будем проверять, нашли ли мы это слово

            //тут проходим по символам строки и собираем слово
            for (int j = 0; j < chars.Length; j++)
            {
                if (char.IsLetterOrDigit(chars[j]) || chars[j] == '.')
                {
                    word += chars[j];//собираем слово
                }
                else
                {
                    //если слово заканчивается на ".com", выводим строку
                    if (word.EndsWith(".com", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Строка {i + 1}: {lines[i]}");
                        tochkacom = true;
                        break; //прерываем цикл, так как слово найдено
                    }
                    word = ""; //сбрасываем слово
                }
            }

            //это для проверки последнего слова в строке
            if (!tochkacom && word.EndsWith(".com", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Строка {i + 1}: {lines[i]}");
            }
        }

        //ЦИКЛ ДЛЯ ПОИСКА МИНИМАЛЬНОГО КОЛИЧЕСТВА ПРОБЕЛОВ
        for (int i = 0; i < lines.Length; i++)
        {
            int spaceCount = 0;
            // Подсчитываем количество пробелов в строке
            foreach (char c in lines[i])
            {
                if (c == ' ') spaceCount++;//если нашли символ пробела, то обновляем счетчик
            }

            //если количество пробелов в текущей строке меньше, обновляем минимальное значение
            if (spaceCount < minSpaces)
            {
                minSpaces = spaceCount;
                lineIndex = i;//запоминаем номер строки
            }
        }
        Console.WriteLine($"Номер строки с наименьшим количеством пробелов: {lineIndex}");
        //ЦИКЛ ДЛЯ ПОИСКА МИНИМАЛЬНОГО КОЛИЧЕСТВА ПРОБЕЛОВ
    }
    static void FindProbelAndComTHROUGHString(string[] lines)
    {
        
        int minSpaceIndex = -1;
        int minSpaceCount = int.MaxValue;
        //это были переменные для поиска минимального количества пробелов
        for (int i = 0; i < lines.Length; i++)//цикл для обработки всех строк
        {
            string line = lines[i];

            if (Regex.IsMatch(line, @"\b\w+\.com\b", RegexOptions.IgnoreCase))//регулярное выражение, которое находит, есть ли .com в каком-либо слове
            {
                Console.WriteLine($"Строка {i + 1}: {line}");//если нашло, то выводит номер строки и содержимое
            }
            
            int spaceCount = line.Split(' ').Length - 1;//для подсчёта количества пробелов разбиваем строку с пробелом в качестве разделителя

            if (spaceCount < minSpaceCount) //если счётчик пробелов меньше минимального, то обновляем значение
            {
                minSpaceCount = spaceCount;
                minSpaceIndex = i;//запоминаем индекс строки, где пробелов минимальное кол-во
            }
        }
        Console.WriteLine($"Номер строки с наименьшим количеством пробелов: {minSpaceIndex}");
    }
}
