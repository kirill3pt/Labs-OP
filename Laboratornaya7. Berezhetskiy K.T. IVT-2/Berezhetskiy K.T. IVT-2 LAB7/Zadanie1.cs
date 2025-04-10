using System;
using System.Collections.Generic;
using System.IO;

namespace LAB5
{
    class LAB5
    {
        struct Country //структура для данных, связанных с любой страной, которую будем добавляь
        {
            public string Name;
            public string Capital;
            public int Population;
            public string GovernmentForm;
        }

        struct Logs //структура для лога
        {
            public string Operation;
            public DateTime Timestamp;
            public string Details;
        }

        static Country[] countries = new Country[100]; //массив структур из 100 стран
        static int countryCount = 0; //счетчик для количества стран. начинается с 0, ибо изначально у нас не вписано ничего

        static Logs[] logs = new Logs[50]; //массив структур из 50 логов
        static int logCount = 0; //счетчик для количества логов

        static DateTime lastActionTime = DateTime.Now; // время последнего действия в программе
        static TimeSpan longestIdleTime = TimeSpan.Zero; // для отображения периода бездействия 

        static string filePath = "lab.dat"; //имя бинарного файла, куда будем сохранять все записи

        static void Main()
        {
            while (true)
            {
                Open();
                UpdateTime(); // обновляем время последнего действия при каждом выполнении цикла

                //предложенные действия в соотвествии с условием задания
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 - Просмотр таблицы");
                Console.WriteLine("2 - Добавить запись");
                Console.WriteLine("3 - Удалить запись");
                Console.WriteLine("4 - Обновить запись");
                Console.WriteLine("5 - Поиск записей");
                Console.WriteLine("6 - Просмотреть лог");
                Console.WriteLine("7 - Отсортировать записи по возрастанию количества населения");
                Console.WriteLine("8 - Выход");
                //предложенные действия в соотвествии с условием задания
                Console.Write("\nДЕЙСТВИЕ: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 8) //тут проверяем, введено ли число от 1 до 7
                {
                    Console.WriteLine("Ошибка ввода. Повторите попытку.");
                    continue;
                }

                switch (choice)
                //часть кода для выбора. если выбирается определенная цифра, то выполняется действие, которое ей соответствует
                {
                    case 1:
                        View();
                        UpdateTime();
                        break;
                    case 2:
                        Add();
                        Saves();
                        UpdateTime();
                        break;
                    case 3:
                        Delete();
                        Saves();
                        UpdateTime();
                        break;
                    case 4:
                        Update();
                        Saves();
                        UpdateTime();
                        break;
                    case 5:
                        Search();
                        UpdateTime();
                        break;
                    case 6:
                        ViewLog();
                        UpdateTime();
                        Console.WriteLine($"\nСамый долгий период бездействия пользователя: {longestIdleTime.ToString(@"hh\:mm\:ss")}");
                        Console.WriteLine();
                        break;
                    case 7:
                        Sort();
                        View();
                        Saves();
                        break;
                    case 8:
                        Saves();
                        Console.WriteLine("Выход из программы. Нажмите ENTER");
                        Console.ReadLine();
                        return;
                }
                //часть кода для выбора. если выбирается определенная цифра, то выполняется действие, которое ей соответствует
            }
        }

        static void View() //функция для просмотра таблицы. 
        {
            if (countryCount == 0) //если нет стран, выводим сообщение
            {
                Console.WriteLine("Таблица пуста.");
                return;
            }

            //часть кода для заголовка таблицы
            Console.WriteLine("География");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20}", "Государство", "Столица", "Население", "Форма правления");
            Console.WriteLine(new string('-', 70));
            // часть кода для заголовка таблицы

            //цикл для отображения всех стран
            for (int i = 0; i < countryCount; i++)
            {
                var country = countries[i];
                Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20}", country.Name, country.Capital, country.Population, country.GovernmentForm);
                Console.WriteLine(new string('-', 70)); //когда все данные были отображены для 1 страны, делаем новую линию и повторяем действия
            }
            Console.WriteLine("{0,-20}", "Перечисляемый тип: Ф - Федерация, УГ - Унитарное государство"); //это часть для отображения нижней строки
            Console.WriteLine(new string('-', 70));
            //цикл для отображения всех стран
            UpdateTime();
        }

