using System;
using System.Collections.Generic;

namespace TournamentTree
{
    class Zadanie6
    {
        // класс узла дерева, хранит команды, счёт и потомков
        class Node
        {
            public string Team1;
            public string Team2;
            public int Score1;
            public int Score2;
            public Node Left;
            public Node Right;
        }

        // рандом нужен для генерации счёта
        static Random rnd = new Random();

        static void Main()
        {
            // список команд, 16 штук
            string[] teams = {
                "BRA", "ARG", "FRA", "COL", "CHI", "URU", "GER", "NIG",
                "CRC", "MEX", "NED", "GRE", "BEL", "SWI", "USA", "ALG"
            };

            // очередь для построения дерева матчей
            Queue<Node> queue = new Queue<Node>();

            // 1/16 финала — пары команд играют друг с другом
            for (int i = 0; i < teams.Length; i += 2)
            {
                Node node = new Node();
                node.Team1 = teams[i];
                node.Team2 = teams[i + 1];
                PlayMatch(node);        // играем матч
                queue.Enqueue(node);    // добавляем результат в очередь
            }

            // сборка турнирного дерева из победителей
            while (queue.Count > 1)
            {
                Node left = queue.Dequeue();   // берём левый матч
                Node right = queue.Dequeue();  // берём правый матч

                Node parent = new Node();      // создаём родительский матч
                parent.Left = left;            // привязываем левый матч
                parent.Right = right;          // привязываем правый матч
                parent.Team1 = GetWinner(left);    // первая команда — победитель из левого
                parent.Team2 = GetWinner(right);   // вторая — из правого

                PlayMatch(parent);         // играем новый матч
                queue.Enqueue(parent);     // добавляем в очередь
            }

            // финальный матч — корень дерева
            Node root = queue.Dequeue();

            Console.WriteLine("Результаты турнира:\n");
            PrintTree(root, 0);    // выводим дерево
            Console.ReadLine();    // ждём, чтобы окно не закрылось сразу
        }

        // функция, играющая матч — генерирует счёт
        static void PlayMatch(Node match)
        {
            match.Score1 = rnd.Next(0, 5);  // случайный счёт от 0 до 4
            match.Score2 = rnd.Next(0, 5);

            while (match.Score1 == match.Score2)  // ничья не допускается
            {
                match.Score1 = rnd.Next(0, 5);
                match.Score2 = rnd.Next(0, 5);
            }
        }

        // возвращает победителя матча
        static string GetWinner(Node match)
        {
            return match.Score1 > match.Score2 ? match.Team1 : match.Team2;
        }

        // рекурсивный вывод дерева турнира
        static void PrintTree(Node node, int level)
        {
            if (node == null)
                return;

            string indent = new string(' ', level * 4); // отступ зависит от уровня
            Console.WriteLine($"{indent}{node.Team1} - {node.Team2} : {node.Score1} - {node.Score2}");
            PrintTree(node.Left, level + 1);  // выводим левую ветку
            PrintTree(node.Right, level + 1); // потом правую
        }
    }
}
