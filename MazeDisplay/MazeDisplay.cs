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
        public MazeDisplay()
        {
            DoubleBuffered = true;
            mazeDrawer = new MazeDrawer(ClientSize.Width, ClientSize.Height);
            InitializeComponent();
            Console.WriteLine(Width.ToString() + "," + Height.ToString());
            Console.WriteLine(ClientSize.Width.ToString() + "," + ClientSize.Height.ToString());
        }

        public void MazeDisplay_DrawMaze(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Background
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));
            mazeDrawer.Draw(g);
        }

        protected override void OnResize(EventArgs e)
        {
            mazeDrawer.Resize(ClientSize.Width, ClientSize.Height);
            Invalidate();
        }

        private void MazeDisplay_Click(object sender, MouseEventArgs e)
        {
            //Get active cell
            //Fill in cell
            //Update maze solver
            if(mazeDrawer.MouseLookup(e))
            {
                mazeDrawer.CreateCells();
                Invalidate();
            }
        }

    }
}
