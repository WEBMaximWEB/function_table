using FunctionBuilder.Logic;
using System;
using System.Collections.Generic;
using System.IO;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Rpn.StartRpn(ReadFlile());
            WriteSheet(str);
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

        private static string ReadFlile()
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

        private static void WriteSheet(string str)
        {
            double steps, xMin, xMax;
            steps = Check("шаг функции");
            xMin = Check("минимальное значение");
            xMax = Check("минимальное значение");

            WriteInFile(Sheet.StartSheet(str, steps, xMin, xMax));
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
    }
}