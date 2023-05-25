using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DepthFirstSolve
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
        public Node(Point pp)
        {
            p = pp;
            children = new List<Node>();
        }
        public override bool Equals(object o)
        {
            var other = o as Node;
            if (other == null)
            {
                return false;
            } else
            {
                return (other.p.X == p.X && other.p.Y == p.Y);
            }
        }
        public override string ToString()
        {
            string ret = p.ToString() + " Par: " + (parent == null ? "None" : parent.p.ToString());
            ret += " Children: [";
            foreach(Node np in children)
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
            if(lookup.ContainsKey(key))
            {
                ret = lookup[key];
            } else
            {
                Console.WriteLine("Warning: Got null node");
            }
            return ret;
        }
        public Node Add(Point p, Node parent)
        {
            Node ret = null;
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
            foreach(Node c in n.children)
            {
                PrintHelper(c, n, indent + "\t");
            }
        }
    }

    public class QueueObject
    {
        public Point cur;
        public Node prev;
        public QueueObject(Point curr, Node prevv)
        {
            cur = curr;
            prev = prevv;
        }
    }


    internal class DepthFirstSolveMain
    {

        static void PrintMaze(int[][] maze)
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

        void PrintSeen(HashSet<int> seen)
        {
            Console.Write("[ ");
            foreach (int p in seen)
            {
                //i = p.y*w + p.x
                int x = p % 3;
                int y = (p - x) / 3;
                Console.Write("(" + x.ToString() + "," + y.ToString() + ") ");
            }
            Console.WriteLine("] ");
        }

        static bool OutOfBounds(Point p, int[][] maze)
        {
            return p.X < 0 || p.X >= maze.Length || p.Y < 0 || p.Y >= maze[0].Length;
        }



        static void Main(string[] args)
        {
            int mazeWidth = 100;
            int mazeHeight = 100;
            int[][] maze = new int[mazeWidth][];
            for (int i = 0; i < mazeWidth; ++i)
            {
                maze[i] = new int[mazeHeight];
            }


            //int[][] maze = new int[6][];
            //maze[0] = new int[7] { 0, 0, 0, 0, 0, 1, 1 };
            //maze[1] = new int[7] { 1, 0, 1, 1, 0, 1, 1 };
            //maze[2] = new int[7] { 1, 0, 1, 1, 0, 0, 0 };
            //maze[3] = new int[7] { 1, 0, 0, 1, 0, 0, 0 };
            //maze[4] = new int[7] { 1, 1, 0, 1, 0, 0, 0 };
            //maze[5] = new int[7] { 1, 1, 0, 1, 1, 1, 0 };

            //int mazeWidth = maze.Length;
            //int mazeHeight = maze[0].Length;

            PrintMaze(maze);

            //make separate maze with weights?

            List<Point> offsetList = new List<Point> {
                    new Point(-1, 0), new Point(1, 0),
                    new Point(0, -1), new Point(0, 1)
                };


            Node n = new Node(new Point(0, 0));
            Console.WriteLine(n);

            Tree t = new Tree(mazeWidth);


            //want to add points into tree
            //if point in tree, skip
            //need to keep track of previous point
            Node prev = null;
            Point cur = new Point(0, 0);
            Queue<QueueObject> queue = new Queue<QueueObject>();
            queue.Enqueue(new QueueObject(cur, prev));
            
            while(queue.Count > 0)
            {
                QueueObject q = queue.Dequeue();
                Point curPoint = q.cur;
                Node prevNode = q.prev;
                if(!t.Contains(curPoint))
                {
                    Node curNode = t.Add(curPoint, prevNode);
                    foreach(Point off in offsetList)
                    {
                        Point next = new Point(curPoint.X+off.X, curPoint.Y+off.Y);
                        if(!OutOfBounds(next, maze))
                        {
                            if (maze[next.X][next.Y] != 1)
                            {
                                queue.Enqueue(new QueueObject(next, curNode));
                            }
                        }
                    }
                } 
                //else
                //{
                //    prevNode.children.Add(lookup[pointInt]);
                //}
            }
            Console.WriteLine("Done");
            t.Print();

            Point end = new Point(mazeWidth - 1, mazeHeight - 1);
            if (t.Contains(end))
            {
                HashSet<int> path = new HashSet<int>();
                Node iter = t.GetNode(end);
                path.Add(t.PointToInt(iter.p));
                while(iter != t.root)
                {
                    Console.WriteLine(iter.p);
                    iter = iter.parent;
                    path.Add(t.PointToInt(iter.p));
                    if(iter == null)
                    {
                        break;
                    }
                }

                for (int i = 0; i < maze.Length; ++i)
                {
                    for (int j = 0; j < maze[i].Length; ++j)
                    {
                        //todo: fix this
                        string val = maze[i][j].ToString();
                        if (path.Contains(t.PointToInt(new Point(i, j))))
                        {
                            val = "x";
                        }
                        Console.Write(val + ",");
                    }
                    Console.WriteLine();
                }

            }


            int cnt = 0;
            Console.WriteLine("Done " + cnt.ToString());
            string aaa = Console.ReadLine();
        }
    }
}
