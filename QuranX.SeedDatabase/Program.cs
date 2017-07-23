﻿using System;
using System.IO;

namespace QuranX.SeedDatabase
{
    class Program
    {
        static string DataFolder;

        static void Main(string[] args)
        {
            if ((args ?? new string[0]).Length != 1 || !Directory.Exists(args[0]))
                ShowHelp();
            else
            {
                DataFolder = args[0];
                QuranImporter.Execute(DataFolder);
                TafsirImporter.Execute(DataFolder);
                HadithImporter.Execute(DataFolder);
                Console.WriteLine("===========DONE=========");
                Console.ReadLine();
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("QuranX.SeedDatabase {path to data files}");
        }

    }
}