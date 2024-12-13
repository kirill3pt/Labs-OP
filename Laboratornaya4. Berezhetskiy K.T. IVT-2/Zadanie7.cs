using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Zadanie7
{
    internal class Zadanie7
    {
        static void Main()
        {
            string[] tracklist = {
                "Gentle Giant – Free Hand [6:15]",
                "Supertramp – Child Of Vision [07:27]",
                "Camel – Lawrence [10:46]",
                "Yes – Don’t Kill The Whale [3:55]",
                "10CC – Notell Hotel [04:58]",
                "Nektar – King Of Twilight [4:16]",
                "The Flower Kings – Monsters & Men [21:19]",
                "Focus – Le Clochard [1:59]",
                "Pendragon – Fallen Dream And Angel [5:23]",
                "Kaipa – Remains Of The Day (08:02)"
            };

            List<Track> tracks = new List <Track>();
            
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА СУММЫ ВРЕМЕНИ ЗВУЧАНИЯ ВСЕХ ТРЕКОВ
            int SumOfDlitelnost = 0;
            Regex regex = new Regex (@"\[(\d+):(\d+)\]|\((\d+):(\d+)\)");//в этом регулярном выражении обрабатывается время трека
                                                                         //для случая [0:00] и для (0:00)

            foreach (string line in tracklist)
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    int minutes = int.Parse(match.Groups[1].Value != "" ? match.Groups[1].Value : match.Groups[3].Value);
                    int seconds = int.Parse(match.Groups[2].Value != "" ? match.Groups[2].Value : match.Groups[4].Value);

                    int Dlitelnost = minutes * 60 + seconds;
                    SumOfDlitelnost += Dlitelnost;

                    tracks.Add(new Track(line, Dlitelnost));
                }
            }
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА СУММЫ ВРЕМЕНИ ЗВУЧАНИЯ ВСЕХ ТРЕКОВ
            //-------------------------------------------------------------
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА САМОГО КОРОТКОГО ТРЕКА И САМОГО ДЛИННОГО
            Track maxDlit = null;
            Track minDlit = null;
            foreach (Track track in tracks)
            {
                if (maxDlit == null || track.Dlitelnost > maxDlit.Dlitelnost)
                {
                    maxDlit = track;
                }
                if (minDlit == null || track.Dlitelnost < minDlit.Dlitelnost)
                {
                    minDlit = track;
                }
            }
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА САМОГО КОРОТКОГО ТРЕКА И САМОГО ДЛИННОГО
            //-------------------------------------------------------------
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА ПАРЫ С САМОЙ МИНИМАЛЬНОЙ РАЗНИЦЕЙ ЗВУЧАНИЯ ТРЕКА
            Track raznitsa1 = null;
            Track raznitsa2 = null;
            int minRaznitsa = int.MaxValue;
            for (int i = 0; i < tracks.Count; i++)
            {
                for (int j = i + 1; j < tracks.Count; j++)
                {
                    int raznitsa = Math.Abs(tracks[i].Dlitelnost - tracks[j].Dlitelnost);
                    if (raznitsa < minRaznitsa)
                    {
                        minRaznitsa = raznitsa;
                        raznitsa1 = tracks[i];
                        raznitsa2 = tracks[j];
                    }
                }
            }
            //ЧАСТЬ КОДА ДЛЯ ПОИСКА ПАРЫ С САМОЙ МИНИМАЛЬНОЙ РАЗНИЦЕЙ ЗВУЧАНИЯ ТРЕКА
            //-------------------------------------------------------------
            //ЧАСТЬ КОДА ДЛЯ ВЫВОДА РЕЗУЛЬТАТОВ
            Console.WriteLine($"Общая длительность всех треков: {FormatTime(SumOfDlitelnost)} ");
            Console.WriteLine($"Cамая длинная песня: {maxDlit.Title}. Его длительность: {FormatTime(maxDlit.Dlitelnost)}");
            Console.WriteLine($"Cамая короткая песня: {minDlit.Title}. Его длительность: {FormatTime(minDlit.Dlitelnost)}");
            Console.WriteLine($"Самая маленькая разница во времени звучания = {FormatTime(minRaznitsa)} у треков:");
            Console.WriteLine($"1: {raznitsa1.Title} ({FormatTime(raznitsa1.Dlitelnost)})");
            Console.WriteLine($"2: {raznitsa2.Title} ({FormatTime(raznitsa2.Dlitelnost)})");
            //ЧАСТЬ КОДА ДЛЯ ВЫВОДА РЕЗУЛЬТАТОВ
            Console.ReadLine();
        }

        static string FormatTime (int totSec)
        {
            int minutes = totSec / 60;
            int seconds = totSec % 60;
            return $"{minutes:D2}:{seconds:D2}";
        }
    }
    public class Track
    {
        public string Title { get; set; }
        public int Dlitelnost { get; set; }
        public Track(string title, int dlitelnos)
        {
            Title = title;
            Dlitelnost = dlitelnos;
        }
    }
}

