using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeDisplay
{
    internal class MazeSolver
    {
        public class Point
        {
            public int X;
            public int Y;
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
            public override string ToString()
            {
                return "(" + X.ToString() + "," + Y.ToString() + ")";
            }
        }

        public class Node
        {
            public Point p;
            public List<Node> children;
            public Node parent = null;
            public Node(Point newpoint)
            {
                p = newpoint;
                children = new List<Node>();
            }
            public override bool Equals(object o)
            {
                var other = o as Node;
                if (other == null)
                {
                    return false;
                }
                else
                {
                    return other.p.X == p.X && other.p.Y == p.Y;
                }
            }
            public override string ToString()
            {
                string ret = p.ToString() + " Par: " + (parent == null ? "None" : parent.p.ToString());
                ret += " Children: [";
                foreach (Node np in children)
                {
                    ret += np.p.ToString() + ",";
                }
                ret += "]";
                return ret;
            }
        }
        public class Tree
        {
            public Node root = null;
            public Dictionary<int, Node> lookup = new Dictionary<int, Node>();
            public int mazeWidth;
            public Tree(int w)
            {
                mazeWidth = w;
            }
            public int PointToInt(Point p)
            {
                return p.Y * mazeWidth + p.X;
            }
            public bool Contains(Point p)
            {
                return lookup.ContainsKey(PointToInt(p));
            }
            public Node GetNode(Point p)
            {
                Node ret = null;
                int key = PointToInt(p);
                if (lookup.ContainsKey(key))
                {
                    ret = lookup[key];
                }
                else
                {
                    Console.WriteLine("Warning: Got null node");
                }
                return ret;
            }
            public Node Add(Point p, Node parent)
            {
                Node ret;
                if (parent == null)
                {
                    root = new Node(p);
                    root.parent = root;
                    ret = root;
                }
                else
                {
                    Node n = new Node(p);
                    n.parent = parent;
                    parent.children.Add(n);
                    ret = n;
                }
                lookup[PointToInt(p)] = ret;
                return ret;
            }
            public void Print()
            {
                PrintHelper(root, root, "");
            }
            private void PrintHelper(Node n, Node parent, string indent)
            {
                Console.WriteLine(indent + n.p.ToString() + ":" + parent.p.ToString());
                foreach (Node c in n.children)
                {
                    PrintHelper(c, n, indent + "\t");
                }
            }
        }
        class QueueObject
        {
            public Point cur;
            public Node prev;
            public QueueObject(Point curr, Node prevv)
            {
                cur = curr;
                prev = prevv;
            }
        }

        private HashSet<int> lastSolved = new HashSet<int>();
        private Tree t;
        private int mazeWidth = 0;
        private int mazeHeight = 0;

        public MazeSolver()
        {
        }

        public void PrintMaze(int[][] maze)
        {
            for (int i = 0; i < maze.Length; ++i)
            {
                for (int j = 0; j < maze[i].Length; ++j)
                {
                    Console.Write(maze[i][j] + ",");
                }
                Console.WriteLine();
            }
        }

        private bool OutOfBounds(Point p, int[][] maze)
        {
            return p.X < 0 || p.X >= maze.Length || p.Y < 0 || p.Y >= maze[0].Length;
        }
        public bool PointInPath(int i, int j)
        {
            if(t == null)
            {
                return false;
            } else
            {
                return lastSolved.Contains(t.PointToInt(new Point(i,j)));
            }
        }

        public void Solve(int[][] maze)
        {
            if(maze.Length == 0 || maze[0].Length == 0)
            {
                return;
            }

            mazeWidth = maze.Length;
            mazeHeight = maze[0].Length;

            PrintMaze(maze);

            //Next points to check from current point
            List<Point> offsetList = new List<Point> {
                    new Point(-1, 0), new Point(1, 0),
                    new Point(0, -1), new Point(0, 1)
                };


            t = new Tree(mazeWidth);

            Node prev = null;
            Point cur = new Point(0, 0);
            Queue<QueueObject> queue = new Queue<QueueObject>();
            queue.Enqueue(new QueueObject(cur, prev));

            while (queue.Count > 0)
            {
                QueueObject q = queue.Dequeue();
                Point curPoint = q.cur;
                Node prevNode = q.prev;
                if (!t.Contains(curPoint))
                {
                    Node curNode = t.Add(curPoint, prevNode);
                    foreach (Point off in offsetList)
                    {
                        Point next = new Point(curPoint.X + off.X, curPoint.Y + off.Y);
                        if (!OutOfBounds(next, maze))
                        {
                            if (maze[next.X][next.Y] != 1)
                            {
                                queue.Enqueue(new QueueObject(next, curNode));
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Done");
            //t.Print();

            Point end = new Point(mazeWidth - 1, mazeHeight - 1);
            Console.WriteLine("End " + end.ToString());
            if (t.Contains(end))
            {
                Console.WriteLine("Solved!");
                lastSolved = new HashSet<int>();

                //Build up path from end node to root
                Node iter = t.GetNode(end);
                lastSolved.Add(t.PointToInt(iter.p));
                while (iter != t.root)
                {
                    Console.WriteLine(iter.p);
                    iter = iter.parent;
                    lastSolved.Add(t.PointToInt(iter.p));
                    if (iter == null)
                    {
                        //Sanity hit top of tree
                        break;
                    }
                }

                for (int i = 0; i < maze.Length; ++i)
                {
                    for (int j = 0; j < maze[i].Length; ++j)
                    {
                        string val = maze[i][j].ToString();
                        if (lastSolved.Contains(t.PointToInt(new Point(i, j))))
                        {
                            val = "x";
                        }
                        Console.Write(val + ",");
                    }
                    Console.WriteLine();
                }
            } else
            {
                Console.WriteLine("NOT solved");
                lastSolved.Clear();
            }
        }
    }
}
