using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem10 : IProblem
    {
        public Problem10()
        {
            Day = 10;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        class Command
        {
            public Command(string line)
            {
                var args = line.Split(' ');
                if (args.Length == 1)
                {
                    NoOfCycles = 1;
                    Value = 0;
                }
                else
                {
                    NoOfCycles = 2;
                    Value = int.Parse(args[1]);
                }
            }

            public int NoOfCycles { get; set; }
            public int Value { get; set; }

            public int ExecuteCommand(int registerValue)
            {
                return registerValue + Value;
            }
        }

        class CPU
        {
            public CPU()
            {
                Register = 1;
                Command = null;
                RemainingCycles = 0;
            }

            public int Register { get; set; }
            public Command Command { get; set; }
            public int RemainingCycles { get; set; }

            public void SetCommand(Command c)
            {
                Command = c;
                RemainingCycles = c.NoOfCycles;
            }

            public void ExecuteCurrentCommandCycle()
            {
                RemainingCycles--;
                if (RemainingCycles == 0)
                {
                    Register = Command.ExecuteCommand(Register);
                    Command = null;
                }
            }

            public char GetPixel(int currentCycle)
            {
                //when cycle is 40, treat it as the last cycle in the first row
                var crtPosition = (currentCycle - 1) % 40;
                if (Register == crtPosition)
                    return '#';
                if (Register - 1 >= 0 && (Register - 1) == crtPosition)
                    return '#';
                if (Register + 1 <= 39 && (Register + 1) == crtPosition)
                    return '#';
                return '.';
            }
        }

        public void SolveProblem1()
        {
            var cpu = new CPU();
            var commandList = Lines.Select(line => new Command(line)).ToList();
            var cycle = 1;
            var signal = 0;
            while (commandList.Any() || cpu.Command != null)
            {
                // add command and "start executing"
                if (cpu.Command == null)
                {
                    cpu.SetCommand(commandList.First());
                    commandList = commandList.Skip(1).ToList();
                }

                // during execution of the current cycle
                if (cycle % 40 == 20)
                {
                    signal += cycle * cpu.Register;
                }

                //get resolution after cycle
                cpu.ExecuteCurrentCommandCycle();
                cycle++;
            }
        }

        public void SolveProblem2()
        {
            var cpu = new CPU();
            var commandList = Lines.Select(line => new Command(line)).ToList();
            var cycle = 0;
            var signal = 0;
            while (commandList.Any() || cpu.Command != null)
            {
                cycle++;
                // add command and "start executing"
                if (cpu.Command == null)
                {
                    cpu.SetCommand(commandList.First());
                    commandList = commandList.Skip(1).ToList();
                }

                // during execution of the current cycle
                Console.Write(cpu.GetPixel(cycle));
                if (cycle % 40 == 0)
                {
                    Console.WriteLine();
                }

                //get resolution after cycle
                cpu.ExecuteCurrentCommandCycle();
            }
        }
    }
}
