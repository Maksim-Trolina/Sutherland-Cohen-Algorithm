using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal struct Line
    {
        public PointF FirstPoint { get; set; }

        public PointF SecondPoint { get; set; }
        //Must have
        public static Line GetRandomLine(int xMax, int yMax, int xMin, int yMin)
        {
            var random = new Random();

            var firstPoint = new PointF
            {
                X = random.Next(xMin, xMax),
                Y = random.Next(yMin, yMax)
            };

            var secondPoint = new PointF
            {
                X = random.Next(xMin, xMax),
                Y = random.Next(yMin, yMax)
            };

            var line = new Line
            {
                FirstPoint = firstPoint,
                SecondPoint = secondPoint
            };

            return line;
        }
    }
}
