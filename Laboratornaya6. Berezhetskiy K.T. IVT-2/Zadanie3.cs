using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Zadanie3
{
    internal class Zadanie3
    {
        static string inputFile = "E:\\BACKUP\\Учёба\\Berezhetskiy K.T. IVT-2 LAB5\\Zadanie3\\bin\\Debug\\zadanie3.txt";
        static string outputFile = "zadanie31.txt";
        static void Main()
        {
            Write();
            Console.ReadLine();
        }
        static void Write()
        {
            if (!File.Exists(inputFile))
            {
                Console.WriteLine("Файл не найден.");
                return;
            }
            int count = 0;
            using (StreamReader reader = new StreamReader(inputFile))
            {
                using (StreamWriter writer = new StreamWriter(outputFile, false))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Any(char.IsDigit))
                        {
                            writer.WriteLine(line);
                        }
                        else
                        {
                            count++;
                        }
                    }
                    Console.WriteLine($"Результат записан в {outputFile}");
                    Console.WriteLine($"Строк удалено: {count}");
                }
            }
        }
    }
}
