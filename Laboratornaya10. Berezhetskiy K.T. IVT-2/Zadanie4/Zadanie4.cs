using System;

namespace Zadanie4
{
    class Zadanie4
    {
        static void Main()
        {
            int[,] A = 
            {
                //0   1    2    3    4    5
                { 0,  50,  -1,  100, -1,  -1 }, //0
                { 50, 0,   60,  -1,  80,  -1 }, //1
                { -1, 60,  0,   70,  -1,  90 }, //2
                { 100,-1,  70,  0,   -1,  40 }, //3
                { -1, 80,  -1,  -1,  0,   30 }, //4
                { -1, -1,  90,  40,  30,  0  }  //5
            };

            int startCity = 0; // из какого города считаем
            int maxDistance = 200;

            int[] dist = algorithm(A, startCity);

            Console.WriteLine($"Города, достижимые из города {startCity} не дальше {maxDistance} км:");

            for (int i = 0; i < dist.Length; i++)
            {
                if (dist[i] <= maxDistance)
                {
                    Console.WriteLine($"Город {i} — расстояние {dist[i]} км");
                }
            }
            Console.ReadLine();
        }
        static int[] algorithm(int[,] A, int s)
        {
            int n = A.GetLength(0);
            int[] dist = new int[n];
            bool[] visited = new bool[n];

            // инициализация массива расстояний
            for (int i = 0; i < n; i++)
                dist[i] = int.MaxValue;

            dist[s] = 0;

            for (int i = 0; i < n; i++)
            {
                // выбираем непосещённый город с минимальным расстоянием
                int u = -1;
                int minDist = int.MaxValue;

                for (int j = 0; j < n; j++)
                {
                    if (!visited[j] && dist[j] < minDist)
                    {
                        minDist = dist[j];
                        u = j;
                    }
                }

                if (u == -1) break; // все достижимые обработаны

                visited[u] = true;

                // обновляем расстояния до соседей
                for (int v = 0; v < n; v++)
                {
                    if (A[u, v] != -1 && !visited[v])
                    {
                        int newDist = dist[u] + A[u, v];
                        if (newDist < dist[v])
                            dist[v] = newDist;
                    }
                }
            }

            return dist;
        }
    }
}
