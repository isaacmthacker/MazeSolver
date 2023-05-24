using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class DepthFirstSolveMain
    {

        int PointToInt(Point p, int w)
        {
            return p.Y * w + p.X;
        }

        void PrintMaze(int[][] maze)
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

        void PrintMazePath(int[][] maze, HashSet<int> seen)
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





        static void Main(string[] args)
        {
        }
    }
}
