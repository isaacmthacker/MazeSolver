using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeDisplay
{
    public partial class MazeDisplay : Form
    {
        private MazeDrawer mazeDrawer = null;
        private List<int> xs = new List<int>();
        private List<int> ys = new List<int>();
        public MazeDisplay()
        {
            DoubleBuffered = true;
            mazeDrawer = new MazeDrawer(ClientSize.Width, ClientSize.Height);
            InitializeComponent();
            Console.WriteLine(Width.ToString() + "," + Height.ToString());
            Console.WriteLine(ClientSize.Width.ToString() + "," + ClientSize.Height.ToString());
        }

        private void MazeDisplay_Load(object sender, EventArgs e)
        {

        }

        public void MazeDisplay_DrawMaze(object sender, System.Windows.Forms.PaintEventArgs e)
        {

            Console.WriteLine("Drawing");
            Graphics g = e.Graphics;

            g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            for(int i = 0;  i < xs.Count; ++i)
            {
                g.DrawEllipse(Pens.Red, new Rectangle(xs[i], ys[i], 50, 50));
            }

            g.FillEllipse(Brushes.Crimson, 0, 0, 10, 10);
            g.FillEllipse(Brushes.Crimson, Width-24, 0, 10, 10);
            g.FillEllipse(Brushes.Crimson, 0, Height-39-19, 10, 10);
            g.FillEllipse(Brushes.Crimson, Width-24, Height - 39 - 19, 10, 10);
            mazeDrawer.Draw(g);

            float boxWidth = 150;
            float boxHeight = 150;
            float x = (float)(ClientSize.Width / 2.0 - (boxWidth / 2.0)) - 16;
            float y = (float)(ClientSize.Height / 2.0 - (boxHeight / 2.0));
            g.FillRectangle(Brushes.Blue, new RectangleF(
                new PointF(x, y),
                new SizeF(boxWidth, boxHeight)));

            g.DrawLine(Pens.Black, new PointF(x, y), new PointF(x + boxWidth, y));
            //left
            g.DrawLine(Pens.Black, new PointF(x, y), new PointF(x, y + boxHeight));
            //right
            g.DrawLine(Pens.Black, new PointF(x + boxWidth, y), new PointF(x + boxWidth, y + boxHeight));
            //bottom
            g.DrawLine(Pens.Black, new PointF(x, y + boxHeight), new PointF(x + boxWidth, y + boxHeight));


            //todo: update draw rect to draw lines instead
            //fill will work, drawing needs to use lines
        }

        protected override void OnResize(EventArgs e)
        {
            Console.WriteLine("resize");
            mazeDrawer.Resize(ClientSize.Width, ClientSize.Height);
            Invalidate();
        }

        private void MazeDisplay_Click(object sender, MouseEventArgs e)
        {
            //Get active cell
            //Fill in cell
            //Update maze solver
            Console.WriteLine("Click");
            xs.Add(e.X-25);
            ys.Add(e.Y-25);
            
            Invalidate();
        }

    }
}
