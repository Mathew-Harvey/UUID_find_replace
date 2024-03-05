using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class UUIDTransformer
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: UUIDTransformer <directoryPath>");
            return;
        }

        string directoryPath = "/Users/mathewharvey/Documents/TestUUID/";
        TransformUUIDsInDirectory(directoryPath);
    }

    private static void TransformUUIDsInDirectory(string directoryPath)
    {
        var filePatterns = new string[] { "*.cs", "*.csv" };
        foreach (var pattern in filePatterns)
        {
            foreach (var filePath in Directory.EnumerateFiles(directoryPath, pattern, SearchOption.AllDirectories))
            {
                TransformUUIDsInFile(filePath);
            }
        }
    }

    private static void TransformUUIDsInFile(string filePath)
    {
        string content = File.ReadAllText(filePath);
        // UUID regex pattern
        string uuidPattern = @"\b[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}\b";
        string modifiedContent = Regex.Replace(content, uuidPattern, new MatchEvaluator(TransformUUID));

        File.WriteAllText(filePath, modifiedContent);
        Console.WriteLine($"Processed {filePath}");
    }

    private static string TransformUUID(Match match)
    {
        char[] uuidChars = match.Value.ToCharArray();
        for (int i = 0; i < uuidChars.Length; i++)
        {
            if (char.IsDigit(uuidChars[i]))
            {
                int digit = (int)char.GetNumericValue(uuidChars[i]);
                // Wrap around after 9 to 'a'
                uuidChars[i] = digit == 9 ? 'a' : (char)('0' + digit + 1);
            }
            else if (char.IsLetter(uuidChars[i]))
            {
                // Shift letters by one; wrap around z to a, Z to A
                if (uuidChars[i] == 'z') uuidChars[i] = 'a';
                else if (uuidChars[i] == 'Z') uuidChars[i] = 'A';
                else uuidChars[i] = (char)(uuidChars[i] + 1);
            }
        }

        return new string(uuidChars);
    }
}
