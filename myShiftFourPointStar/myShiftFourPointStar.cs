using System.Windows.Media;
using System.Windows;
using myShape;
using System.Windows.Shapes;

namespace myShiftFourPointStar
{
    public class myShiftFourPointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftFourPointStar";
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

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var radius = Math.Min(Math.Abs(start.X - end.X), Math.Abs(start.Y - end.Y)) / 2;

            var path = new Path
            {
                Fill = Brushes.Yellow,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Data = CreateFourPointStarGeometry(center, radius)
            };

            return path;
        }

        private Geometry CreateFourPointStarGeometry(Point center, double radius)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(center.X + radius * Math.Cos(-Math.PI / 2), center.Y + radius * Math.Sin(-Math.PI / 2)),
                IsClosed = true
            };

            for (int i = 1; i <= 4; i++)
            {
                double outerAngle = i * 2 * Math.PI / 4 - Math.PI / 2;
                double innerAngle = outerAngle + Math.PI / 4;

                figure.Segments.Add(new LineSegment(new Point(center.X + radius * Math.Cos(outerAngle), center.Y + radius * Math.Sin(outerAngle)), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + radius / 2 * Math.Cos(innerAngle), center.Y + radius / 2 * Math.Sin(innerAngle)), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
