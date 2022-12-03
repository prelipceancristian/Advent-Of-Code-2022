using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem3 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public Problem3()
        {
            Day = 3;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            List<char> commonItems = new();
            foreach (var line in Lines)
            {
                var firstContents = line.Substring(0, line.Length / 2);
                var secondContents = line.Substring(line.Length / 2);
                List<char> localCommonItems = new();
                foreach (var item in firstContents)
                {
                    if (secondContents.Contains(item) && !localCommonItems.Contains(item))
                    {
                        localCommonItems.Add(item);
                        commonItems.Add(item);
                    }
                }
            }
            Console.WriteLine(commonItems.Sum(ch => GetItemPriority(ch)));
        }

        private static int GetItemPriority(char ch)
        {
            return char.IsUpper(ch) ? ch - 'A' + 27 : ch - 'a' + 1;
        }

        public void SolveProblem2()
        {
            var sum = 0;
            while (Lines.Count > 0)
            {
                var groupLines = Lines.Take(3).ToList();
                foreach(var ch in groupLines[0])
                {
                    //inefficient as hell but the data is small enough
                    if (groupLines[1].Contains(ch) && groupLines[2].Contains(ch))
                    {
                        sum += GetItemPriority(ch);
                        break;
                    }
                }
                Lines = Lines.Skip(3).ToList();
            }
            Console.WriteLine(sum);
        }
    }
}
