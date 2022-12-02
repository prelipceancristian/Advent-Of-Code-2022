namespace Advent_Of_Code_2022.Problems
{
    internal class Problem2 : IProblem
    {
        enum RPS
        {
            Rock,
            Paper,
            Scissors
        }

        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public Problem2()
        {
            Day = 2;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        public void SolveProblem1()
        {
            var score = 0;
            foreach(var line in Lines) 
            { 
                var choices = line.Split(' ');
                var elfChoice = GetChoice(choices[0]);
                var humanChoice = GetChoice(choices[1]);
                score += GetScore(elfChoice, humanChoice);
            }
            Console.WriteLine(score);
        }

        private RPS GetChoice(string choice)
        {
            switch (choice)
            {
                case "A": case "X":
                    return RPS.Rock;
                case "B": case "Y": 
                    return RPS.Paper;
                default: return RPS.Scissors;
            }
        }

        enum Outcome
        {
            Win = 6,
            Loss = 0,
            Draw = 3
        }

        private static int GetScore(RPS elfChoice, RPS humanChoice)
        {
            return GetChoiceScore(humanChoice) + GetOutcomeScore(elfChoice, humanChoice);
        }

        private static int GetOutcomeScore(RPS elfChoice, RPS humanChoice)
        {
            if (elfChoice == humanChoice)
            {
                return 3;
            }
            if ((humanChoice == RPS.Rock && elfChoice == RPS.Scissors) || 
                (humanChoice == RPS.Paper && elfChoice == RPS.Rock) || 
                (humanChoice == RPS.Scissors && elfChoice == RPS.Paper))
            {
                return 6;
            }
            return 0;
        }

        private static int GetChoiceScore(RPS humanChoice)
        {
            switch(humanChoice) 
            {
                case RPS.Rock: return 1;
                case RPS.Paper: return 2;
                default: return 3;
            }
        }

        public void SolveProblem2()
        {
            var score = 0;
            foreach (var line in Lines)
            {
                var choices = line.Split(' ');
                var elfChoice = GetChoice(choices[0]);
                var outcomeResult = GetOutcomeResult(choices[1]);
                score += GetScore2(elfChoice, outcomeResult);
            }
            Console.WriteLine(score);
        }

        private static Outcome GetOutcomeResult(string outcome)
        {
            switch (outcome)
            {
                case "X":
                    return Outcome.Loss;
                case "Y":
                    return Outcome.Draw;
                default:
                    return Outcome.Win;
            }
        }

        private static int GetScore2(RPS elfChoice, Outcome outcomeResult)
        {
            return (int)outcomeResult + GetChoiceScore(GetChoiceFromRound(elfChoice, outcomeResult));
        }

        private static RPS GetChoiceFromRound(RPS elfChoice, Outcome outcomeResult)
        {
            if (outcomeResult == Outcome.Draw)
            {
                return elfChoice;
            }
            if(outcomeResult == Outcome.Loss)
            {
                switch (elfChoice)
                {
                    case RPS.Rock:
                        return RPS.Scissors;
                    case RPS.Paper:
                        return RPS.Rock;
                    default:
                        return RPS.Paper;
                }
            }
            switch (elfChoice)
            {
                case RPS.Rock:
                    return RPS.Paper;
                case RPS.Paper:
                    return RPS.Scissors;
                default:
                    return RPS.Rock;
            }
        }
    }
}
