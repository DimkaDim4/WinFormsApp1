using Microsoft.VisualBasic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        bool isRed = true;
        int mode = 0;
        int x, y;
        public Form1()
        {
            InitializeComponent();

            System.Windows.Forms.Timer timerTime = new System.Windows.Forms.Timer();
            timerTime.Tick += new EventHandler(timerTime_Tick);
            timerTime.Interval = 1000;
            timerTime.Enabled = true;
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
            if ((e.Button.HasFlag(MouseButtons.Left)) && (mode == 0))
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

        private void крестикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = 0;
        }

        private void часыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int l = ClientRectangle.Left;
            int t = ClientRectangle.Top + menuStrip1.Height;
            int r = ClientRectangle.Right;
            int b = ClientRectangle.Bottom;

            if (mode == 0)
            {
                e.Graphics.Clear(Color.White);
                var pen = new Pen(isRed ? Color.FromName("Red") : Color.FromName("Green"), 3);
                e.Graphics.DrawLine(pen, l, t, r, b);
                e.Graphics.DrawLine(pen, r, t, l, b);
            }

            if (mode == 1)
            {
                e.Graphics.Clear(Color.White);

                int h = b - t;
                int w = r - l;

                int d = Math.Min(h, w);
                int radius = d / 2;
                int _y = h / 2 - radius + menuStrip1.Height;
                int _x = w / 2 - radius;

                int d1 = (int)(0.9 * d);
                int radius1 = d1 / 2;
                int _y1 = h / 2 - radius1 + menuStrip1.Height;
                int _x1 = w / 2 - radius1;

                int r_h = (int)(radius1 * 0.3);
                int r_m = (int)(radius1 * 0.5);
                int r_s = (int)(radius1 * 0.7);

                //прорисовка внешней окружности
                e.Graphics.DrawEllipse(new Pen(new SolidBrush(Color.CornflowerBlue), 2), new Rectangle(_x, _y, d, d));

                //прорисовка линий, который указывают на деления часов
                for (int i = 0; i < 12; i++)
                {
                    e.Graphics.DrawLine(new Pen(new SolidBrush(Color.CornflowerBlue), 2),
                        new Point(_x + radius, _y + radius),
                        new Point((int)(_x + radius * (1 + Math.Cos(Math.PI / 6 * i))),
                                  (int)(_y + radius * (1 + Math.Sin(Math.PI / 6 * i)))));
                }

                //прорисовка круга, который закрывает внутреннюю часть линий чтобы остались только черточки
                //этот круг меньше диаметром внешней окружности
                e.Graphics.FillEllipse(new SolidBrush(Control.DefaultBackColor), new Rectangle(_x1, _y1, d1, d1));

                //прорисовка стрелок часов
                DateTime dt = DateTime.Now;

                //прорисовка минутной стрелки
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 2),
                    new Point(_x1 + radius1, _y1 + radius1),
                    new Point((int)(_x1 + radius1 + r_m * Math.Sin(2 * Math.PI / 60 * dt.Minute)),
                              (int)(_y1 + radius1 - r_m * Math.Cos(2 * Math.PI / 60 * dt.Minute))));

                //определения количества часов, прошедших после полудня или после полуночи
                //фактически перевод 23=>11 и так далее
                int hour;
                if (dt.Hour <= 12)
                {
                    hour = dt.Hour;
                }
                else
                {
                    hour = dt.Hour - 12;
                }

                //прорисовка часовой стрелки
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Black), 2),
                    new Point(_x1 + radius1, _y1 + radius1),
                    new Point((int)(_x1 + radius1 + r_h * Math.Sin(2 * Math.PI / 12 * hour + 2 * Math.PI / (12 * 60) * dt.Minute)),
                              (int)(_y1 + radius1 - r_h * Math.Cos(2 * Math.PI / 12 * hour + 2 * Math.PI / (12 * 60) * dt.Minute))));

                //прорисовка секундной стрелки
                e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red), 2),
                    new Point(_x1 + radius1, _y1 + radius1),
                    new Point((int)(_x1 + radius1 + r_s * Math.Sin(2 * Math.PI / 60 * dt.Second)),
                              (int)(_y1 + radius1 - r_s * Math.Cos(2 * Math.PI / 60 * dt.Second))));
            }


            e.Graphics.DrawString($"({x},{y})", DefaultFont,
            new SolidBrush(Color.Black), x, y);
        }

        //"тик" таймера
        private void timerTime_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}