using System;
using System.IO;

class BMPReader
{
    static void Main()
    {
        BMPRead();
        Console.ReadLine();
    }
    static void BMPRead()
    {
        Console.Write("Введите имя BMP-файла (без пути): ");
        string fileName = Console.ReadLine();

        // получаем полный путь к файлу в папке программы
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

        // проверяем, существует ли файл
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Ошибка: файл не найден!");
            return;
        }
        Console.WriteLine($"Файл найден: {filePath}");

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader reader = new BinaryReader(fs))
            {
                fs.Seek(2, SeekOrigin.Begin);
                int fileSize = reader.ReadInt32(); // размер файла

                fs.Seek(18, SeekOrigin.Begin);
                int width = reader.ReadInt32();  // ширина изображения
                int height = reader.ReadInt32(); // высота изображения

                fs.Seek(28, SeekOrigin.Begin);
                short bitsPerPixel = reader.ReadInt16(); // Глубина цвета

                fs.Seek(30, SeekOrigin.Begin);
                int compression = reader.ReadInt32(); // тип сжатия

                fs.Seek(38, SeekOrigin.Begin);
                int horizontalResolution = reader.ReadInt32(); // горизонтальное разрешение
                int verticalResolution = reader.ReadInt32();   // вертикальное разрешение

                Console.WriteLine("\nИнформация о BMP-файле:");
                Console.WriteLine($"Размер файла: {fileSize} байт");
                Console.WriteLine($"Размер изображения: {width} x {height} пикселей");
                Console.WriteLine($"Глубина цвета: {bitsPerPixel} бит/пиксель");
                Console.WriteLine($"Разрешение: {horizontalResolution} x {verticalResolution} пикселей/м");

                string compressionType;
                switch (compression)
                {
                    case 0:
                        compressionType = "Без сжатия (BI_RGB)";
                        break;
                    case 1:
                        compressionType = "8-bit RLE (BI_RLE8)";
                        break;
                    case 2:
                        compressionType = "4-bit RLE (BI_RLE4)";
                        break;
                    default:
                        compressionType = "Неизвестный формат";
                        break;
                }
                Console.WriteLine($"Тип сжатия: {compressionType}");
            }
        }
    }
}
