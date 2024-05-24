using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string directoryPath = ".";
        string searchPattern = "*.*";
        bool showHelp = false;

        foreach (var arg in args)
        {
            if (arg.Equals("--help", StringComparison.OrdinalIgnoreCase))
            {
                showHelp = true;
            }
            else if (arg.StartsWith("--directory=", StringComparison.OrdinalIgnoreCase))
            {
                directoryPath = arg.Substring("--directory=".Length);
            }
            else if (arg.StartsWith("--pattern=", StringComparison.OrdinalIgnoreCase))
            {
                searchPattern = arg.Substring("--pattern=".Length);
            }
        }

        if (showHelp)
        {
            ShowHelp();
            Environment.Exit(0);
        }

        try
        {
            long totalSize = CalculateTotalSize(directoryPath, searchPattern);
            Console.WriteLine($"Total size of files: {totalSize} bytes");
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            Environment.Exit(1);
        }
    }

    static long CalculateTotalSize(string directoryPath, string searchPattern)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException("Directory not found.");
        }

        long totalSize = 0;
        var files = Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            totalSize += fileInfo.Length;
        }

        return totalSize;
    }

    static void ShowHelp()
    {
        Console.WriteLine("Usage: Program [--directory=path] [--pattern=*.ext] [--help]");
        Console.WriteLine("Options:");
        Console.WriteLine("--directory=path   Specify the directory to calculate file sizes.");
        Console.WriteLine("--pattern=*.ext    Specify the file pattern to match.");
        Console.WriteLine("--help             Show this help message.");
    }
}

