using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeDisplay
{
    internal class MazeDrawer
    {
        int width, height;
        //float cellWidth, cellHeight;
        int[][] maze;
        public MazeDrawer(int w, int h) {
            width = w;
            height = h;

            maze = new int[6][];
            maze[0] = new int[7] { 0, 0, 0, 0, 0, 1, 1 };
            maze[1] = new int[7] { 1, 0, 1, 1, 0, 1, 1 };
            maze[2] = new int[7] { 1, 0, 1, 1, 0, 0, 0 };
            maze[3] = new int[7] { 1, 0, 0, 1, 0, 0, 0 };
            maze[4] = new int[7] { 1, 1, 0, 1, 0, 0, 0 };
            maze[5] = new int[7] { 1, 1, 0, 1, 1, 1, 0 };

            //maze = new int[20][];
            //for(int i = 0; i < 20; ++i)
            //{
            //    maze[i] = new int[20];
            //}
        }
        public void Resize(int w, int h)
        {
            width = w;
            height = h;
        }
        public void Draw(Graphics g)
        {
            //...
            //0,0       0,1     0,2
            //1         2       3
            //1,0       1,1     1,2
            //4,        5,      6
            //2,0       2,1     2,2
            //7,        8,      9
            for(int i = 0; i < maze.Length; i++)
            {
                for(int j = 0; j < maze[i].Length; ++j)
                {
                    float cellWidth = width / (maze.Length+1);
                    float cellHeight = height / (maze[0].Length+1);

                    Console.WriteLine("Dims " + cellWidth.ToString() + "," + cellHeight.ToString());
                    //left corner of first box = 0,0
                    //left corner of last box: Width-(boxWidth
                    //Flip i and j so arr[0].Length is the width
                    float x = j * (float)cellWidth + (cellWidth/2.0f);
                    float y = i * (float)cellHeight + (cellHeight/2.0f);
                    Console.WriteLine(i.ToString() + "," + j.ToString() + "," + x + "," + y);

                    if (maze[i][j] == 1)
                    {
                        //Filled
                        Console.WriteLine("Filled: " + i.ToString() + "," + j.ToString());
                        g.FillRectangle(Brushes.Black, new RectangleF(new PointF(x, y), new SizeF(cellWidth, cellHeight)));
                    }


                    //top
                    g.DrawLine(Pens.Blue, new PointF(x, y), new PointF(x+cellWidth, y));
                    //left
                    g.DrawLine(Pens.Blue, new PointF(x, y), new PointF(x, y+cellHeight));
                    //right
                    g.DrawLine(Pens.Blue, new PointF(x+cellWidth, y), new PointF(x+cellWidth, y+cellHeight));
                    //bottom
                    g.DrawLine(Pens.Blue, new PointF(x, y+cellHeight), new PointF(x+cellWidth, y+cellHeight));
                }
            }
        }
    }
}
