using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using myShape;
using myStroke;
using myWidthness;

namespace myHeart
{
    public class myHeart : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        public string shapeName => "Heart";
        public string shapeImage => "images/shapeHeart.png";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width)
        {
            widthness = width;
        }
        public void addStrokeStyle(IStroke stroke)
        {
            strokeStyle = stroke;
        }
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
            var radiusX = width / 2;
            var radiusY = height / 2;

            var path = new Path
            {
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Data = CreateHeartGeometry(center, radiusX, radiusY)
            };

            return path;
        }

        private Geometry CreateHeartGeometry(Point center, double radiusX, double radiusY)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(center.X, center.Y + radiusY)
            };

            // First arc (left side)
            figure.Segments.Add(new ArcSegment(new Point(center.X - radiusX, center.Y), new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true));

            // Left bottom curve
            figure.Segments.Add(new BezierSegment(new Point(center.X - radiusX, center.Y + radiusY / 2), new Point(center.X - radiusX, center.Y + radiusY * 3 / 4), new Point(center.X, center.Y + radiusY), true));

            // Right bottom curve
            figure.Segments.Add(new BezierSegment(new Point(center.X + radiusX, center.Y + radiusY), new Point(center.X + radiusX, center.Y + radiusY * 3 / 4), new Point(center.X + radiusX, center.Y + radiusY / 2), true));

            // Second arc (right side)
            figure.Segments.Add(new ArcSegment(new Point(center.X, center.Y + radiusY), new Size(radiusX, radiusY), 0, false, SweepDirection.Clockwise, true));

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
