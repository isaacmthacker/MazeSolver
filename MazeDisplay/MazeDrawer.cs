using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeDisplay
{
    internal class MazeDrawer
    {
        float cellWidth, cellHeight;
        float halfCellWidth, halfCellHeight;
        int[][] maze;
        List<MazeCell> cells = new List<MazeCell>();
        MazeSolver mazeSolver = new MazeSolver();
        MazeSolver.Point end;
        public MazeDrawer(int w, int h)
        {

            //maze = new int[6][];
            //maze[0] = new int[7] { 0, 0, 0, 0, 0, 1, 1 };
            //maze[1] = new int[7] { 1, 0, 1, 1, 0, 1, 1 };
            //maze[2] = new int[7] { 1, 0, 1, 1, 0, 0, 0 };
            //maze[3] = new int[7] { 1, 0, 0, 1, 0, 0, 0 };
            //maze[4] = new int[7] { 1, 1, 0, 1, 0, 0, 0 };
            //maze[5] = new int[7] { 1, 1, 0, 1, 1, 1, 0 };

            maze = new int[34][];
            maze[0] = new int[18] { 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[1] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[2] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[3] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[4] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[5] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[6] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[7] = new int[18] { 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[8] = new int[18] { 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[9] = new int[18] { 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[10] = new int[18] { 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[11] = new int[18] { 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[12] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[13] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[14] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[15] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[16] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 };
            maze[17] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 };
            maze[18] = new int[18] { 0, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 };
            maze[19] = new int[18] { 0, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 };
            maze[20] = new int[18] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0 };
            maze[21] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[22] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[23] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[24] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[25] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[26] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0 };
            maze[27] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[28] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[29] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[30] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0 };
            maze[31] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[32] = new int[18] { 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            maze[33] = new int[18] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //int mazeW = 30;
            //int mazeH = 30;
            //maze = new int[mazeW][];
            //for (int i = 0; i < mazeW; ++i)
            //{
            //    maze[i] = new int[mazeH];
            //}

            //Default end point to bottom-right corner of maze
            end = new MazeSolver.Point(maze.Length - 1, maze[0].Length - 1);

            //Needs maze to be defined before calling
            Resize(w, h);
        }
        public void Resize(int w, int h)
        {
            cellWidth = (float)w / (maze[0].Length + 1);
            cellHeight = (float)h / (maze.Length + 1);

            halfCellHeight = cellHeight / 2.0f;
            halfCellWidth = cellWidth / 2.0f;

            CreateCells();
        }

        public void CreateCells()
        {
            mazeSolver.Solve(maze, end);
            cells.Clear();
            for (int i = 0; i < maze.Length; i++)
            {
                for (int j = 0; j < maze[i].Length; ++j)
                {
                    //Flip i and j so j is width and i is height
                    float y = i * cellHeight + halfCellHeight;
                    float x = j * cellWidth + halfCellWidth;
                    MazeCell m = new MazeCell(i, j, x, y, cellWidth, cellHeight);
                    if (end.X == i && end.Y == j)
                    {
                        m.End = true;
                    }
                    else
                    {
                        //Only check if not the end cell
                        if (maze[i][j] == 1)
                        {
                            m.Blocked = true;
                        }
                        if (mazeSolver.PointInPath(i, j))
                        {
                            m.Filled = true;
                        }
                    }
                    cells.Add(m);
                }
            }
        }
        public void Draw(Graphics g)
        {
            foreach (MazeCell cell in cells)
            {
                cell.Draw(g);
            }
        }
        public bool MouseLookup(MouseEventArgs e)
        {
            foreach (MazeCell cell in cells)
            {
                if (cell.Click(e.X, e.Y))
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        //Update maze end cell
                        maze[cell.I][cell.J] = 0;
                        end.X = cell.I;
                        end.Y = cell.J;
                    }
                    else
                    {
                        //Block or unblock non-end/non-beginning cell
                        if (maze[cell.I][cell.J] == 0)
                        {
                            if (!(cell.I == 0 && cell.J == 0))
                            {
                                if (!(end.X == cell.I && end.Y == cell.J))
                                {
                                    //Only block if not the end cell or beginning
                                    maze[cell.I][cell.J] = 1;
                                }
                            }
                        }
                        else
                        {
                            maze[cell.I][cell.J] = 0;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
