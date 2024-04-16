﻿using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace myShiftRightTriangle
{
    public class myShiftRightTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftRightTriangle";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            var start = startPoint;
            var end = endPoint;

            var width = Math.Abs(end.X - start.X);
            var height = Math.Abs(end.Y - start.Y);

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);

            // Determine the length of the hypotenuse (the longest side of the right triangle)
            var hypotenuseLength = Math.Sqrt(width * width + height * height);

            var rightTriangle = new Polygon
            {
                Fill = Brushes.Magenta,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Points = CreateRightTrianglePoints(center, width, height, hypotenuseLength)
            };

            return rightTriangle;
        }

        private PointCollection CreateRightTrianglePoints(Point center, double width, double height, double hypotenuseLength)
        {
            var points = new PointCollection();

            // Vertex at the bottom-left corner
            points.Add(new Point(center.X - width / 2, center.Y + height / 2));

            // Vertex at the top-left corner
            points.Add(new Point(center.X - width / 2, center.Y - height / 2));

            // Vertex at the bottom-right corner
            points.Add(new Point(center.X + width / 2, center.Y + height / 2));

            return points;
        }
    }
}