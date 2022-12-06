namespace Advent_Of_Code_2022.Problems
{
    internal class Problem6 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public static int SignalLength = 4;

        public Problem6()
        {
            Day = 6;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            var signal = Lines[0];
            var index = 0;
            while(true)
            {
                var txt = signal.Substring(index, SignalLength);
                if(HasUniqueCharacters(txt))
                {
                    break;
                }
                index++;
            }
            Console.WriteLine(index + SignalLength);
        }

        private static bool HasUniqueCharacters(string s)
        {
            int bitChecker = 0;
            foreach(char ch in s)
            {
                int bitAtIndex = ch - 'a';
                if((bitChecker & (1 << bitAtIndex)) > 0)
                {
                    return false;
                }
                bitChecker |= (1 << bitAtIndex);
            }
            return true;
        }

        public void SolveProblem2()
        {
            //lol
            SignalLength = 14;
            SolveProblem1();
        }
    }
}
