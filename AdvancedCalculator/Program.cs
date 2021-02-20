using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using AdvancedCalculator;

namespace function_table
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = Rpn.StartRpn();
            WriteInFile(Sheet.StartSheet(str));
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
    }
}