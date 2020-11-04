using System;
using System.Collections.Generic;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            float steps, xmin, xmax, x, y;
            var list_x = new List<string>();
            var list_y = new List<string>();

            // ввод данных
            steps = Check("шаг построения");
            xmin = Check("минимальное значение x");
            xmax = Check("максимальное значение x");

            for (x = xmin; x < xmax; x += steps)
            {
                //Здесь указать функцию:
                y = x * x - 4 * x + 4;

                string xstr = x.ToString();
                list_x.Add(xstr);
                string ystr = y.ToString();
                list_y.Add(ystr);
            }
            // таблица
            Cycle("╔", "═", "╤", "╗", Max_len(list_y, list_x));
            Cycle("║y", " ", "│x", "║", Max_len(list_y, list_x) - 1);
            Cycle("╚", "═", "╪", "╝", Max_len(list_y, list_x));
            Sheet(list_y, list_x, Max_len(list_y, list_x));
            Cycle("└", "─", "┴", "┘", Max_len(list_y, list_x));

            Console.ReadKey();
        }

        static void Sheet(List<string> y, List<string> x, int maximum_length)
        {
            int len_list = x.Count;

            //body
            for (int i = 0; i < len_list; i++)
            {
                Console.Write("│" + y[i]);
                for (int k = 0; k <= maximum_length - y[i].Length; k++)
                    Console.Write(" ");

                Console.Write("│");
                Console.Write(x[i]);
                for (int j = 0; j <= maximum_length - x[i].Length; j++)
                    Console.Write(" ");
                Console.WriteLine("│");
            }
        }

        static int Max_len(List<string> y, List<string> x)
        {
            int maxl = 0, len_list = x.Count;

            for (int i = 0; i < len_list; i++)
            {
                if (x[i].Length > maxl)
                    maxl = x[i].Length;
                if (y[i].Length > maxl)
                    maxl = y[i].Length;
            }
            return maxl;
        }

        static float Check(string text)
        {
            string a;
            float b;
            Console.Write("Укажите " + text + ":");
            a = Console.ReadLine();
            while (!float.TryParse(a, out b))
            {
                Console.Write("Вы допустили ошибку! Введите значение повторно:");
                a = Console.ReadLine();
            }
            return b;
        }

        static void Cycle(string start, string line, string center, string end, int max_l)
        {
            Console.Write(start);
            for (int i = 0; i <= max_l * 2 + 1; i++)
            {
                Console.Write(line);
                if (i == max_l)
                    Console.Write(center);
            }
            Console.WriteLine(end);
        }
    }
}