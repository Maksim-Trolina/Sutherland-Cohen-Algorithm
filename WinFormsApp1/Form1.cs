namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Line[] lines;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            lines = GetLines();
            DrawLines(graphics, Color.Orange);
        }

        Line[] GetLines()
        {
            int xMin = 0;
            int yMin = 0;
            int xMax = pictureBox1.Width + xMin;
            int yMax = pictureBox1.Height + yMin;
            int countLines = (int)numericUpDown3.Value;
            var lines = new Line[countLines];

            for(int i = 0; i < countLines; i++)
            {
                lines[i] = Line.GetRandomLine(xMax, yMax, xMin, yMin);
            }

            return lines;
        }

        void DrawLines(Graphics graphics, Color color)
        {
            var pen = new Pen(color);

            foreach(var line in lines)
            {
                graphics.DrawLine(pen, line.FirstPoint, line.SecondPoint);
            }
        }
        //Must have
        int GetCode(PointF point, int xMin, int xMax, int yMin, int yMax)
        {
            return ((point.X < xMin) ? 1 : 0) |
                ((point.X > xMax) ? 1 : 0) << 1 |
                ((point.Y < yMin) ? 1 : 0) << 3 |
                ((point.Y > yMax) ? 1 : 0) << 2;
        }

        void Repainting(Graphics graphics, int xMax, int yMax, int xMin, int yMin)
        {
            foreach(var line in lines)
            {
                var pen = GetPen(line, xMin, xMax, yMin, yMax);
                graphics.DrawLine(pen, line.FirstPoint, line.SecondPoint);
            }

            graphics.DrawRectangle(Pens.Black, xMin, yMin, xMax - xMin, yMax - yMin);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int xMin = e.X;
            int yMin = e.Y;
            int xMax = (int)numericUpDown1.Value + xMin;
            int yMax = (int)numericUpDown2.Value + yMin;
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.Clear(Color.White);
            Repainting(graphics, xMax, yMax, xMin, yMin);
        }
        //Must have
        Pen GetPen(Line line, int xMin, int xMax, int yMin, int yMax)
        {
            int firstCode = GetCode(line.FirstPoint, xMin, xMax, yMin, yMax);
            int secondCode = GetCode(line.SecondPoint, xMin, xMax, yMin, yMax);

            if (firstCode == 0 && secondCode == 0)
                return Pens.Green;
            PointF p1 = line.FirstPoint;
            PointF p2 = line.SecondPoint;
            bool flag;
            while((firstCode | secondCode) != 0)
            {
                if ((firstCode & secondCode) != 0)
                    return Pens.Blue;

                if(firstCode != 0)
                {
                    p1 = Shift(p1, p2, firstCode, xMin, xMax, yMin, yMax, p1, out flag);
                    firstCode = flag ? 0 : GetCode(p1, xMin, xMax, yMin, yMax);
                }
                else
                {
                    p2 = Shift(p1, p2, secondCode, xMin, xMax, yMin, yMax, p2, out flag);
                    secondCode = flag ? 0 : GetCode(p2, xMin, xMax, yMin, yMax);
                }
            }

            return Pens.Red;
        }

        PointF Shift(PointF point1, PointF point2, int code, int xMin, int xMax, int yMin, int yMax, PointF source, out bool flag)
        {
            float shift;
            if((code & 1) != 0)
            {
                shift = (point1.Y - point2.Y) * (xMin - source.X) / (point1.X - point2.X);
                if(shift == 0)
                {
                    flag = true;
                    return source;
                }
                source.Y += shift;
                source.X = xMin;
            }
            else if((code & 2) != 0)
            {
                shift = (point1.Y - point2.Y) * (xMax - source.X) / (point1.X - point2.X);
                if (shift == 0)
                {
                    flag = true;
                    return source;
                }
                source.Y += shift;
                source.X = xMax;
            }
            else if((code & 4) != 0)
            {
                shift = (point1.X - point2.X) * (yMin - source.Y) / (point1.Y - point2.Y);
                if (shift == 0)
                {
                    flag = true;
                    return source;
                }
                source.X += shift;
                source.Y = yMin;
            } 
            else if((code & 8) != 0)
            {
                shift = (point1.X - point2.X) * (yMax - source.Y) / (point1.Y - point2.Y);
                if (shift == 0)
                {
                    flag = true;
                    return source;
                }
                source.X += shift;
                source.Y = yMax;
            }
            flag = false;
            return source;
        }
    }
}