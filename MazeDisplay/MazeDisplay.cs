using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, ClientSize.Width, ClientSize.Height));
            mazeDrawer.Draw(g);
        }

        protected override void OnResize(EventArgs e)
        {
            mazeDrawer.Resize(ClientSize.Width, ClientSize.Height);
            menuStrip1.Width = ClientSize.Width;
            Invalidate();
        }

        private void MazeDisplay_Click(object sender, MouseEventArgs e)
        {
            //Get active cell
            //Fill in cell
            //Update maze solver
            if (mazeDrawer.MouseLookup(e))
            {
                mazeDrawer.CreateCells();
                Invalidate();
            }
        }

        private void MazeDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Y <= menuStrip1.Height)
            {
                menuStrip1.Show();
            } else
            {
                menuStrip1.Hide();
            }
        }

        private void importFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            System.Windows.Forms.DialogResult selected = dialog.ShowDialog();
            if (selected == DialogResult.OK)
            {
                string filename = dialog.FileName;
                if (File.Exists(filename))
                {
                    string[] lines = File.ReadAllLines(filename);

                    if(lines.Length == 0)
                    {
                        Console.WriteLine("Error: Empty maze");
                        return;
                    }

                    char[] delim = new char[] { ',' };

                    int[][] newMaze = new int[lines.Length][];
                    int expectedLen = -1;
                    for (int i = 0; i < lines.Length; ++i)
                    {
                        string[] entries = lines[i].Trim().Split(delim);

                        if (i == 0)
                        {
                            //Save length of first row to ensure maze is symmetrical
                            expectedLen = entries.Length;
                        }

                        if (entries.Length != expectedLen)
                        {
                            Console.WriteLine("Unexpected row length at line " + (i + 1).ToString() +
                                " Expected len: " + expectedLen.ToString() +
                                " Found len: " + expectedLen.ToString());
                            return;
                        }
                        newMaze[i] = new int[entries.Length];
                        for (int j = 0; j < entries.Length; ++j)
                        {
                            int val;
                            bool ret = Int32.TryParse(entries[j], out val);
                            if(!ret)
                            {
                                Console.WriteLine("Found non-number at line " + i.ToString() + " val: " + entries[j]);
                                return;
                            }
                            if (val == 0 || val == 1)
                            {
                                newMaze[i][j] = val;
                            }
                            else
                            {
                                Console.WriteLine("Unexpected value at line: " + (j + 1).ToString() +
                                    " value: " + val.ToString());
                                return;
                            }
                        }
                    }
                    //Force start and end point to not be blocked
                    newMaze[0][0] = 0;
                    newMaze[newMaze.Length - 1][newMaze[0].Length - 1] = 0;


                    for (int i = 0; i < newMaze.Length; ++i)
                    {
                        for (int j = 0; j < newMaze[i].Length; ++j)
                        {
                            Console.Write(newMaze[i][j] + ",");
                        }
                        Console.WriteLine();
                    }

                    mazeDrawer.UpdateMaze(newMaze);
                    //Call resize to recalculate the cell sizes
                    mazeDrawer.Resize(ClientSize.Width, ClientSize.Height);
                    Invalidate();
                }
            }
            else
            {
                Console.WriteLine("Failed to read file");
            }
        }
    }
}
