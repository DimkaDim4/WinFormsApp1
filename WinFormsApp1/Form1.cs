using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        bool isRed = true;
        int x, y;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            x = e.X;
            y = e.Y;
            Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                isRed = !isRed;
                Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.Exit();
            }
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            var pen = new Pen(isRed ? Color.FromName("Red") : Color.FromName("Green"), 3);

            int l = ClientRectangle.Left;
            int t = ClientRectangle.Top + menuStrip1.Height;
            int r = ClientRectangle.Right;
            int b = ClientRectangle.Bottom;
            e.Graphics.DrawLine(pen, l, t, r, b);
            e.Graphics.DrawLine(pen, r, t, l, b);
            e.Graphics.DrawString($"({x},{y})", DefaultFont,
            new SolidBrush(Color.Black), x, y);
        }
    }
}