        static void Add() //функция для добавления элементов в таблицу
        {
            if (countryCount >= countries.Length) //если достигли ограничения в 100 стран, то выводим сообщение
            {
                Console.WriteLine("Невозможно добавить запись: достигнуто ограничение в 100 стран.");
                return;
            }

            //ввод данных
            Console.Write("Введите название страны: ");
            UpdateTime();
            string name = Console.ReadLine();
            UpdateTime();
            Console.Write("Введите столицу: ");
            UpdateTime();
            string capital = Console.ReadLine();
            UpdateTime();
            Console.Write("Введите население: ");
            UpdateTime();

            if (!int.TryParse(Console.ReadLine(), out int population) || population < 0)
            {
                Console.WriteLine("Неверное значение населения.");
                return;
            }

            Console.Write("Введите форму правления: Ф - федерация, УГ - унитарное государство ");
            string governmentForm = Console.ReadLine();
            UpdateTime();
            //ввод данных

            countries[countryCount++] = new Country { Name = name, Capital = capital, Population = population, GovernmentForm = governmentForm };
            //добавление записи,
            //увеличиваем счетчик стран на 1, создаем новую страну.                                                                                                                                
            Log("Добавлена запись", name);

            Console.WriteLine("Запись добавлена.");
        }

        static void Delete() //функция для удаления страны из таблицы
        {
            Console.Write("Введите номер записи для удаления (от 1 до {0}): ", countryCount); //ввод номера страны, которую нам нужно удалить
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > countryCount) //ограничение для ввода
            {
                Console.WriteLine("Неверный номер записи.");
                return;
            }

            string deletedName = countries[index - 1].Name; //сохраняем название страны для дальнейшего добавления в лог

            for (int i = index - 1; i < countryCount - 1; i++) //освобождаем место в массиве
            {
                countries[i] = countries[i + 1]; //перенос страны, которая стояла после удаленной, на её место
            }

            countryCount--; //уменьшаем счётчик стран
            Log("Удалена запись", deletedName);

