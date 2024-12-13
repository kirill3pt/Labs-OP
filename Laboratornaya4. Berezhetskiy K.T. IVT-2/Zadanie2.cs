using System;

namespace Zadanie2
{
    internal class Zadanie2
    {
        static void Main()
        {
            Console.WriteLine("Введите предложение, с точкой в конце.");
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
            predlozhenie = predlozhenie.TrimEnd('.'); //убираем точку, чтобы обрабатывалось всё корректно
            Console.WriteLine("\nЧерез обработку массива символов:");
            Console.WriteLine(ThroughArray(predlozhenie) + '.');
            Console.WriteLine("\nЧерез методы string:");
            Console.WriteLine(ThroughString(predlozhenie) + ".");
            Console.ReadLine();
        }

        static string ThroughArray(string text)
        {
            char[] massivSimvolov = text.ToCharArray();
            string result = "";
            string word = "";
            int counter = 0;

            foreach (char c in massivSimvolov)
            {
                if (char.IsLetterOrDigit(c)) //если символ является буквой или цифрой, входит в слово
                {
                    word += c;//тогда добавляем символ к этому слову
                }
                else if (!string.IsNullOrEmpty(word))//тут проверяем, закончилось ли слово или нет
                {
                    counter++;//увеличиваем значение в счётчике
                    result += word + $"({counter})" + c;//добавляем в новую строку слово с номером этого слова, взятого из счётчика,
                                                        //и разделитель, который идет после этого слова
                    word = "";//обнуляем это слово для записи нового слова
                }
                else
                {
                    result += c; //это для разделителя, который не является частью слова
                }
            }

            if (!string.IsNullOrEmpty(word)) //это нужно если осталось слово после выполнения цикла, и оно не было добавлено в результат,
                                             //просто добавляем оставшееся слово в новую строку
            {
                counter++;
                result += word + $"({counter})";
            }
            return result;
        }//метод обработки строки как массив

        static string ThroughString(string text)
        {
            string result = "";
            string[] words = text.Split( ' ', '-', ','); //разделяем слова, тут либо пробел, либо запятая, либо тире.
                                                         //если встречается что-то из этого, значит слово закончилось.
            int counter = 1;//счетчик для количества слов
            int currentIndex = 0;//этот счётчик нужен для сохранения разделителей, чтобы потом мы их вернули в новое предложение.
                                 //т.к. из-за Split они теряются.
            foreach (string word in words)
            {
                if (!string.IsNullOrEmpty(word))//тут проверяем, закончилось ли слово или нет
                {
                    result += word + $"({counter})"; //прибавляем в новое предложение слово и то, что посчитает счётчик.
                    counter++;
                    currentIndex += word.Length;
                }
                //тут замудрено, но по другому не получилось...
                //т.е. пока текущий символ не является буквой или цифрой,
                //то мы добавляем этот символ в новое предложение(как раз добавится разделитель)
                while (currentIndex < text.Length && !char.IsLetterOrDigit(text[currentIndex]))
                {
                    result += text[currentIndex];//добавляем тот самый разделитель, который теряется при делении строки
                    currentIndex++;//переход к следующему символу
                }
            }
            return result;
        }//метод обработки c помощью методов класса string
    }
}