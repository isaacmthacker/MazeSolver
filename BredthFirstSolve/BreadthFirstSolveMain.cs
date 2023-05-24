using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace BreadthFirstSolve
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
            return X.ToString() + "," + Y.ToString();
        }
    }

    public class MazePath
    {
        public Point curLocation;
        public HashSet<int> seen;
        public MazePath(Point p)
        {
            curLocation = p;
            seen = new HashSet<int>();
        }
    }

    internal class BreadthFirstSolveMain
    {
        public static int PointToInt(Point p, int w)
        {
            return p.Y * w + p.X;
        }
        public static void PrintMaze(int[][] maze)
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

        public static void PrintMazePath(int[][] maze, HashSet<int> seen)
        {
            for (int i = 0; i < maze.Length; ++i)
            {
                for (int j = 0; j < maze[i].Length; ++j)
                {
                    //todo: fix this
                    string val = maze[i][j].ToString();
                    if (seen.Contains(PointToInt(new Point(i, j), maze.Length)))
                    {
                        val = "x";
                    }
                    Console.Write(val + ",");
                }
                Console.WriteLine();
            }
        }

        public static void PrintSeen(HashSet<int> seen)
        {
            Console.Write("[ ");
            foreach (int p in seen)
            {
                //i = p.y*w + p.x
                int x = p % 3;
                int y = (p - x)/ 3;
                Console.Write("(" + x.ToString() + "," + y.ToString() + ") ");       
            }
            Console.WriteLine("] ");
        }

        public static bool Solve(int[][] maze)
        {
            int mazeWidth = maze.Length;

            //x +/- 1, y
            //x, y +/-1
            List<Point> offsetList = new List<Point> {
                    new Point(-1, 0), new Point(1, 0),
                    new Point(0, -1), new Point(0, 1)
                };

            //Assume start is at 0,0 and end is at xlen-1,ylen-1
            Point End = new Point(maze.Length - 1, maze[0].Length - 1);
            Queue<MazePath> queue = new Queue<MazePath>();
            MazePath starting = new MazePath(new Point(0, 0));
            starting.seen.Add(PointToInt(starting.curLocation, mazeWidth));

            queue.Enqueue(starting);
            Console.WriteLine(queue.Count);
            while (queue.Count > 0)
            {
                MazePath cur = queue.Dequeue();
                Point loc = cur.curLocation;

                //Console.WriteLine("Point: " + loc.ToString());
                //PrintSeen(cur.seen);

                if (loc.X == End.X && loc.Y == End.Y)
                {
                    Console.WriteLine("Solved!");
                    PrintSeen(cur.seen);
                    PrintMazePath(maze, cur.seen);
                    return true;
                }

                int x = loc.X;
                int y = loc.Y;

                foreach (Point pointOffset in offsetList)
                {
                    Point p = new Point(x + pointOffset.X, y + pointOffset.Y);
                    if (0 <= p.X && p.X < maze.Length && 0 <= p.Y && p.Y < maze[0].Length)
                    {
                        if (maze[p.X][p.Y] != 1)
                        {
                            int pointInt = PointToInt(p, maze.Length);
                            if (!cur.seen.Contains(pointInt))
                            {
                                //Console.WriteLine("Adding");
                                MazePath mp = new MazePath(p);
                                HashSet<int> seen = new HashSet<int>(cur.seen);
                                seen.Add(pointInt);
                                mp.seen = seen;
                                queue.Enqueue(mp);
                            }
                        }
                    }
                }
            }
            return false;
        }


        static void Main(string[] args)
        {

            Console.WriteLine("Start");

            //int mazeWidth = 3;
            //int mazeHeight = 3;
            //int[][] maze = new int[mazeWidth][];
            //for (int i = 0; i < mazeWidth; ++i)
            //{
            //    maze[i] = new int[mazeHeight];
            //}


            int[][] maze = new int[6][];
            maze[0] = new int[7] { 0, 0, 0, 0, 0, 1, 1 };
            maze[1] = new int[7] { 1, 0, 1, 1, 0, 1, 1 };
            maze[2] = new int[7] { 1, 0, 1, 1, 0, 0, 0 };
            maze[3] = new int[7] { 1, 0, 0, 1, 0, 0, 0 };
            maze[4] = new int[7] { 1, 1, 0, 1, 0, 0, 0 };
            maze[5] = new int[7] { 1, 1, 0, 1, 1, 1, 0 };



            PrintMaze(maze);
            bool solved = Solve(maze);
            
            Console.WriteLine("Done " + solved.ToString());
            string a = Console.ReadLine();
        }
    }

}