            Console.WriteLine("Запись удалена.");
            UpdateTime();
        }

        static void Update() //функция для обновления данных
        {
            Console.Write("Введите номер записи для обновления (от 1 до {0}): ", countryCount); //ввод номера страны, которую нам нужно изменить
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > countryCount) //задаем ограничение для ввода
            {
                Console.WriteLine("Неверный номер записи.");
                return;
            }

            var country = countries[index - 1]; //извлекаем запись из массива, 

            //ввод данных
            Console.Write("Введите новое название страны (текущее: {0}): ", country.Name);
            string name = Console.ReadLine();
            UpdateTime();
            Console.Write("Введите новую столицу (текущая: {0}): ", country.Capital);
            string capital = Console.ReadLine();
            UpdateTime();
            Console.Write("Введите новое население (текущее: {0}): ", country.Population);
            string populationInput = Console.ReadLine();
            UpdateTime();
            Console.Write("Введите новую форму правления (текущая: {0}): ", country.GovernmentForm);
            string governmentForm = Console.ReadLine();
            UpdateTime();
            //ввод данных

            //здесь если ничего не ввели для какой-то части таблицы, то всё остается так, как и было
            if (!string.IsNullOrWhiteSpace(name)) country.Name = name;
            if (!string.IsNullOrWhiteSpace(capital)) country.Capital = capital;
            if (int.TryParse(populationInput, out int population) && population > 0) country.Population = population; //если вводим число и оно больше 0,
                                                                                                                      //то обновляем данные. иначе нет
            if (!string.IsNullOrWhiteSpace(governmentForm)) country.GovernmentForm = governmentForm;
            //здесь если ничего не ввели для какой-то части таблицы, то всё остается так, как и было

            countries[index - 1] = country; //добавляем запись снова в массив, уже измененную
            Log("Обновлена запись", name);

            Console.WriteLine("Запись обновлена.");
            UpdateTime();
        }

        static void Search() //функция для поиска данных (выбпрал фильтр по поиску столицы/страны, выдает соответствующую поиску столицу/страну
        {
            Console.Write("Введите строку для поиска: ");
            string query = Console.ReadLine();

            var results = new List<Country>(); //создаем список для хранения найденных данных
            for (int i = 0; i < countryCount; i++)
            {
                if (countries[i].Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    countries[i].Capital.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) //если столица или страна совпадает с вводом,
                                                                                                  //то добавляем страну и столицу в список
                {
                    results.Add(countries[i]);
                }
            }
            if (results.Count == 0)
            {
                Console.WriteLine("Ничего не найдено."); //если ничего не нашли - выдаем сообщение
                return;
            }

            Console.WriteLine("Результаты поиска:"); //выводим найденные данные
            foreach (var country in results)
            {
                Console.WriteLine("{0} ({1})", country.Name, country.Capital);
            }
            UpdateTime();
        }
        static void ViewLog() //функция для отображения лога
        {
            if (logCount == 0)
            {
                Console.WriteLine("Лог пуст."); //если нет записей в логе, то выводим сообщение 
                return;
            }

            Console.WriteLine("Лог:");
            for (int i = 0; i < logCount; i++) //проходимся по всему логу, пока не дойдем до конца
            {
                var entry = logs[i]; //получаем запись на текущем номере операции
                Console.WriteLine($"{entry.Timestamp:HH:mm:ss} - {entry.Operation}: {entry.Details}");//выводим в соответсвии с условием 
            }
            UpdateTime();
        }

        static void Log(string operation, string details) //функция для заполнения лога
        {
            if (logCount >= logs.Length) //если достигли ограничения 
            {
                Array.Copy(logs, 1, logs, 0, logs.Length - 1); //сдвигаем все данные лога на одну позицию влево, удаляя самую первую запись
                                                               //копируем все элементы массива, начиная с 1, в начало массива (0).
                                                               //уменьшаем счетчик на 1
                logCount--;
            }

            logs[logCount++] = new Logs { Operation = operation, Timestamp = DateTime.Now, Details = details };
            //добавляем новую запись, записывая время выполнения операции, её время и что именно было добавлено, увеличиваем счетчик на 1
        }

        static void UpdateTime() //функция для обновления времени
        {
            var currentTime = DateTime.Now; //узнаем настоящее значение времени
            var idleTime = currentTime - lastActionTime; //вычитаем из настоящего времени время последнего действия

            if (idleTime > longestIdleTime) //если время простоя больше самого длинного времени простоя, то
            {
                longestIdleTime = idleTime; //обновляем значение времени простоя
            }
            lastActionTime = currentTime; //присваиваем настоящеее время для времени последнего действия
        }
        static void Saves()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                writer.Write(countryCount); //сначала записываем количество записей
                for (int i = 0; i < countryCount; i++)
                {
                    writer.Write(countries[i].Name);
                    writer.Write(countries[i].Capital);
                    writer.Write(countries[i].Population);
                    writer.Write(countries[i].GovernmentForm);
                }
            }
        }
        static void Open()
        {
            if (!File.Exists(filePath))
            {
                return; //проверяем, есть ли файл
            }
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                countryCount = reader.ReadInt32();
                for (int i = 0; i < countryCount; i++)
                {
                    countries[i] = new Country
                    {
                        Name = reader.ReadString(),
                        Capital = reader.ReadString(),
                        Population = reader.ReadInt32(),
                        GovernmentForm = reader.ReadString()
                    };
                }
            }
        }
        static void Sort()
        {
            for (int i = 0; i < countryCount - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < countryCount; j++)
                {
                    if (countries[j].Population < countries[minIndex].Population)
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    Country temp = countries[i];
                    countries[i] = countries[minIndex];
                    countries[minIndex] = temp;
                }
            }
            Console.WriteLine("Записи отсортированы по населению.");
        }

    }
}
