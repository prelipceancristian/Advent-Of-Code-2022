using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem7 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public File RootElement;
        public File CurrentElement;
        public int Index = 1; // skip root dir
        private readonly string ListCommand = "ls";
        private const string UpperLevel = "..";


        internal class File
        {
            private readonly string DirectoryKeyword = "dir";

            public File() { }

            public File(string line, File parentFile)
            {
                ParentFile = parentFile;
                var args = line.Split(' ');
                if (args[0] == DirectoryKeyword)
                {
                    IsDirectory = true;
                    Size = 0;
                    Name = args[1];
                }
                else
                {
                    IsDirectory = false;
                    Size = decimal.Parse(args[0]);
                    Name = args[1];
                }
            }

            public bool IsDirectory { get; set; }
            public string Name { get; set; }
            public decimal Size { get; set; }
            public decimal TotalSize { get; set; }
            
            public List<File> Files = new();
            public File ParentFile { get; set; }

            public void SetTotalSize()
            {
                if(!IsDirectory)
                {
                    TotalSize = Size;
                }
                else
                {
                    Files.ToList().ForEach(x => x.SetTotalSize());
                    TotalSize = Files.Sum(x => x.TotalSize);
                }
            }
        }

        public Problem7()
        {
            Day = 7;
            Lines = System.IO.File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
            RootElement = new File() { IsDirectory = true, Name = "/" };
            CurrentElement = RootElement;
            Index = 1;
        }

        public decimal GetTotalSum(File file)
        {
            if(file.IsDirectory)
            {
                var subDirectorySum = file.Files.Sum(x => GetTotalSum(x));
                return subDirectorySum + (file.TotalSize < 100000 ? file.TotalSize : 0);
            }
            return 0;
        }

        public void SolveProblem1()
        {
            BuildElementTree();
            RootElement.SetTotalSize();
            Console.WriteLine(GetTotalSum(RootElement));
        }

        private void BuildElementTree()
        {
            while(Index < Lines.Count)
            {
                HandleCommand();
            }
        }

        private void HandleCommand()
        {
            var args = Lines[Index].Split(' ');
            Index++;
            if (args[1] == ListCommand)
            {
                //ls command
                while (Index < Lines.Count && Lines[Index][0] != '$')
                {
                    CurrentElement.Files.Add(new File(Lines[Index], CurrentElement));
                    Index++;
                }
            }
            else
            {
                //cd command
                CurrentElement = args[2] switch
                {
                    UpperLevel => CurrentElement.ParentFile,
                    "/" => RootElement,
                    _ => CurrentElement.Files.First(x => x.Name == args[2]),
                };
            }
        }

        private File GetDirectoryToDelete(File file, decimal minSpaceToDelete)
        {
            //assume we re working with directories only
            var subDirectories = file.Files.Where(x => x.IsDirectory);
            if(!subDirectories.Any())
            {
                return file;
            }

            var subDirectoriesFilesToDelete = subDirectories.Select(x => GetDirectoryToDelete(x, minSpaceToDelete));
            var result = file;
            foreach(var subDir in subDirectoriesFilesToDelete)
            {
                if(subDir.TotalSize >= minSpaceToDelete && subDir.TotalSize < result.TotalSize)
                {
                    result = subDir;
                }
            }
            return result;
        }

        public void SolveProblem2()
        {
            decimal TotalAvailableSpace = 70000000;
            decimal NecessarySpace = 30000000;
            var minimumSpaceToDelete = NecessarySpace - (TotalAvailableSpace - RootElement.TotalSize); // necessary - already free

            BuildElementTree();
            RootElement.SetTotalSize();
            var fileToDelete = GetDirectoryToDelete(RootElement, minimumSpaceToDelete);
            Console.WriteLine(fileToDelete.TotalSize);
        }
    }
}
