﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

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

        string backupDirPath = Path.Combine(Directory.GetCurrentDirectory(),"Backups");
        if (!Directory.Exists(backupDirPath))
        {
            Directory.CreateDirectory(backupDirPath);
        }

        string tempDirPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        string zipFilePath = Path.Combine(backupDirPath, "UUIDbackup.zip");
        CopyDirectory(path, tempDirPath);
        ZipFile.CreateFromDirectory(tempDirPath, zipFilePath);
        Console.WriteLine($"Backup created at {zipFilePath}");
        
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
                bool fileModified = false;

                foreach (Match match in matches)
                {
                    string originalUUID = match.Value;
                    string transposedUUID = TransposeUUID(originalUUID);
                    Console.WriteLine($"{match.Value} => {transposedUUID}");
                    content = content.Replace(originalUUID, transposedUUID);
                    fileModified = true;
                    totalCount++;
                }
                if (fileModified)
                {
                    File.WriteAllText(file, content);
                }
            }
            Console.WriteLine($"Total UUIDs found: {totalCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            if (Directory.Exists(tempDirPath))
            {
                Directory.Delete(tempDirPath, true);
            }
        }
    }
    static string TransposeUUID(string uuid)
    {
        char TransposeHexChar(char c)
        {
            if (c >= '0' && c < '9') return (char)(c + 1);
            if (c == '9') return 'a';
            if (c >= 'a' && c < 'f') return (char)(c + 1);
            if (c == 'f') return '0';
            return c; // Non-hex characters remain unchanged, though valid UUIDs shouldn't have any.
        }

        return new string(uuid.Select(TransposeHexChar).ToArray());
    }

static void CopyDirectory(string sourceDir, string destinationDir)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDir);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException($"Source directory does not exist or could not be found: {sourceDir}");
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        Directory.CreateDirectory(destinationDir);

        foreach (FileInfo file in dir.GetFiles())
        {
            string tempPath = Path.Combine(destinationDir, file.Name);
            file.CopyTo(tempPath, false);
        }

        foreach (DirectoryInfo subdir in dirs)
        {
            string tempPath = Path.Combine(destinationDir, subdir.Name);
            CopyDirectory(subdir.FullName, tempPath);
        }
    }
}