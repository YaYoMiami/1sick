using System;
using System.IO;
using System.Collections.Generic;

namespace _4._16_СИ
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\TIIT-Raschetka\Tests\";
            int test_num = 3;
            for (int i = 1; i <= test_num; i++)
            {
                StreamReader input = new StreamReader(path + "input" + i + ".txt");
                Orgraf orgraf = new Orgraf(input);
                orgraf.Build_KonGr();
                Console.WriteLine("Test #" + i);
                orgraf.Print();
                Console.WriteLine();
                input.Close();
            }
        }
    }
}
