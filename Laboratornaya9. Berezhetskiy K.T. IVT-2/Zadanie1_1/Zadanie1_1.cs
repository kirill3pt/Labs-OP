using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LAB5
{
    class LAB5
    {
        struct Country // структура данных о стране
        {
            public string Name;
            public string Capital;
            public int Population;
            public string GovernmentForm;
        }

        struct Logs // структура для лога
        {
            public string Operation;
            public DateTime Timestamp;
            public string Details;
        }

        // Используем List для хранения данных о странах
        static List<Country> countries = new List<Country>();
        static List<Logs> logs = new List<Logs>();
        static DateTime lastActionTime = DateTime.Now;
        static TimeSpan longestIdleTime = TimeSpan.Zero;
        static string filePath = "lab.dat";

        static void Main()
        {
            while (true)
            {
                Open();
                UpdateTime();

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - Добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Обновить запись");
                Console.WriteLine("5 - Поиск записей");
                Console.WriteLine("6 - Просмотреть лог");
                Console.WriteLine("7 - Отсортировать записи по возрастанию населения");
                Console.WriteLine("8 - Выход");
                Console.Write("\nДЕЙСТВИЕ: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 8)
                {
                    Console.WriteLine("Ошибка ввода. Повторите попытку.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        View(); UpdateTime();
                        break;
                    case 2:
                        Add(); Saves(); UpdateTime();
                        break;
                    case 3:
                        Delete(); Saves(); UpdateTime();
                        break;
                    case 4:
                        Update(); Saves(); UpdateTime();
                        break;
                    case 5:
                        Search(); UpdateTime();
                        break;
                    case 6:
                        ViewLog(); UpdateTime();
                        break;
                    case 7:
                        countries.Sort((x, y) => x.Population.CompareTo(y.Population)); View(); Saves();
                        break;
                    case 8:
                        Saves(); Console.WriteLine("Выход. Нажмите ENTER"); Console.ReadLine(); return;
                }
            }
        }

        static void View()
        {
            if (countries.Count == 0)
            {
                Console.WriteLine("Таблица пуста.");
                return;
            }
            Console.WriteLine("География");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20}", "Государство", "Столица", "Население", "Форма правления");
            Console.WriteLine(new string('-', 70));
            foreach (var country in countries)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20}", country.Name, country.Capital, country.Population, country.GovernmentForm);
                Console.WriteLine(new string('-', 70));
            }
        }

        static void Add()
        {
            Console.Write("Введите название страны: "); string name = Console.ReadLine();
            Console.Write("Введите столицу: "); string capital = Console.ReadLine();
            Console.Write("Введите население: ");
            if (!int.TryParse(Console.ReadLine(), out int pop) || pop < 0) { Console.WriteLine("Неверное население."); return; }
            Console.Write("Введите форму правления: "); string gov = Console.ReadLine();
            countries.Add(new Country { Name = name, Capital = capital, Population = pop, GovernmentForm = gov });
            Log("Добавлена запись", name);
            Console.WriteLine("Запись добавлена.");
        }

        static void Delete()
        {
            Console.Write($"Введите номер записи для удаления (1-{countries.Count}): ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > countries.Count)
            {
                Console.WriteLine("Неверный номер."); return;
            }
            var del = countries[idx - 1].Name;
            countries.RemoveAt(idx - 1);
            Log("Удалена запись", del);
            Console.WriteLine("Запись удалена.");
        }

        static void Update()
        {
            Console.Write($"Введите номер записи для обновления (1-{countries.Count}): ");
            if (!int.TryParse(Console.ReadLine(), out int idx) || idx < 1 || idx > countries.Count)
            {
                Console.WriteLine("Неверный номер."); return;
            }
            var cur = countries[idx - 1];
            Console.Write($"Новое название (текущее: {cur.Name}): "); string name = Console.ReadLine();
            Console.Write($"Новое столица (текущая: {cur.Capital}): "); string capital = Console.ReadLine();
            Console.Write($"Новое население (текущее: {cur.Population}): "); string popIn = Console.ReadLine();
            Console.Write($"Новая форма (текущая: {cur.GovernmentForm}): "); string gov = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) cur.Name = name;
            if (!string.IsNullOrWhiteSpace(capital)) cur.Capital = capital;
            if (int.TryParse(popIn, out int p) && p > 0) cur.Population = p;
            if (!string.IsNullOrWhiteSpace(gov)) cur.GovernmentForm = gov;
            countries[idx - 1] = cur;
            Log("Обновлена запись", cur.Name);
            Console.WriteLine("Запись обновлена.");
        }

        static void Search()
        {
            Console.Write("Введите строку для поиска: "); string q = Console.ReadLine();
            var found = new List<Country>();
            foreach (var c in countries)
            {
                if (c.Name.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0 || c.Capital.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    found.Add(c);
                }
            }
            if (found.Count == 0) { Console.WriteLine("Ничего не найдено."); return; }
            Console.WriteLine("Результаты поиска:");
            foreach (var c in found)
                Console.WriteLine($"{c.Name} ({c.Capital})");
        }

        static void ViewLog()
        {
            if (logs.Count == 0)
            {
                Console.WriteLine("Лог пуст.");
                return;
            }

            Console.WriteLine("Лог:");
            foreach (var e in logs)
            {
                Console.WriteLine($"{e.Timestamp:HH:mm:ss} - {e.Operation}: {e.Details}");
            }
        }

        static void Log(string op, string det)
        {
            // если в списке уже 50 элементов, удаляем самый старый (первый)
            if (logs.Count >= 50)
                logs.RemoveAt(0);

            // добавляем новый лог в конец
            logs.Add(new Logs
            {
                Operation = op,
                Timestamp = DateTime.Now,
                Details = det
            });
        }

        static void UpdateTime()
        {
            var now = DateTime.Now;
            var idle = now - lastActionTime;
            if (idle > longestIdleTime) longestIdleTime = idle;
            lastActionTime = now;
        }

        static void Saves()
        {
            using (var writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(countries.Count);
                foreach (var c in countries)
                {
                    writer.Write(c.Name);
                    writer.Write(c.Capital);
                    writer.Write(c.Population);
                    writer.Write(c.GovernmentForm);
                }
            }
        }

        static void Open()
        {
            if (!File.Exists(filePath)) return;

            countries.Clear(); // Очистить список перед загрузкой данных

            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int cnt = reader.ReadInt32();
                for (int i = 0; i < cnt; i++)
                {
                    var c = new Country
                    {
                        Name = reader.ReadString(),
                        Capital = reader.ReadString(),
                        Population = reader.ReadInt32(),
                        GovernmentForm = reader.ReadString()
                    };
                    countries.Add(c);
                }
            }
        }
    }
}
