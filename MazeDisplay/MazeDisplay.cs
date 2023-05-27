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

            mazeDrawer.Draw(g);

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
            if(mazeDrawer.MouseLookup(e))
            {
                Console.WriteLine("Clicked in cell");
                mazeDrawer.CreateCells();
                Invalidate();
            }
        }

    }
}
