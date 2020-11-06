using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Linq;
using System.Security.Cryptography.X509Certificates;
=======
using System.IO;
//using System.Globalization;
//using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Globalization;
>>>>>>> task2-file

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            //RunMethods();
            Console.WriteLine(calculate(ParseExpression("(6+10-4)/(1+1*2)+1")));
=======
        WriteInFile(RunMethods());   
>>>>>>> task2-file
        }

        static string CreatSheet(List<string> y, List<string> x, int maxLength, int i)
        {
            string str = "";
            int len_list = x.Count;
            //body
                str += "│" + y[i];
                for (int k = 0; k <= maxLength - y[i].Length; k++)
                    str += " ";

                str += "│" + x[i];
                for (int j = 0; j <= maxLength - x[i].Length; j++)
                    str += " ";
                str += "│";
            return str;
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

        static string Cycle(string start, string line, string center, string end, int maxLength)
        {
            string str = "";
            str += start;
            for (int i = 0; i <= maxLength * 2 + 1; i++)
            {
                str += line;
                if (i == maxLength)
                    str += center;
            }
            str += end;
            return str;
        }

        static List<string> RunSheet(List<string> list_x, List<string> list_y)
        {
            List<string> sheet = new List<string>();
            sheet.Add(Cycle("╔", "═", "╤", "╗", SearchMaxLength(list_y, list_x)));
            sheet.Add(Cycle("║y", " ", "│x", "║", SearchMaxLength(list_y, list_x) - 1));
            sheet.Add(Cycle("╚", "═", "╪", "╝", SearchMaxLength(list_y, list_x)));
            for (int i = 0; i < list_x.Count; i++)
                sheet.Add(CreatSheet(list_y, list_x, SearchMaxLength(list_y, list_x), i));

            sheet.Add(Cycle("└", "─", "┴", "┘", SearchMaxLength(list_y, list_x)));
            return sheet;
        }

        static float InputValues(string outputText)
        {
            float value;
            value = Check(outputText);
            return value;
        }

        static List<string> RunMethods()
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
            return RunSheet(list_x, list_y);
        }

        static void ParseText(string line)
        {
            
        }

        static string ReadFlile()
        {
            string fullPath = Path.GetFullPath("input.txt");
            string line = "";
            int counter = 0;

            StreamReader f = new StreamReader(fullPath);
            while (line == "" && counter < 20)
            {
                line += f.ReadLine();
                counter++;
            }
            f.Close();
            
            line = line.Replace(" ", "");
            line = line.Replace("y=", "");
            return line;
        }

        static void WriteInFile(List<string> sheet)
        {
            string writePath = Path.GetFullPath("output.txt"); ;
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                for(int i = 0; i < sheet.Count; i++)
                    sw.WriteLine(sheet[i]);
            }
        }

        static string ParseExpression(string line)
        {
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
                        while (stack[stack.Count - 1] != '(')
                        {
                            str += stack[stack.Count - 1];
                            stack.RemoveAt(stack.Count - 1);
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
                    string s = "";
                    while (Char.IsDigit(line[i]))
                    {
                        s += line[i];
                        i++;
                        if (line.Length <= i)
                        {
                            break;
                        }

                    }
                    i--;
                    str += s + " ";
                }
                else
                    continue;
            }
            while(stack.Count != 0)
            {
                str += stack[stack.Count - 1];
                stack.RemoveAt(stack.Count - 1);
            }
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

        static double calculate(string str)
        {
            List<double> result = new List<double>();
            
            for(int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                {
                    string num = "";
                    while(Char.IsDigit(str[i]))
                    {
                        num += str[i];
                        i++;
                    }
                    result.Add(Convert.ToDouble(num));
                }
                else if(IsSign(str[i]))
                {
                    double num1 = result[result.Count - 1];
                    result.RemoveAt(result.Count - 1);
                    double num2 = result[result.Count - 1];
                    result.RemoveAt(result.Count - 1);

                    switch (str[i])
                    {
                        case '+': result.Add(num1 + num2); break;
                        case '-': result.Add(num2 - num1); break;
                        case '*': result.Add(num1 * num2); break;
                        case '/': result.Add(num2 / num1); break;
                        case '^': result.Add(Math.Pow(num2, num1)); break;
                    }
                }
            }
            return result[0];
        }
    }
}