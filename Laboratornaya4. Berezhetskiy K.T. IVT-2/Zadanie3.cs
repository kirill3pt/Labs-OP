using System;
using System.Text;

namespace Zadanie3
{
    internal class Zadanie3
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
            Console.WriteLine(ThroughArray(predlozhenie));
            Console.WriteLine("\nЧерез методы string:");
            Console.WriteLine(ThroughStringBuilder(predlozhenie));
            Console.ReadLine();
        }

        static string ThroughArray(string text)
        {
            //описываем переменные, массивы и т.д.
            char[] massivSimvolov = text.ToCharArray();
            string word = "";
            string result = "";
            string[] words = new string[text.Length];
            int wordIndex = 0;
            //описываем переменные, массивы и т.д.

            for (int i = 0; i < massivSimvolov.Length; i++) //цикл для сбора символов в слова
            {
                if (massivSimvolov[i] != ' ') //если элемент не является пробелом, тогда ---
                {
                    word += massivSimvolov[i];//---> записываем этот элемент в слово
                }
                else if (!string.IsNullOrEmpty(word)) //если встретили пробел, то добавляем это слово в массив
                {
                    words[wordIndex++] = word; 
                    word = ""; //обнуляем текущее слово
                }

            }

            if (!string.IsNullOrEmpty(word)) //это снова для последнего слова, которе не было добавлено ранее. если встретили пробел,
                                             //то добавляем это слово в массив.
            {
                words[wordIndex++] = word;
            }

            for (int i = wordIndex - 1; i >= 0; i--) //переворачиваем эти слова, идем по массиву слов в обратном порядке
            {
                result += words[i]; //записываем в результат слово
                if (i > 0) result += " "; //добавляем пробел между словами
            }
            return result += '.';
        }//метод обработки строки как массив

        static string ThroughStringBuilder(string text)
        {
            string[] words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//тут мы разделили строку на слова (разделили по пробелу)
            Array.Reverse(words);//перевернули этот массив с помощью Array.Reverse
            //создали новую строку, в которую добавили слова, полученные с помощью split и array.reverse
            StringBuilder result = new StringBuilder();
            result.Append(string.Join(" ", words));
            result.Append(".");//это просто добавляем точку.
            //создали новую строку, в которую добавили слова, полученные с помощью split и array.reverse
            return result.ToString();
        }//метод обработки c помощью методов класса string
    }
}
