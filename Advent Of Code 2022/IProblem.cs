namespace Advent_Of_Code_2022
{
    internal interface IProblem
    {
        int Day { get; set; }
        public List<string> Lines { get; set; }
        void SolveProblem1();
        void SolveProblem2();
    }
}
