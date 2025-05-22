using System;
using System.Collections.Generic;

namespace Zadanie3
{
    class Zadanie3
    {
        static Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>()
    {
        {1, new List<int>{2,3}},
        {2, new List<int>{1,7,6}},
        {3, new List<int>{1,4,6,8}},
        {4, new List<int>{3,5}},
        {5, new List<int>{4,6}},
        {6, new List<int>{2,3,5}},
        {7, new List<int>{2,8}},
        {8, new List<int>{3,7}}
    };

        static void Main()
        {
            Console.Write("Введите вершину X: ");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Введите вершину Y: ");
            int y = int.Parse(Console.ReadLine());

            Console.WriteLine("\nDFS:");
            var visited = new HashSet<int>();
            var path = new List<int>();
            if (DFS(x, y, visited, path))
                Console.WriteLine(string.Join(" -> ", path.ToArray()));
            else
                Console.WriteLine("Путь не найден");

            Console.WriteLine("\nBFS:");
            BFS(x, y);
            Console.ReadLine();
        }

        static bool DFS(int current, int target, HashSet<int> visited, List<int> path)
        {
            visited.Add(current);
            path.Add(current);
            if (current == target)
                return true;

            foreach (int neighbor in adj[current])
            {
                if (!visited.Contains(neighbor))
                {
                    if (DFS(neighbor, target, visited, path))
                        return true;
                }
            }

            path.RemoveAt(path.Count - 1);
            return false;
        }

        static void BFS(int start, int end)
        {
            Queue<List<int>> queue = new Queue<List<int>>();
            List<int> init = new List<int>();
            init.Add(start);
            queue.Enqueue(init);

            while (queue.Count > 0)
            {
                List<int> path = queue.Dequeue();
                int last = path[path.Count - 1];

                if (last == end)
                {
                    Console.WriteLine(string.Join(" -> ", path.ToArray()));
                    return;
                }

                foreach (int neighbor in adj[last])
                {
                    if (!path.Contains(neighbor))
                    {
                        List<int> newPath = new List<int>(path);
                        newPath.Add(neighbor);
                        queue.Enqueue(newPath);
                    }
                }
            }

            Console.WriteLine("Путь не найден");
        }
    }
}
