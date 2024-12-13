using System;
using System.Text.RegularExpressions;

namespace Zadanie5
{
    internal class Zadanie5
    {
        static void Main()
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();
            Console.WriteLine("\nЧерез обработку массива символов:");
            ThroughArrays(text);
            Console.WriteLine("\nВывод через регулярные выражения:");
            ThroughRegex(text);
            Console.ReadLine();
        }

        static void ThroughArrays(string text)
        {
            //преобразуем строку в массив символов
            char[] chars = text.ToCharArray();
            string word = "";  //переменная для хранения текущего слова

            for (int i = 0; i < chars.Length; i++)
            {
                if (char.IsLetterOrDigit(chars[i]))  //если символ - буква или цифра
                {
                    word += chars[i];  //добавляем символ к текущему слову
                }
                else
                {
                    //если слово начинается с большой буквы и заканчивается на 2 цифры, выводим его
                    if (word.Length > 2 && char.IsUpper(word[0]) && char.IsDigit(word[word.Length - 1]) && char.IsDigit(word[word.Length - 2]))
                    {
                        Console.WriteLine(word);
                    }
                    word = "";//сбрасываем текущее слово
                }
            }

            //это для последнего слова, т.к. оно может отсеиться, ибо не заканчивается на какой-либо символ.
            if (word.Length > 2 && char.IsUpper(word[0]) && char.IsDigit(word[word.Length - 1]) && char.IsDigit(word[word.Length - 2]))
            {
                Console.WriteLine(word);
            }
        }
        static void ThroughRegex(string text)
        {
            Regex regex = new Regex(@"\b([A-Za-zА-Яа-яЁё])[a-zA-Zа-яА-ЯЁё]*\d{2}\b");//тут задаем регулярное выражение, которое соответствует условию:
                                                                                     //1 буква заглавная, последующие любые, последних 2 символа - числа.
            MatchCollection matches = regex.Matches(text); //создаем коллекцию для поиска совпадений по заданному регуляронму выражению
            foreach (Match match in matches)
            {
                Console.WriteLine(match.Value);//выводим данные совпадения
            }
        }
    }
}
