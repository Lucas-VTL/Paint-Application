using System.Windows.Media;
using System.Windows;
using myShape;
using System.Windows.Shapes;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftFivePointStar
{
    public class myShiftFivePointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftFivePointStar";
        public string shapeImage => "";

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
        public void addColor(IColor color)
        {
            colorValue = color;
        }
        public void addPointList(List<Point> pointList) { }
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

            var element = new Path
            {
                Fill = Brushes.AliceBlue,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Data = CreateStarGeometry(center, radius)
            };

            return element;
        }

        private Geometry CreateStarGeometry(Point center, double radius)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure
            {
                StartPoint = new Point(center.X + radius * Math.Cos(-Math.PI / 2), center.Y + radius * Math.Sin(-Math.PI / 2)),
                IsClosed = true
            };

            for (int i = 1; i <= 5; i++)
            {
                double angle = i * 4 * Math.PI / 5 - Math.PI / 2;
                figure.Segments.Add(new LineSegment(new Point(center.X + radius * Math.Cos(angle), center.Y + radius * Math.Sin(angle)), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
