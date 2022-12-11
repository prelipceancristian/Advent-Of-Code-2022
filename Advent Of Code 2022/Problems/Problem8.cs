using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem8 : IProblem
    {
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public Problem8()
        {
            Day = 8;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }

        private Tree[,] GetTreeArray()
        {
            Tree[,] treeArray = new Tree[Lines.Count, Lines.Count];
            for(int i = 0; i < Lines.Count; i++)
            {
                for(int j = 0; j < Lines[i].Length; j++)
                {
                    treeArray[i,j] = new Tree() { Size = (int)(Lines[i][j] - '0') };
                }
            }
            return treeArray;
        }

        class Tree
        {
            public int Size { get; set; }
            public bool IsVisibleTop { get; set; }
            public bool IsVisibleBottom { get; set; }
            public bool IsVisibleLeft { get; set; }
            public bool IsVisibleRight { get; set; }

            public int LeftScenicValue { get; set; }
            public int RightScenicValue { get; set; }
            public int TopScenicValue { get; set; }
            public int BottomScenicValue { get; set; }

            public bool IsVisible()
            {
                return IsVisibleTop || IsVisibleBottom || IsVisibleLeft || IsVisibleRight;
            }

            public int GetTotalScenicScore()
            {
                return LeftScenicValue * RightScenicValue * TopScenicValue * BottomScenicValue;
            }
        }

        private int[] InitializeArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = -1;
            }
            return array;
        }

        public void SolveProblem1()
        {
            var treeArray = GetTreeArray();
            var visibleTreesCounter = 0;
            for(int i = 0; i < treeArray.GetLength(0); i++)
            {
                var treeLeftValue = -1;
                for(int j = 0; j < treeArray.GetLength(1); j++)
                {
                    if(treeArray[i, j].Size > treeLeftValue)
                    {
                        treeLeftValue = treeArray[i, j].Size;
                        treeArray[i, j].IsVisibleLeft = true;
                    }
                }

                var treeRightValue = -1;
                for(int j = treeArray.GetLength(1) - 1; j >= 0; j--)
                {
                    if (treeArray[i, j].Size > treeRightValue)
                    {
                        treeRightValue = treeArray[i, j].Size;
                        treeArray[i, j].IsVisibleRight = true;
                    }
                }
            }

            for (int j = 0; j < treeArray.GetLength(1); j++)
            {
                var treeTopValue = -1;
                for (int i = 0; i < treeArray.GetLength(0); i++)
                {
                    if (treeArray[i, j].Size > treeTopValue)
                    {
                        treeTopValue = treeArray[i, j].Size;
                        treeArray[i, j].IsVisibleTop = true;
                    }
                }

                var treeBottomValue = -1;
                for (int i = treeArray.GetLength(0) - 1; i >= 0; i--)
                {
                    if (treeArray[i, j].Size > treeBottomValue)
                    {
                        treeBottomValue = treeArray[i, j].Size;
                        treeArray[i, j].IsVisibleBottom = true;
                    }
                }
            }
            for(int i = 0; i < treeArray.GetLength(0); i++)
            {
                for(int j = 0; j < treeArray.GetLength(1); j++)
                {
                    if (treeArray[i,j].IsVisible())
                    {
                        visibleTreesCounter++;
                    }
                }
            }
            Console.WriteLine(visibleTreesCounter);
        }

        public void SolveProblem2()
        {
            var treeArray = GetTreeArray();
            for(int i = 0; i < treeArray.GetLength(0); i++)
            {
                for(int j = 0; j < treeArray.GetLength(1); j++)
                {
                    var directionalScenicScore = 0;
                    for (int k = j - 1; k >= 0; k--)
                    {
                        directionalScenicScore++;
                        if (treeArray[i, k].Size >= treeArray[i, j].Size)
                        {
                            break;
                        }
                    }
                    treeArray[i,j].LeftScenicValue = directionalScenicScore;

                    directionalScenicScore = 0;
                    for (int k = j + 1; k < treeArray.GetLength(1); k++)
                    {
                        directionalScenicScore++;
                        if (treeArray[i, k].Size >= treeArray[i, j].Size)
                        {
                            break;
                        }
                    }
                    treeArray[i, j].RightScenicValue = directionalScenicScore;

                }
            }

            for (int j = 0; j < treeArray.GetLength(1); j++)
            {
                for (int i = 0; i < treeArray.GetLength(0); i++)
                {
                    var directionalScenicScore = 0;
                    for (int k = i - 1; k >= 0; k--)
                    {
                        directionalScenicScore++;
                        if (treeArray[k, j].Size >= treeArray[i, j].Size)
                        {
                            break;
                        }
                    }
                    treeArray[i, j].TopScenicValue = directionalScenicScore;

                    directionalScenicScore = 0;
                    for (int k = i + 1; k < treeArray.GetLength(1); k++)
                    {
                        directionalScenicScore++;
                        if (treeArray[k, j].Size >= treeArray[i, j].Size)
                        {
                            break;
                        }
                    }
                    treeArray[i, j].BottomScenicValue = directionalScenicScore;
                }
            }

            var maxScenicValue = -1;
            for (int i = 0; i < treeArray.GetLength(0); i++)
            {
                for (int j = 0; j < treeArray.GetLength(1); j++)
                {
                    if (treeArray[i, j].GetTotalScenicScore() > maxScenicValue)
                    {
                        maxScenicValue = treeArray[i, j].GetTotalScenicScore();
                    }
                }
            }
            Console.WriteLine(maxScenicValue);
        }
    }
}
