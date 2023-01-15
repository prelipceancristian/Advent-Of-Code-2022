namespace Advent_Of_Code_2022.Problems
{
    public class Problem11 : IProblem
    {

        public class Monkey
        {
            public List<double> Inventory = new();
            public string Operator { get; set; }
            public string SecondTerm { get; set; }
            public int DivisionTest { get; set; }
            public int FirstMonkeyIndex { get; set; }
            public int SecondMonkeyIndex { get; set; }

            public int InspectedItems = 0;

            public static List<Monkey> Monkeys = new();

            private static int MonkeyLCM = 0;


            public void InspectItem(int itemIndex)
            {
                InspectedItems++;
                Inventory[itemIndex] = Inventory[itemIndex] % MonkeyLCM;
                var secondTerm = SecondTerm == "old" ? Inventory[itemIndex] : double.Parse(SecondTerm);
                switch(Operator) {
                    case "+":
                        Inventory[itemIndex] = Inventory[itemIndex] + secondTerm;
                        break;
                    case "-":
                        Inventory[itemIndex] = Inventory[itemIndex] - secondTerm;
                        break;
                    case "*":
                        Inventory[itemIndex] = Inventory[itemIndex] * secondTerm;
                        break;
                    case "/":
                        Inventory[itemIndex] = Inventory[itemIndex] / secondTerm;
                        break;
                }
            }

            public void ThrowItem(int i)
            {
                if (Inventory[i] % DivisionTest == 0)
                {
                    Monkeys[FirstMonkeyIndex].Inventory.Add(Inventory[i]);
                }
                else
                {
                    Monkeys[SecondMonkeyIndex].Inventory.Add(Inventory[i]);
                }
            }

            public void MakeTurn()
            {
                for(int i = 0; i < Inventory.Count; i++)
                {
                    InspectItem(i);
                    //Inventory[i] /= 3;
                    ThrowItem(i);
                }
                Inventory.Clear();
            }

            public static void CalculateLCMMonkeys()
            {
                MonkeyLCM = Monkeys.Select(x => x.DivisionTest).Aggregate((S, val) => S * val / GCD(S, val));
            }
        }


        public Problem11()
        {
            Day = 11;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public void SolveProblem1()
        {
            //read data, obtain monkey list
            while(Lines.Any())
            {
                Monkey monkey = new();
                var monkeyLines = Lines.Take(7).ToList();
                var items = monkeyLines[1].Split(':')[1].Split(",");
                monkey.Inventory.AddRange(items.AsEnumerable().Select(x => double.Parse(x)));
                monkey.Operator = monkeyLines[2].Trim().Split(' ')[4];
                monkey.DivisionTest = int.Parse(monkeyLines[3].Split(' ').ToList().Last());
                monkey.FirstMonkeyIndex = int.Parse(monkeyLines[4].Split(' ').ToList().Last());
                monkey.SecondMonkeyIndex = int.Parse(monkeyLines[5].Split(' ').ToList().Last());
                monkey.SecondTerm = monkeyLines[2].Trim().Split(' ')[5];
                Monkey.Monkeys.Add(monkey);

                Lines = Lines.Skip(7).ToList();
            }
            Monkey.CalculateLCMMonkeys();
            for (int round = 0; round < 10000; round++)
            {
                RunRound();
            }
            var bestMonkeys = Monkey.Monkeys.OrderByDescending(m => m.InspectedItems).Take(2).ToList();
            var result = bestMonkeys[0].InspectedItems * bestMonkeys[1].InspectedItems;
            Console.WriteLine(result.ToString());
        }

        private static void RunRound()
        {
            for (int index = 0; index < Monkey.Monkeys.Count; index++)
            {
                Monkey.Monkeys[index].MakeTurn();
            }
        }

        public void SolveProblem2()
        {
            throw new NotImplementedException();
        }

        private static int GCD(int a, int b)
        {
            if(b == 0)
            {
                return a;
            }
            return GCD(b, a % b);
        }
    }
}
