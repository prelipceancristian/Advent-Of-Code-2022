namespace Advent_Of_Code_2022.Problems
{
    internal class Problem4 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        class Assignment
        {
            public int StartValue { get; set; }
            public int EndValue { get; set; }
            public Assignment(string interval)
            {
                var args = interval.Split('-');
                StartValue = int.Parse(args[0]);
                EndValue = int.Parse(args[1]);
            }

            public Assignment()
            {
            }

            public bool IsContained(Assignment assignment)
            {
                //completely contained
                return assignment.StartValue <= StartValue && assignment.EndValue >= EndValue;
            }

            public bool IsOverlapped(Assignment assignment)
            {
                // overlaps "to the left" if the end of the given assignment is contained somewhere
                // in the original assignment
                return assignment.EndValue >= StartValue && assignment.EndValue <= EndValue;
            }
        }

        public Problem4()
        {
            Day = 4;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            //wanna be one liner lol
            var count = Lines.Count(line => 
                new Assignment(line.Split(',')[0]).IsContained(new Assignment(line.Split(',')[1])) ||
                new Assignment(line.Split(',')[1]).IsContained(new Assignment(line.Split(',')[0])));
            Console.WriteLine(count);
        }

        public void SolveProblem2()
        {
            var count = Lines.Count(line =>
                new Assignment(line.Split(',')[0]).IsOverlapped(new Assignment(line.Split(',')[1])) ||
                new Assignment(line.Split(',')[1]).IsOverlapped(new Assignment(line.Split(',')[0])));
            Console.WriteLine(count);
        }
    }
}
