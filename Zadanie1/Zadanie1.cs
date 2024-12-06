using System;

namespace Laboratornaya2
{
    class Zadanie1
    {
        static void Main()
        {
            int a, b, c, discriminant;
            float x, y, x1, x2;

            Console.WriteLine("Введите числа с клавиатуры для решения квадратного уравнения вида ax^2+bx+c=0");
            Console.Write("Введите аx^2: ");
            a = int.Parse(Console.ReadLine());
            Console.Write("Введите bx: ");
            b = int.Parse(Console.ReadLine());
            Console.Write("Введите c: ");
            c = int.Parse(Console.ReadLine());

            discriminant = (b * b) - (4 * a * c);

            if (discriminant > 0)
            {
                x1 = (float)((-b - Math.Sqrt(discriminant)) / (2 * a));
                x2 = (float)((-b + Math.Sqrt(discriminant)) / (2 * a));
                Console.WriteLine($"Дискриминант = {discriminant}, 2 действительных корня: \n" +
                    $"x1 = {x1}, x2 = {x2}");
            }

            else if (discriminant == 0)
            {
                x = (float)(-b) / (2 * a);
                Console.WriteLine($"Дискриминант = {discriminant}, 1 действительный корень: \n" +
                    $"x = {x}");
            }

            else
            {
                discriminant = -discriminant;
                x1 = -b / (2 * a);
                x = (float)(x1 - (Math.Sqrt(discriminant)));
                y = (float)(x1 + (Math.Sqrt(discriminant)));
                Console.WriteLine($"Дискриминант = -{discriminant}, нет действительных корней. Ответ в виде комплексных чисел: \n" +
                    $"x1 = {x} + {y}*i\n" +
                    $"x2 = {x} - {y}*i ");
            }
            Console.ReadLine();

        }
    }
}