using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a directory path.");
            return;
        }

        string path = args[0];
        if (!Directory.Exists(path))
        {
            Console.WriteLine("The provided path does not exist.");
            return;
        }

        try
        {
            IEnumerable<string> files = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".cs") || s.EndsWith(".csv"));

            Regex uuidRegex = new Regex(@"\b[0-9a-fA-F]{8}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{4}\b-[0-9a-fA-F]{12}\b");
            int totalCount = 0;
            foreach (var file in files)
            {
                string content = File.ReadAllText(file);
                MatchCollection matches = uuidRegex.Matches(content);
                

                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                    totalCount++;
                }
            }
            Console.WriteLine($"Total UUIDs found: {totalCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
