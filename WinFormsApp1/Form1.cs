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
        int GetCode(Point point, int xMin, int xMax, int yMin, int yMax)
        {
            return ((point.X < xMin) ? 1 : 0) << 3 |
                ((point.X > xMax) ? 1 : 0) << 2 |
                ((point.Y < yMin) ? 1 : 0) << 1 |
                ((point.Y > yMax) ? 1 : 0);
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

            if ((firstCode & secondCode) != 0)
                return Pens.Blue;

            return Pens.Red;
        }
    }
}