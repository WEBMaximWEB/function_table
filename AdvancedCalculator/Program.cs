using System;
using System.Collections.Generic;
using System.IO;
//using System.Globalization;
//using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Globalization;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteInFile(RunMethods());
        }

        static string CreatSheet(List<string> y, List<string> x, int maxLength)
        {
            string str = "";
            int len_list = x.Count;
            //body
            for (int i = 0; i < len_list; i++)
            {
                str += "│" + y[i];
                for (int k = 0; k <= maxLength - y[i].Length; k++)
                    str += " ";

                str += "│" + x[i];
                for (int j = 0; j <= maxLength - x[i].Length; j++)
                    str += " ";
                str += "│";
            }
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
            sheet.Add(CreatSheet(list_y, list_x, SearchMaxLength(list_y, list_x)));
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
            //List<char> nums = for(int i = 0; i < 10; i++) nums.Add((char)i);
            //List<char> signs = new List<char>{'+', '-', '*', '/', ')', '('}
            //foreach(char i in line)
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
            return line;
        }

        static void WriteInFile(List<string> sheet)
        {
            string writePath = @"C:/Users/pmaxq/OneDrive/Desktop/output.txt";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                    sw.WriteLine("hello");
            }
            //var path = System.IO.Path.GetFullPath(@"OneDrive\Desktop");
            //StreamWriter sw = new StreamWriter(path, true);
            //StreamWriter sw = new StreamWriter("C:/Users/pmaxq/OneDrive/Desktop/output.txt", true);
            //for (int i = 0; i < sheet.Count; i++)
             //   sw.WriteLine(sheet[i]);
            //sw.Close();
        }
    }
}