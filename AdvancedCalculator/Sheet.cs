using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedCalculator
{
    public class Sheet
    {
        public static List<string> StartSheet(string str)
        {
            double steps, xMin, xMax;
            steps = Check("шаг функции");
            xMin = Check("минимальное значение");
            xMax = Check("минимальное значение");

            CaculateFunction(steps, xMin, xMax, str, out List<string> listX, out List<string> listY);
            return WriteSheet(listX, listY);
        }

        private static string CreatSheet(List<string> y, List<string> x, int maxLength, int i)
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

        private static int SearchMaxLength(List<string> y, List<string> x)
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

        private static float Check(string text)
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


        private static string CreateSheet(string start, string line, string center, string end, int maxLength)
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
        private static List<string> WriteSheet(List<string> list_x, List<string> list_y)
        {
            List<string> sheet = new List<string>();
            int maxLength = SearchMaxLength(list_y, list_x);
            sheet.Add(CreateSheet("╔", "═", "╤", "╗", maxLength));
            sheet.Add(CreateSheet("║y", " ", "│x", "║", maxLength - 1));
            sheet.Add(CreateSheet("╚", "═", "╪", "╝", maxLength));
            for (int i = 0; i < list_x.Count; i++)
                sheet.Add(CreatSheet(list_y, list_x, maxLength, i));
            sheet.Add(CreateSheet("└", "─", "┴", "┘", maxLength));
            return sheet;
        }



        private static double Calculate(string str)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                {
                    string num = "";
                    while (Char.IsDigit(str[i]))
                    {
                        num += str[i];
                        i++;
                    }
                    result.Add(Convert.ToDouble(num));
                }
                else if (Rpn.IsSign(str[i]))
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

        private static void CaculateFunction(double steps, double xMin, double xMax, string str,
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
