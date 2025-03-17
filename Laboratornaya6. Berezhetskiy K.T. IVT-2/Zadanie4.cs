using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie4
{
    internal class Zadanie4
    {
        static string firstPath = "E:\\BACKUP\\Учёба\\Berezhetskiy K.T. IVT-2 LAB5\\Zadanie1\\bin\\Debug\\lab.dat";
        static string secondPath = "E:\\BACKUP\\Lab6_Temp\\lab.dat";
        static string path = "E:\\BACKUP\\Lab6_Temp";
        static string firstFile = "E:\\BACKUP\\Lab6_Temp\\lab.dat";
        static string secondFile = "E:\\BACKUP\\Lab6_Temp\\lab_backup.dat";
        static void Main()
        {
            Create();
            Copy();
            CopyBite(firstFile, secondFile);
            Info();
        }
        static void Create()
        {
            Directory.CreateDirectory(path);
        }
        static void Copy()
        {
            File.Copy(firstPath, secondPath, true);
        }
        static void CopyBite(string source, string destination)
        {
            using (FileStream firstF = new FileStream(firstFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream secondF = new FileStream(secondFile, FileMode.Create, FileAccess.Write))
                {
                    int byteRead;
                    while ((byteRead = firstF.ReadByte()) != -1)
                    {
                        secondF.WriteByte((byte)byteRead);
                    }
                }
            }
        }
        static void Info()
        {
            FileInfo fileInfo = new FileInfo(firstFile);
            Console.WriteLine($"Размер файла: {fileInfo.Length} байт");
            Console.WriteLine($"Дата последнего изменения: {fileInfo.LastWriteTime}");
            Console.WriteLine($"Дата последнего доступа: {fileInfo.LastAccessTime}");
            Console.ReadLine();
        }
    }
}
