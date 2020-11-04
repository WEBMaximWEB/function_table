﻿using System;
using System.Collections.Generic;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMethods();
        }

        static void CreatSheet(List<string> y, List<string> x, int maxLength)
        {
            int len_list = x.Count;
            //body
            for (int i = 0; i < len_list; i++)
            {
                Console.Write("│" + y[i]);
                for (int k = 0; k <= maxLength - y[i].Length; k++)
                    Console.Write(" ");

                Console.Write("│");
                Console.Write(x[i]);
                for (int j = 0; j <= maxLength - x[i].Length; j++)
                    Console.Write(" ");
                Console.WriteLine("│");
            }
        }

        static int SearchMaxLength(List<string> y, List<string> x)
        {
            int maxLength = 0, len_list = x.Count;

            for (int i = 0; i < len_list; i++)
            {
                if (x[i].Length > maxLength)
                    maxLength = x[i].Length;
                if (y[i].Length > maxLength)
                    maxLength = y[i].Length;
            }
            return maxLength;
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

        static void Cycle(string start, string line, string center, string end, int maxLength)
        {
            Console.Write(start);
            for (int i = 0; i <= maxLength * 2 + 1; i++)
            {
                Console.Write(line);
                if (i == maxLength)
                    Console.Write(center);
            }
            Console.WriteLine(end);
        }

        static void RunSheet(List<string> list_x, List<string> list_y)
        {
            Cycle("╔", "═", "╤", "╗", SearchMaxLength(list_y, list_x));
            Cycle("║y", " ", "│x", "║", SearchMaxLength(list_y, list_x) - 1);
            Cycle("╚", "═", "╪", "╝", SearchMaxLength(list_y, list_x));
            CreatSheet(list_y, list_x, SearchMaxLength(list_y, list_x));
            Cycle("└", "─", "┴", "┘", SearchMaxLength(list_y, list_x));
        }

        static float InputValues(string outputText)
        {
            float value;
            value = Check(outputText);
            return value;
        }

        static void RunMethods()
        {
            float steps, xMin, xMax, x, y;
            var list_x = new List<string>();
            var list_y = new List<string>();

            steps = InputValues("шаг функции");
            xMin = InputValues("минимальное значение");
            xMax = InputValues("минимальное значение");

            for (x = xMin; x < xMax; x += steps)
            {
                //Здесь указать функцию:
                y = x * x - 4 * x + 4;

                string xstr = x.ToString();
                list_x.Add(xstr);
                string ystr = y.ToString();
                list_y.Add(ystr);
            }
            RunSheet(list_x, list_y);

            Console.ReadKey();
        }
    }
}