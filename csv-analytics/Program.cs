using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csv_analytics
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string csvFile = CheckCSVFile();

            string[] csvData = File.ReadAllLines(csvFile);

            Console.WriteLine("1. Press 1 or enter 1 as input: To Insert Data");
            Console.WriteLine("2. Press 2 or enter 2 as input: To Display Data");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.KeyChar)
            {
                case '1':
                    InsertNewData(csvData);
                    break;

                case '2':
                    DisplayData(csvData);
                    break;

                case '\n':
                case '\r':
                    {
                        string input = Console.ReadLine();

                        if (input == "1")
                        {
                            InsertNewData(csvData);
                        }
                        else if (input == "2")
                        {
                            DisplayData(csvData);
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input.");
                        }

                        break;
                    }

                default:
                    Console.WriteLine("\nInvalid input.");
                    break;
            }
        }

        public static void DisplayData(string[] csvData)
        {
            Console.WriteLine("\n\nDISPLAY DATA\n============");
            var fullNames = new List<string>();
            var scores = new List<int>();
            //start at 1 - ignore column row
            for (int i = 1; i < csvData.Length; i++)
            {
                string[] rowData = csvData[i].Split(',');

                fullNames.Add($"{rowData[0]} {rowData[1]}");
                var scoreValue = Convert.ToInt32(rowData[2].Trim());
                scores.Add(scoreValue);
            }
            int maxScore = scores.Max();
            var maxScoreIndices = Enumerable.Range(0, scores.Count).Where(i => scores[i] == maxScore);
            var sortedFullNames = maxScoreIndices.Select(index => fullNames[index]).OrderBy(fullName => fullName);

            foreach (string fullName in sortedFullNames)
            {
                Console.WriteLine($"{fullName}\n");
            }

            Console.WriteLine($"Score: {maxScore}");
        }

        public static void InsertNewData(string[] csvData)
        {
            Console.WriteLine("\n\nINSERT DATA\n============");

            for (int i = 0; i < csvData.Length; i++)
            {
                string[] rowData = csvData[i].Split(',');

                if (i == 0) PromptUserInput(rowData);
            }
        }

        private static void PromptUserInput(string[] rowData)
        {
            if (rowData.Length > 1)
            {
                for (int i = 0; i < rowData.Length; i++)
                {
                    Console.Write($"Input {rowData[i]}: ");
                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine($"{rowData[i]} can not be empty");
                        return;
                    }
                    else
                    {
                        if (i == 2 && !int.TryParse(input, out int score))
                        {
                            Console.WriteLine($"{rowData[i]} must be a number");
                            return;
                        }

                        rowData[i] = input;
                    }
                }

                WriteCSVData(rowData);
            }
        }

        private static void WriteCSVData(string[] rowData)
        {
            string csvFile = CheckCSVFile();

            using (StreamWriter writer = new StreamWriter(csvFile, true))
            {
                writer.WriteLine(string.Join(",", rowData));
            }

            Console.WriteLine("Input values written to CSV file.");
        }

        private static string CheckCSVFile()
        {
            string csvFile = "TestData.csv";

            if (!File.Exists(csvFile))
            {
                File.Create(csvFile).Close();
                Console.WriteLine("CSV file is created.");
            }

            return csvFile;
        }
    }
}