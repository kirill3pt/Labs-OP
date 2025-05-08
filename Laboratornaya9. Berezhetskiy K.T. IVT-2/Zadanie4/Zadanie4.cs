using System;
using System.Collections.Generic;
using System.IO;

namespace Zadanie5
{
    class Zadanie4
    {
        static void Main()
        {
            const int maxN = 50000;
            const int maxCube = 100; // ограничим кубы, чтобы не выходили далеко за 50000
            var combinations = new Dictionary<int, int>();

            for (int x = 0; x <= maxCube; x++)
            {
                int x3 = x * x * x;
                for (int y = 0; y <= maxCube; y++)
                {
                    int y3 = y * y * y;
                    for (int z = 0; z <= maxCube; z++)
                    {
                        int z3 = z * z * z;
                        int sum = x3 + y3 + z3;

                        if (sum > maxN)
                            continue;

                        if (combinations.ContainsKey(sum))
                            combinations[sum]++;
                        else
                            combinations[sum] = 1;
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter("результаты.txt"))
            {
                foreach (var pair in combinations)
                {
                    if (pair.Value >= 3)
                    {
                        writer.WriteLine($"N = {pair.Key}, комбинаций: {pair.Value}");
                    }
                }
            }
            Console.WriteLine("Результаты сохранены в файл: результаты.txt");
            Console.ReadLine();

        }
    }
}