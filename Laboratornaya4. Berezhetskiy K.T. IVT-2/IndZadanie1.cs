using System;
using System.Collections.Generic;
using System.Text;

namespace IndZadanie1
{
    internal class IndZadanie1
    {
        static void Main()
        {
            Console.WriteLine("Выберите метод шифрования:");
            Console.WriteLine("1. Шифр Полибия");
            Console.WriteLine("2. Шифр Гронсфельда");
            Console.WriteLine("3. Книжный шифр");
            Console.Write("ВЫБОР: ");
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите текст:");
            string input = Console.ReadLine();

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Зашифрованный текст:");
                    string encrypted = PolybiusCipher.EncryptDecrypt(input, true);
                    Console.WriteLine(encrypted);
                    Console.WriteLine("Расшифрованный текст:");
                    Console.WriteLine(PolybiusCipher.EncryptDecrypt(encrypted, false));
                    break;

                case 2:
                    Console.WriteLine("Введите ключ для шифра Гронсфельда:");
                    string key = Console.ReadLine();
                    Console.WriteLine("Зашифрованный текст:");
                    string encText = GronsfeldCipher.Process(input, key, true);
                    Console.WriteLine(encText);
                    Console.WriteLine("Расшифрованный текст:");
                    Console.WriteLine(GronsfeldCipher.Process(encText, key, false));
                    break;

                case 3:
                    Console.WriteLine("Введите ключ-книгу:");
                    string bookKey = Console.ReadLine();
                    Console.WriteLine("Зашифрованный текст:");
                    string bookEncrypted = BookCipher.Process(input, bookKey, true);
                    Console.WriteLine(bookEncrypted);
                    Console.WriteLine("Расшифрованный текст:");
                    Console.WriteLine(BookCipher.Process(bookEncrypted, bookKey, false));
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
            Console.ReadLine();
        }
    }
    //ШИФР ПОЛИБИЯ
    static class PolybiusCipher
    {
        //таблица шифрования для шифра Полибия (матрица 5x5)
        static char[,] square =
        {
        { 'A', 'B', 'C', 'D', 'E' },
        { 'F', 'G', 'H', 'I', 'K' },
        { 'L', 'M', 'N', 'O', 'P' },
        { 'Q', 'R', 'S', 'T', 'U' },
        { 'V', 'W', 'X', 'Y', 'Z' }
    };

        //метод шифрования и дешифрования
        public static string EncryptDecrypt(string text, bool encrypt)
        {
            if (string.IsNullOrEmpty(text)) return ""; //если текст пустой, возвращаем пустую строку

            text = text.ToUpper().Replace("J", "I"); //заменяем 'J' на 'I', т.к. в таблице Полибия нет 'J'
            StringBuilder result = new StringBuilder(); //храним результат
            Dictionary<char, (int, int)> charMap = new Dictionary<char, (int, int)>(); //словарь для хранения позиций букв

            //заполнение словаря с позициями символов в таблице (r - строка, c - столбец)
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    charMap[square[r, c]] = (r + 1, c + 1); // (r + 1, c + 1) - индекс с единицы

            if (encrypt) // Шифрование
            {
                foreach (char ch in text)
                {
                    if (charMap.ContainsKey(ch)) //если символ есть в словаре
                    {
                        var pos = charMap[ch]; //получаем позицию символа
                        result.Append(pos.Item1).Append(pos.Item2); //добавляем к результату две цифры (строка и столбец)
                    }
                    else
                        result.Append(ch); //если символ не найден, добавляем его как есть
                }
            }
            else //дешифрование
            {
                for (int i = 0; i < text.Length; i += 2) //проходим через текст, обрабатывая пары цифр
                {
                    if (i + 1 < text.Length && char.IsDigit(text[i]) && char.IsDigit(text[i + 1])) //если оба символа цифры
                    {
                        int row = text[i] - '0' - 1; //переводим строку из цифры в индекс (с нуля)
                        int col = text[i + 1] - '0' - 1; //переводим столбец из цифры в индекс (с нуля)
                        result.Append(square[row, col]); //добавляем символ из таблицы на основе индексов
                    }
                    else
                    {
                        result.Append(text[i]); //если это не пара цифр, добавляем символ как есть
                    }
                }
            }
            return result.ToString();
        }
    }
    //ШИФР ГРОНСФЕЛЬДА
    static class GronsfeldCipher
    {
        //метод для шифрования и дешифрования с использованием шифра Гронсфельда
        public static string Process(string input, string key, bool encrypt)
        {
            StringBuilder result = new StringBuilder();//строка для хранения результата
            for (int i = 0; i < input.Length; i++)//проходим через все символы входного текста
            {
                if (char.IsLetter(input[i]))//если символ - буква
                {
                    char baseChar = char.IsUpper(input[i]) ? 'A' : 'a';//определяем базовый символ для верхнего или нижнего регистра
                    int shift = key[i % key.Length] - '0';//считываем сдвиг из ключа, который цикличен по длине ключа
                    if (!encrypt) shift = -shift;//если дешифруем, сдвиг инвертируется
                    result.Append((char)((input[i] - baseChar + shift + 26) % 26 + baseChar));//применяем сдвиг и добавляем символ в результат
                }
                else
                {
                    result.Append(input[i]);//если это не буква, просто добавляем символ как есть
                }
            }
            return result.ToString();
        }
    }
    //КНИЖНЫЙ ШИФР
    static class BookCipher
    {
        // Метод для шифрования и дешифрования с использованием шифра Книга
        public static string Process(string input, string key, bool encrypt)
        {
            StringBuilder result = new StringBuilder();//храним результат
            if (encrypt)//шифрование
            {
                for (int i = 0; i < input.Length; i++)//проходим по всем символам входного текста
                {
                    if (i < key.Length)//если текущий индекс меньше длины ключа
                    {
                        int value = input[i] + key[i];//складываем значения символа и соответствующего символа ключа
                        result.Append(value.ToString("X"));//добавляем результат в шестнадцатеричном формате
                    }
                }
            }
            else//дешифрование
            {
                for (int i = 0; i < input.Length; i += 2)//проходим по строке, обрабатывая по два символа (шестнадцатеричные значения)
                {
                    if (i / 2 < key.Length)//еесли индекс половины строки меньше длины ключа
                    {
                        int value = Convert.ToInt32(input.Substring(i, 2), 16) - key[i / 2];//преобразуем из шестнадцатеричного в число и вычитаем значение из ключа
                        result.Append((char)value);//добавляем результат в строку
                    }
                }
            }
            return result.ToString();
        }
    }
}