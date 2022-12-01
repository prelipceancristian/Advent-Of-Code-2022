namespace Advent_Of_Code_2022.Problems
{
    internal class Problem1 : IProblem
    {
        public int Day { get; set; }
        public List<string> lines { get; set; }

        public Problem1()
        {
            Day = 1;
            lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            var totalSum = 0;
            var localSum = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    totalSum = totalSum < localSum ? localSum : totalSum;
                    localSum = 0;
                }
                else
                {
                    localSum += Int32.Parse(lines[i]);
                }
            }
            totalSum = totalSum < localSum ? localSum : totalSum;
            Console.WriteLine(totalSum);
        }

        public void SolveProblem2()
        {
            var maxSums = new List<int>() { 0, 0, 0 };
            var localSum = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    UpdateMaxSums(maxSums, localSum);
                    localSum = 0;
                }
                else
                {
                    localSum += Int32.Parse(lines[i]);
                }
            }
            UpdateMaxSums(maxSums, localSum);
            Console.WriteLine(maxSums.Sum());
        }

        private static void UpdateMaxSums(List<int> maxSums, int localSum)
        {
            if (localSum > maxSums[0])
            {
                maxSums[2] = maxSums[1];
                maxSums[1] = maxSums[0];
                maxSums[0] = localSum;
            }
            else if (localSum > maxSums[1])
            {
                maxSums[2] = maxSums[1];
                maxSums[1] = localSum;
            }
            else if (localSum > maxSums[2])
            {
                maxSums[2] = localSum;
            }
        }
    }
}
