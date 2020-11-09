using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            double steps, xMin, xMax;
            steps = Check("шаг функции");
            xMin = Check("минимальное значение");
            xMax = Check("минимальное значение");

            string str = ParseExpression(ParseText(ReadFlile()));
            CaculateFunction(steps, xMin, xMax, str, out List<string> listX, out List<string> listY);
            WriteSheet(listX, listY);
        }

        static string CreatSheet(List<string> y, List<string> x, int maxLength, int i)
        {
            string str = "";
            //создаем тело таблицы
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

            //поиск максимальной длины значения
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
            //проверка введенных значений
            string input;
            float output;
            Console.Write("Укажите " + text + ":");
            input = Console.ReadLine();
            while (!float.TryParse(input, out output))
            {
                Console.Write("Вы допустили ошибку! Введите значение повторно:");
                input = Console.ReadLine();
            }
            return output;
        }

        static string Cycle(string start, string line, string center, string end, int maxLength)
        {
            //шапка таблицы
            string str = "";
            str += start;
            for (int i = 0; i <= maxLength * 2 + 1; i++)
            {
                str += line;
                if (i == maxLength) str += center;
            }
            str += end;
            return str;
        }

        //построчно записываем таблицу в список
        static List<string> WriteSheet(List<string> list_x, List<string> list_y)
        {
            List<string> sheet = new List<string>();
            int maxLength = SearchMaxLength(list_y, list_x);
            sheet.Add(Cycle("╔", "═", "╤", "╗", maxLength));
            sheet.Add(Cycle("║y", " ", "│x", "║", maxLength - 1));
            sheet.Add(Cycle("╚", "═", "╪", "╝", maxLength));
            for (int i = 0; i < list_x.Count; i++)
                sheet.Add(CreatSheet(list_y, list_x, maxLength, i));
            sheet.Add(Cycle("└", "─", "┴", "┘", maxLength));
            return sheet;
        }

        static string ParseText(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("y=", "");
            return line;
        }

        static string ReadFlile()
        {
            string line = "";
            int counter = 0;

            StreamReader f = new StreamReader(@"intput.txt");
            while (line == "" && counter < 20)
            {
                line += f.ReadLine();
                counter++;
            }
            f.Close();
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
                if (line[i] == 'x')
                    str += "x";
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
                            str += stack[^1];
                            stack.RemoveAt(stack.Count - 1);
                        }
                        stack.RemoveAt(stack.Count - 1);
                        continue;
                    }
                    if (SearchPriority(line[i]) <= StackPriority(stack) && stack.Count > 0)
                    {
                        str += stack[^1];
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
                            break;
                    }
                    i--;
                    str += s + " ";
                }
                else
                    continue;
            }
            while(stack.Count != 0)
            {
                str += stack[^1];
                stack.RemoveAt(stack.Count - 1);
            }
            Console.WriteLine(str);
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
                return SearchPriority(stack[^1]);
        }

        static double Calculate(string str)
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
                    double num1 = result[^1];
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

        static void CaculateFunction(double steps, double xMin, double xMax, string str,
                                    out List<string> listX, out List<string> listY)
        {
            double xValue, yValue;
            listX = new List<string>();
            listY = new List<string>();
            for (xValue = xMin; xValue < xMax; xValue += steps)
            {
                str = str.Replace("x", xValue.ToString());
                yValue = Calculate(str);

                string xstr = xValue.ToString();
                listX.Add(xstr);
                string ystr = yValue.ToString();
                listY.Add(ystr);
            }
        }
    }
}