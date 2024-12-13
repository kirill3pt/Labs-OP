using System;
using System.Linq;
using System.Text;

namespace IndZadanie2
{
    class IndZadanie2
    {
        static void Main()
        {
            Console.WriteLine("Введите текст:");
            string text = Console.ReadLine();

            //ключевые слова в C#
            string[] keywords = new string[]
            {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class",
            "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event",
            "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if",
            "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null",
            "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly",
            "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct",
            "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
            "ushort", "using", "virtual", "void", "volatile", "while", "+", "-", "=", "!", ".", "?", "&", "%"
            };

            string resultByArray = ThroughArray(text, keywords);
            Console.WriteLine("\nРезультат (через массив символов):");
            Console.WriteLine(resultByArray);
            string resultByStringBuilder = ThroughString(text, keywords);
            Console.WriteLine("\nРезультат (через String/StringBuilder):");
            Console.WriteLine(resultByStringBuilder);

            Console.ReadLine();
        }
        static string ThroughArray(string text, string[] keywords)
        {
            StringBuilder result = new StringBuilder();//для хранения результата
            StringBuilder currentWord = new StringBuilder();//для сборки слова

            char[] chars = text.ToCharArray();

            foreach (char c in chars)
            {
                if (char.IsWhiteSpace(c) || char.IsPunctuation(c))//если символ разделитель или пробел
                {
                    if (currentWord.Length > 0)//если уже есть слово
                    {
                        string word = currentWord.ToString();
                        if (!keywords.Contains(word))//Проверяем, не находится ли слово в keywords
                        {
                            result.Append(word).Append(' ');//Добавляем слово в результат
                        }
                        currentWord.Clear();//освобождаем место для следующего слова
                    }
                }
                else
                {
                    currentWord.Append(c);//добавляем символ к текущему слову
                }
            }

            //обрабатываем последнее слово (если строка не оканчивается разделителем)
            if (currentWord.Length > 0)
            {
                string word = currentWord.ToString();
                if (!keywords.Contains(word))
                {
                    result.Append(word).Append(' ');
                }
            }

            return result.ToString().Trim();
        }
        static string ThroughString(string text, string[] keywords)
        {
            StringBuilder result = new StringBuilder();
            string[] words = text.Split(new char[] { ' ', ',', '-', ';', ':', '!', '?', '.' }, StringSplitOptions.RemoveEmptyEntries);
            //делим текст на слова по разделителям, всем возможным
            foreach (string word in words)//для каждого слова в новом массиве
            {
                if (!keywords.Contains(word))//если слово не находится в массиве запрещенных слов, то тогда выводим слова через пробел оставшиеся
                {
                    result.Append(word + " ");
                }

            }
            return result.ToString().Trim();
        }
    }
}