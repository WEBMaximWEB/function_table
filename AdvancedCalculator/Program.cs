using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunMethods();
            Console.WriteLine(ParseExpression("(6+10-4)/(1+1*2)+1"));
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

        static string ParseExpression(string line)
        {
            Console.WriteLine("*");
            string str = "";
            List<char> stack = new List<char>();

            for(int i = 0; i < line.Length; i++)
            {
                if (IsSign(line[i]))
                {
                    if (SearchPriority(line[i]) == 0)
                    {
                        stack.Add(line[i]);
                        continue;
                    }
                        if (SearchPriority(line[i]) == 1)
                    {
                        Console.WriteLine("*" + stack[stack.Count - 1]);
                        while (stack[stack.Count - 1] != '(')
                        {
                            str += stack[stack.Count - 1];
                            stack.RemoveAt(stack.Count - 1);
                            Console.WriteLine("*");
                        }
                        stack.RemoveAt(stack.Count - 1);
                        continue;
                    }
                    if (SearchPriority(line[i]) <= StackPriority(stack) && stack.Count > 0)
                    {
                        str += stack[stack.Count - 1];
                        stack.RemoveAt(stack.Count - 1);
                    }

                    if (SearchPriority(line[i]) > StackPriority(stack))
                        stack.Add(line[i]);
                }
                else if (Char.IsDigit(line[i]))
                {
                    while (Char.IsDigit(line[i]))
                    {
                        str += line[i];
                        i++;
                        if (line.Length <= i)
                            break;
                    }
                    i--;
                }
                else
                    continue;
            }
            while(stack.Count != 0)
            {
                str += stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
            }
            Console.WriteLine("str= " + str);
            return str;
        }

        static bool IsSign(char x)
        {
            Char[] signs = { '+', '-', '*', '^', '/', '(', ')' };
            if (signs.Contains(x))
                return true;
            else
                return false;
        }

        static int SearchPriority(char x)
        {
            switch(x)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 10;
            }
        }

        static int StackPriority(List<char> stack)
        {
            if (stack.Count == 0)
                return 1;
            else
                return SearchPriority(stack[stack.Count - 1]);
        }
    }
}