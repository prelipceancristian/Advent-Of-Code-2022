using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem5 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        class CrateStack
        {
            public int StackId;
            public Stack<string> Crates = new();
        }

        class StackRepo
        {
            public Dictionary<int, CrateStack> CrateStacks = new();

            public void MoveCrates(Movement move)
            {
                for (var _ = 0; _ < move.MoveCount; _++)
                {
                    var movedCrate = CrateStacks[move.OriginCrateId].Crates.Pop();
                    CrateStacks[move.DestinationCrateId].Crates.Push(movedCrate);
                }
            }

            public void MoveCratesCrateMover9001(Movement move)
            {
                var stack = new Stack<string>();
                for (var _ = 0; _ < move.MoveCount; _++)
                {
                    var movedCrate = CrateStacks[move.OriginCrateId].Crates.Pop();
                    stack.Push(movedCrate);
                }
                for (var _ = 0; _ < move.MoveCount; _++)
                {
                    var movedCrate = stack.Pop();
                    CrateStacks[move.DestinationCrateId].Crates.Push(movedCrate);
                }
            }

            public void AddCrateStack(CrateStack crateStack)
            {
                CrateStacks.Add(crateStack.StackId, crateStack);
            }

            public string ReadWord()
            {
                var sb = new StringBuilder();
                foreach(var crateStack in CrateStacks.OrderBy(x => x.Key))
                {
                    sb.Append(crateStack.Value.Crates.Peek());
                }
                return sb.ToString();
            }
        }

        class Movement
        {
            public Movement(string line)
            {
                var args = line.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
                MoveCount= int.Parse(args[0]);
                OriginCrateId = int.Parse(args[1]);
                DestinationCrateId = int.Parse(args[2]);
            }

            public int MoveCount { get; set; }
            public int OriginCrateId { get; set; }
            public int DestinationCrateId { get; set; }
        }

        public Problem5()
        {
            Day = 5;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            StackRepo repo = BuildStaticRepo();
            List<Movement> movements = ReadMovements();
            foreach(var movement in movements) 
            { 
                repo.MoveCrates(movement);
            }
            Console.WriteLine(repo.ReadWord());
        }

        private List<Movement> ReadMovements()
        {
            return Lines.Skip(10).Select(line => new Movement(line.Replace("move", "").Replace("from", "").Replace("to", ""))).ToList();
        }

        public void SolveProblem2()
        {
            StackRepo repo = BuildStaticRepo();
            List<Movement> movements = ReadMovements();
            foreach (var movement in movements)
            {
                repo.MoveCratesCrateMover9001(movement);
            }
            Console.WriteLine(repo.ReadWord());
        }

        private static StackRepo BuildStaticRepo()
        {
            var repo = new StackRepo();
            repo.AddCrateStack(new CrateStack { StackId = 1, Crates = new Stack<string>(new[] { "P", "L", "M", "N", "W", "V", "B", "H" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 2, Crates = new Stack<string>(new[] { "H", "Q", "M" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 3, Crates = new Stack<string>(new[] { "L", "M", "Q", "F", "G", "B", "D", "N" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 4, Crates = new Stack<string>(new[] { "G", "W", "M", "Q", "F", "T", "Z" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 5, Crates = new Stack<string>(new[] { "P", "H", "T", "M" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 6, Crates = new Stack<string>(new[] { "T", "G", "H", "D", "J", "M", "B", "C" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 7, Crates = new Stack<string>(new[] { "R", "V", "F", "B", "N", "M" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 8, Crates = new Stack<string>(new[] { "S", "G", "R", "M", "H", "L", "P" }.Reverse()) });
            repo.AddCrateStack(new CrateStack { StackId = 9, Crates = new Stack<string>(new[] { "N", "C", "B", "D", "P" }.Reverse()) });
            return repo;
        }
    }
}
