using System;
using System.IO;

namespace Zadanie2
{
    internal class Zadanie2
    {
        static string filePath1 = "zadanie2.dat"; //путь для 1 файла
        static string filePath2 = "zadanie21.dat"; //путь для 2 файла
        static void Main()
        {
            int[] numbers = Progression(); //вызываем функцию, которая создает прогрессию по заданному условию
            Write(numbers); //записываем данную прогрессию в файл
            int[] loadNumbers = Read(); //открываем этот файл, в который записали прогрессию
            WriteSecond(loadNumbers[4], loadNumbers[5]); //записываем данные, полученные при чтении первого файла во второй 
            Console.ReadLine();
        }
        static int[] Progression() //функция для создания прогрессии. все элементы прогрессии хранятся в массиве, т.к. удобно позже получать к ним доступ
        {
            int[] numbers = new int[6]; //создаем новый массив
            numbers[0] = 4; //первый элемент - 4, по условию
            int step = 7; //шаг также, 7, по условию

            Console.WriteLine("Арифмитическая прогрессия с первым членом 4 и шагом 7: ");
            for (int i = 0; i < numbers.Length; i++) //цикл для заполнения прогрессии.
            {
                if (i > 0) //пропускаем первый элемент
                {
                    numbers[i] = numbers[i - 1] + step; //прибавляем к предыдущему элементу шаг 7
                }
                Console.Write(numbers[i] + " | ");
            }
            return numbers; 
        }
        static void Write(int[] numbers) //функция для записи массива чисел в бинарный файл
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath1, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                foreach (int number in numbers) //цикл для записи чисел
                {
                    writer.Write(number);
                }
            }
            Console.WriteLine("\nДанные успешно записаны в файл.");
        }
        static int[] Read() //функция для прочтения массива чисел из файла
        {
            using (BinaryReader reader = new BinaryReader(File.Open(filePath1, FileMode.Open)))
            {
                int length = (int)(reader.BaseStream.Length / sizeof(int)); //берем делим длину в битах на размерность типа int. выходит 6*4 = 24 / 4 = 6 элементов
                int[] numbers = new int[length]; //создаем новый массив для записи в него элементов

                for (int i = 0; i < length; i++) 
                {
                    numbers[i] = reader.ReadInt32(); //считываем каждый элемент массива
                }
                Console.WriteLine($"5-й элемент: {numbers[4]}");
                Console.WriteLine($"6-й элемент: {numbers[5]}"); //выводим нужные элементы: 5 и 6
                return numbers;
            }
        }
        static void WriteSecond(int fifthElement, int sixthElement) //функция для записи во второй файл
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath2, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                writer.Write(fifthElement); //для передачи 5 элемента прогрессии
                writer.Write(sixthElement); //для передачи 6 элемента прогрессии
            }
            Console.WriteLine($"Данные успешно записаны во второй файл: {Path.GetFullPath(filePath2)}");
        }
    }
}
