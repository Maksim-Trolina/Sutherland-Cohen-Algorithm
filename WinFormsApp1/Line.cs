using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal struct Line
    {
        public Point FirstPoint { get; set; }

        public Point SecondPoint { get; set; }
        //Must have
        public static Line GetRandomLine(int xMax, int yMax, int xMin, int yMin)
        {
            var random = new Random();

            var firstPoint = new Point
            {
                X = random.Next(xMin, xMax),
                Y = random.Next(yMin, yMax)
            };

            var secondPoint = new Point
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
