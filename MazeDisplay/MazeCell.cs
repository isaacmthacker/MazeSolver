using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeDisplay
{
    internal class MazeCell
    {
        //Index used to create cell
        int i, j;
        //top left
        float x, y;
        float width, height;
        //Denoting a blocked cell
        bool blocked = false;
        //Denoting a cell in the maze path
        bool filled = false;
        //Cell representing the end of the maze
        bool end = false;
        Pen outline = new Pen(Brushes.Black);
        public MazeCell(int i, int j, float x, float y, float width, float height)
        {
            this.i = i;
            this.j = j;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public int I
        {
            get { return i; }
        }
        public int J
        {
            get { return j; }
        }
        public bool Filled
        {
            set { filled = value; }
        }
        public bool Blocked
        {
            set { blocked = value; }
        }
        public bool End
        {
            set { end = value; }
            get { return end; }
        }
        public void Draw(Graphics g)
        {
            Brush fillColor = Brushes.White;
            if (blocked)
            {
                fillColor = Brushes.Black;
            } else if (filled)
            {
                fillColor = Brushes.Red;
            }
            else if (end)
            {
                fillColor = Brushes.Blue;                
            }

            g.FillRectangle(fillColor, new RectangleF(new PointF(x, y), new SizeF(width, height)));

            //top
            g.DrawLine(outline, new PointF(x, y), new PointF(x + width, y));
            //left
            g.DrawLine(outline, new PointF(x, y), new PointF(x, y + height));
            //right
            g.DrawLine(outline, new PointF(x + width, y), new PointF(x + width, y + height));
            //bottom
            g.DrawLine(outline, new PointF(x, y + height), new PointF(x + width, y + height));
        }
        public bool Intersect(int mouseX, int mouseY)
        {
            return x <= mouseX 
                && mouseX <= x + width 
                && y <= mouseY 
                && mouseY <= y + height;
        }

        public bool Click(int mouseX, int mouseY)
        {
            if (Intersect(mouseX, mouseY))
            {
                blocked = !blocked;
                if(filled)
                {
                    if(blocked)
                    {
                        filled = false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
