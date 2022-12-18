using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem9 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public Problem9()
        {
            Day = 9;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        class RopeKnot
        {
            public bool IsHead { get; set; }
            public bool IsTail { get; set; }
            public KnotPosition Position { get; set; }
            public RopeKnot? PreviousKnot { get; set; }

            internal void Move(string direction)
            {
                switch (direction)
                {
                    case "L":
                        Position.XAxis--;
                        break;
                    case "R":
                        Position.XAxis++;
                        break;
                    case "D":
                        Position.YAxis--;
                        break;
                    case "U":
                        Position.YAxis++;
                        break;
                    default:
                        break;
                }
            }

            internal bool IsTooFar()
            {
                if (PreviousKnot == null)
                {
                    return false;
                }
                return IsTwoUnitsAway();
            }

            private bool IsTwoUnitsAway()
            {
                return Math.Abs(Position.XAxis - PreviousKnot.Position.XAxis) == 2 ||
                    Math.Abs(Position.YAxis - PreviousKnot.Position.YAxis) == 2;
            }

            private bool IsSameAxis()
            {
                return Position.XAxis == PreviousKnot.Position.XAxis ||
                    Position.YAxis == PreviousKnot.Position.YAxis;
            }

            internal void PullPreviousKnot()
            {
                if (IsSameAxis())
                {
                    MoveSameAxis();
                }
                else
                {
                    MoveToSameAxis();
                    MoveSameAxis();
                }
            }

            private void MoveToSameAxis()
            {
                //....
                //.H..
                //....
                //T...
                //move along the axis that has only one difference
                if(Math.Abs(Position.XAxis - PreviousKnot.Position.XAxis) == 1)
                {
                    MoveAlongXAxis();
                }
                else
                {
                    MoveAlongYAxis();
                }
            }

            private void MoveAlongXAxis()
            {
                PreviousKnot.Position.XAxis = Position.XAxis > PreviousKnot.Position.XAxis ?
                    PreviousKnot.Position.XAxis + 1 :
                    PreviousKnot.Position.XAxis - 1;
            }

            private void MoveAlongYAxis()
            {
                PreviousKnot.Position.YAxis = Position.YAxis > PreviousKnot.Position.YAxis ?
                    PreviousKnot.Position.YAxis + 1 :
                    PreviousKnot.Position.YAxis - 1;
            }

            private void MoveSameAxis()
            {
                if (Position.XAxis == PreviousKnot.Position.XAxis)
                {
                    //...T
                    //O...
                    //...H
                    //move along y axis

                    MoveAlongYAxis();
                }
                else
                {
                    MoveAlongXAxis();
                }
            }
        }

        class KnotPosition
        {
            public override bool Equals(object other)
            {
                if (other is not KnotPosition otherKnotPosition) return false;
                return otherKnotPosition.XAxis == XAxis &&
                    otherKnotPosition.YAxis == YAxis;
            }

            public KnotPosition()
            {
                XAxis = 0;
                YAxis = 0;
            }

            public KnotPosition(KnotPosition other)
            {
                XAxis = other.XAxis;
                YAxis = other.YAxis;
            }

            public int XAxis { get; set; }
            public int YAxis { get; set; }
        }

        private List<KnotPosition> SimulateMovement(RopeKnot head, RopeKnot tail)
        {
            var visitedPositions = new List<KnotPosition>
            {
                new KnotPosition()
            };
            foreach (var line in Lines)
            {
                var args = line.Split(' ');
                var direction = args[0];
                var noOfMoves = int.Parse(args[1]);
                while (noOfMoves > 0)
                {
                    var currentKnot = head;
                    currentKnot.Move(direction);
                    while (currentKnot.IsTooFar())
                    {
                        currentKnot.PullPreviousKnot();
                        currentKnot = currentKnot.PreviousKnot;
                    }
                    if (!visitedPositions.Contains(tail.Position))
                    {
                        visitedPositions.Add(new KnotPosition(tail.Position));
                    }
                    noOfMoves--;
                }
            }

            return visitedPositions;
        }

        private static (RopeKnot Head, RopeKnot Tail) GetRopeSetupProblem1()
        {
            RopeKnot head = new() { IsHead = true, Position = new KnotPosition() };
            RopeKnot tail = new() { IsTail = true, Position = new KnotPosition() };
            head.PreviousKnot = tail;
            return (head, tail);
        }

        private static (RopeKnot Head, RopeKnot Tail) GetRopeSetupProblem2()
        {
            RopeKnot head = new() { IsHead = true, Position = new KnotPosition() };
            RopeKnot knot1 = new() { Position = new KnotPosition() };
            RopeKnot knot2 = new() { Position = new KnotPosition() };
            RopeKnot knot3 = new() { Position = new KnotPosition() };
            RopeKnot knot4 = new() { Position = new KnotPosition() };
            RopeKnot knot5 = new() { Position = new KnotPosition() };
            RopeKnot knot6 = new() { Position = new KnotPosition() };
            RopeKnot knot7 = new() { Position = new KnotPosition() };
            RopeKnot knot8 = new() { Position = new KnotPosition() };
            RopeKnot knot9 = new() { IsTail = true, Position = new KnotPosition() };
            head.PreviousKnot = knot1;
            knot1.PreviousKnot = knot2;
            knot2.PreviousKnot = knot3;
            knot3.PreviousKnot = knot4;
            knot4.PreviousKnot = knot5;
            knot5.PreviousKnot = knot6;
            knot6.PreviousKnot = knot7;
            knot7.PreviousKnot = knot8;
            knot8.PreviousKnot = knot9;
            return (head, knot9);
        }

        public void SolveProblem1()
        {
            (var head, var tail) = GetRopeSetupProblem1();
            var visitedPositions = SimulateMovement(head, tail);
            Console.WriteLine(visitedPositions.Count);
        }

        public void SolveProblem2()
        {
            (var head, var tail) = GetRopeSetupProblem2();
            var visitedPositions = SimulateMovement(head, tail);
            Console.WriteLine(visitedPositions.Count);
        }
    }
}
