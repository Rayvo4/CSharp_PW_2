using System;
using PolynomialLib;

namespace PolynomialDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
                        Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Конструктори
            Polynomial p1 = new Polynomial(new double[] { 1, -2, 3 });   // 3x^2 - 2x + 1
            Polynomial p2 = new Polynomial(new double[] { 4, 5 });       // 5x + 4
            Polynomial p0 = new Polynomial();                            // 0
            Polynomial copy = new Polynomial(p1);                        // копія p1

            Console.WriteLine("=== Конструктори та ToString ===");
            Console.WriteLine("p1 = " + p1);
            Console.WriteLine("p2 = " + p2);
            Console.WriteLine("p0 (за замовчуванням) = " + p0);
            Console.WriteLine("copy (копія p1) = " + copy);
            Console.WriteLine("Розмірність p1 = " + p1.Size + ", степінь p1 = " + p1.Degree);

            Console.WriteLine();
            Console.WriteLine("=== Арифметичні операції ===");
            Console.WriteLine("p1 + p2 = " + (p1 + p2));
            Console.WriteLine("p1 - p2 = " + (p1 - p2));
            Console.WriteLine("p1 - p1 = " + (p1 - p1));
            Console.WriteLine("p1 + 10 = " + (p1 + 10));
            Console.WriteLine("10 + p1 = " + (10 + p1));
            Console.WriteLine("p1 * 2 = " + (p1 * 2));
            Console.WriteLine("2 * p1 = " + (2 * p1));

            Console.WriteLine();
            Console.WriteLine("=== Індексатори ===");
            Console.WriteLine("Коефіцієнт p1 при x^2: p1[2] = " + p1[2]);
            Console.WriteLine("Коефіцієнт p1 при x^5: p1[5] = " + p1[5]);
            Console.WriteLine("Значення p1 у точці x = 2: p1[2.0] = " + p1[2.0]);
            Console.WriteLine("Значення p2 у точці x = -1,5: p2[-1.5] = " + p2[-1.5]);
            Polynomial p3 = new Polynomial(p2);
            p3[3] = 1;
            Console.WriteLine("p3 = копія p2, p3[3] = 1  =>  p3 = " + p3);

            Console.WriteLine();
            Console.WriteLine("=== Порівняння ===");
            Console.WriteLine("p1 == copy: " + (p1 == copy));
            Console.WriteLine("p1 == p2:   " + (p1 == p2));
            Console.WriteLine("p1 != p2:   " + (p1 != p2));

            Console.WriteLine();
            Console.WriteLine("=== Операції true і false ===");
            if (p1)
                Console.WriteLine("p1 — ненульовий поліном (true)");
            else
                Console.WriteLine("p1 — нульовий поліном (false)");
            if (p1 - copy)
                Console.WriteLine("p1 - copy — ненульовий поліном (true)");
            else
                Console.WriteLine("p1 - copy — нульовий поліном (false)");

            Console.WriteLine();
            Console.WriteLine("=== Приведення типів ===");
            Polynomial constant = 7.5;                 // неявне double -> Polynomial
            Console.WriteLine("Polynomial constant = 7.5  =>  " + constant + ", степінь " + constant.Degree);
            Console.WriteLine("constant == 7.5 (число неявно стає поліномом): " + (constant == 7.5));
            double number = (double)constant;          // явне Polynomial -> double
            Console.WriteLine("(double)constant = " + number);
            try
            {
                double wrong = (double)p1;
                Console.WriteLine("(double)p1 = " + wrong);
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine("(double)p1: " + e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("=== Функція введення ===");
            Polynomial input = new Polynomial();
            input.Input();
            Console.WriteLine("Введений поліном: " + input);
            Console.WriteLine("Його значення у точці x = 1: " + input[1.0]);
            Console.WriteLine("Введений + p1 = " + (input + p1));

            Console.ReadKey();
        }
    }
}