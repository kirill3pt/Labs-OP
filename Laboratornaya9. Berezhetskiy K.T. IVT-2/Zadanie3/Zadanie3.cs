using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Zadanie3
{
    // Структура для представления участника игры
    class Player
    {
        public string Name { get; set; }
        public Player Next { get; set; } // Следующий участник в списке
    }
    static void Main()
    {
        // Загружаем имена игроков из файла
        Console.Write("Введите путь к файлу с именами игроков: ");
        string filePath = Console.ReadLine();
        var players = load(filePath);

        if (players.Count < 5 || players.Count > 10)
        {
            Console.WriteLine("Количество игроков должно быть от 5 до 10.");
            return;
        }

        // Создаем циклический связный список
        Player playersList = circularList(players);

        // Запрос на ввод начала игры и считалки
        Console.Write("Введите считалку: ");
        string countingString = Console.ReadLine();

        Console.WriteLine("Доступные имена в данном файле: ");
        foreach (var line in load(filePath))
        {
            Console.WriteLine(line.Trim());
        }
        Console.Write("Введите имя игрока, с которого начинаем: ");
        string startPlayerName = Console.ReadLine();

        // Ищем игрока, с которого начинаем
        Player startPlayer = playersList;
        while (startPlayer != null && startPlayer.Name != startPlayerName)
        {
            startPlayer = startPlayer.Next;
            if (startPlayer == playersList)
            {
                break;
            };
        }

        if (startPlayer == null)
        {
            Console.WriteLine("Игрок с таким именем не найден.");
            return;
        }

        string winner = PlayGame(startPlayer, countingString);
        Console.WriteLine($"Победитель: {winner}");
        Console.ReadLine();
    }
    // Метод для создания списка участников из файла
    static List<string> load(string filePath)
    {
        var players = new List<string>();
        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                players.Add(line.Trim());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }
        return players;
    }

    // Метод для создания циклического связного списка
    static Player circularList(List<string> playerNames)
    {
        if (playerNames.Count == 0) return null;

        Player first = new Player { Name = playerNames[0] };
        Player current = first;

        // Создаем список и связываем участников
        for (int i = 1; i < playerNames.Count; i++)
        {
            current.Next = new Player { Name = playerNames[i] };
            current = current.Next;
        }

        // Циклическое связывание
        current.Next = first;

        return first;
    }

    // Метод для нахождения победителя, который останется последним
    static string PlayGame(Player startPlayer, string countingString)
    {
        var currentPlayer = startPlayer;

        // считаем количество слов в считалке, а не символов
        int count = countingString.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

        for (int i = 0; i < count - 1; i++)
        {
            currentPlayer = currentPlayer.Next; // цикл продолжается по кругу
        }

        return currentPlayer.Name; // возвращаем имя игрока, на котором закончилась считалка
    }
}
