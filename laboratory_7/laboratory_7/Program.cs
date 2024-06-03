using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ConsoleApp
{
    public static class Extensions
    {
        public static DateTime FindOldestDate(this DirectoryInfo directory)
        {
            DateTime oldestDate = DateTime.MaxValue;

            foreach (var file in directory.GetFiles())
            {
                if (file.CreationTime < oldestDate)
                {
                    oldestDate = file.CreationTime;
                }
            }

            foreach (var subDir in directory.GetDirectories())
            {
                DateTime subDirDate = subDir.FindOldestDate();
                if (subDirDate < oldestDate)
                {
                    oldestDate = subDirDate;
                }
            }

            return oldestDate;
        }

        public static string GetDOSAttributes(this FileSystemInfo fileSystemInfo)
        {
            var attributes = fileSystemInfo.Attributes;
            return ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly ? "r" : "-") +
                   ((attributes & FileAttributes.Archive) == FileAttributes.Archive ? "a" : "-") +
                   ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden ? "h" : "-") +
                   ((attributes & FileAttributes.System) == FileAttributes.System ? "s" : "-");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string directoryPath = args[0];
            DirectoryInfo directory = new DirectoryInfo(directoryPath);

            if (!directory.Exists)
            {
                return;
            }

            DirectoryInfo[] directories = directory.GetDirectories();
            FileInfo[] files = directory.GetFiles();


            DisplayDirectoryContents(directory, 0);
            Console.WriteLine("\nOldest creation date in directory: " + directory.FindOldestDate());

            SortedList<string, int> sortedCollection = new SortedList<string, int>(new CustomStringComparer());
            foreach (var dir in directories)
            {
                sortedCollection.Add(dir.Name, dir.GetFiles().Length);
            }
            foreach (var file in files)
            {
                sortedCollection.Add(file.Name, (int)file.Length);
            }

            SerializeCollection(sortedCollection);
            DeserializeCollection();
        }

        static void DisplayDirectoryContents(DirectoryInfo directory, int depth)
        {
            // Display directory name
            string indentation = new string(' ', depth);
            Console.WriteLine($"{indentation}{directory.Name} ({directory.GetFiles().Length})");

            // Display subdirectories
            foreach (var subDir in directory.GetDirectories())
            {
                DisplayDirectoryContents(subDir, depth + 2);
            }

            // Display files in the directory
            foreach (var file in directory.GetFiles())
            {
                string attributes = file.GetDOSAttributes();
                Console.WriteLine($"{indentation}{file.Name} {file.Length} bytes {attributes}");
            }
            
        }


        public static void SerializeCollection(SortedList<string, int> collection)
        {
            using (FileStream fs = new FileStream("collection.bin", FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, collection);
            }

            Console.WriteLine("\nCollection has been serialized.");
        }

        static void DeserializeCollection()
        {
            using (FileStream fs = new FileStream("collection.bin", FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                SortedList<string, int> collection = (SortedList<string, int>)formatter.Deserialize(fs);
                Console.WriteLine("Deserialized collection:");
                foreach (var item in collection)
                {
                    Console.WriteLine($"{item.Key} -> {item.Value}");
                }
            }
        }
    }

    [Serializable]
    class CustomStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Length == y.Length)
            {
                return string.Compare(x, y);
            }
            return x.Length.CompareTo(y.Length);
        }
    }
}
