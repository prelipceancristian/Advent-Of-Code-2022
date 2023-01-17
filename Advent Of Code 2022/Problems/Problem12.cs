using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Problems
{
    internal class Problem12 : IProblem
    {
        public class Node
        {
            public Node() 
            {
                Children= new List<Node>();
            }

            public int Value { get; set; }
            public List<Node> Children { get; set; }
            public bool IsEndNode { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsVisited { get; set; }
            public char Name { get; set; }
            public int TraversalOrder { get; set; }

            public int GetDistance(Node node)
            {
                return Math.Abs(X- node.X) + Math.Abs(Y - node.Y);
            }

            public override string ToString()
            {
                return $"({X}, {Y}) - {Name}";
            }
        }


        public Problem12()
        {
            Day = 12;
            Lines = File.ReadAllLines($"..\\..\\..\\input{Day}.txt").ToList();
        }
        public int Day { get; set; }
        public List<string> Lines { get; set; }

        public void SolveProblem1()
        {
            Node root = GetNodesGraph();
            BFS(root);
        }

        private Node GetNodesGraph()
        {
            var nodes = GetInputNodes();
            foreach(var node in nodes)
            {
                foreach(var adjecentNode in GetAdjecentNodes(nodes, node))
                {
                    if(adjecentNode.Value - node.Value <= 1)
                    {
                        node.Children.Add(adjecentNode);
                    }
                }
            }
            return nodes.First(x => x.Name == 'S');
        }

        private IEnumerable<Node> GetAdjecentNodes(List<Node> nodes, Node node)
        {
            return nodes.Where(other => other.GetDistance(node) == 1);
        }

        private List<Node> GetInputNodes(bool isPart2 = false)
        {
            List<Node> nodes = new List<Node>();
            for(int i = 0; i < Lines.Count; i++)
            {
                for(int j = 0; j < Lines[0].Length; j++)
                {
                    int value;
                    if (Lines[i][j] == 'S')
                    {
                        value = 0;
                    }
                    else if (Lines[i][j] == 'E')
                    {
                        value = 'z' - 'a';
                    }
                    else
                    {
                        value = Lines[i][j] - 'a';
                    }
                    nodes.Add(new Node 
                    { 
                        Value = value, 
                        IsEndNode = isPart2 ? 
                            IsEndNode2(Lines[i][j]) : 
                            IsEndNode1(Lines[i][j]), 
                        X = i, 
                        Y = j, 
                        Name = Lines[i][j] 
                    });
                }
            }
            return nodes;
        }

        private bool IsEndNode1(char name)
        {
            return name == 'E';
        }

        private bool IsEndNode2(char name) 
        {
            return name == 'a' || name == 'S';
        }


        private void BFS(Node root)
        {
            var queue = new Queue<Node>();
            root.IsVisited = true;
            root.TraversalOrder = 0;
            queue.Enqueue(root);
            while(queue.Any())
            {
                var node = queue.Dequeue();
                Console.WriteLine(node.ToString());
                foreach(var child in node.Children)
                {
                    if(child.IsEndNode)
                    {
                        Console.WriteLine(node.TraversalOrder + 1);
                        break;
                    }
                    if(!child.IsVisited)
                    {
                        child.TraversalOrder = node.TraversalOrder + 1;
                        child.IsVisited = true;
                        queue.Enqueue(child);
                    }
                }
                if(node.Children.Any(x => x.IsEndNode))
                {
                    break;
                }
            }
        }


        public void SolveProblem2()
        {
            Node root = GetNodesGraph2();
            BFS(root);
        }

        private Node GetNodesGraph2()
        {
            var nodes = GetInputNodes(true);
            foreach (var node in nodes)
            {
                foreach (var adjecentNode in GetAdjecentNodes(nodes, node))
                {
                    if (node.Value - adjecentNode.Value <= 1)
                    {
                        node.Children.Add(adjecentNode);
                    }
                }
            }
            return nodes.First(x => x.Name == 'E');
        }
    }
}
