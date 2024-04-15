using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace myShiftRhombus
{
    public class myShiftRhombus : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "ShiftRhombus";
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
            var halfWidth = width / 2;
            var halfHeight = height / 2;

            var rhombus = new Polygon
            {
                Fill = Brushes.Orange,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Points = CreateRhombusPoints(center, halfWidth, halfHeight)
            };

            return rhombus;
        }

        private PointCollection CreateRhombusPoints(Point center, double halfWidth, double halfHeight)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X - halfWidth, center.Y));
            points.Add(new Point(center.X, center.Y + halfHeight));
            points.Add(new Point(center.X + halfWidth, center.Y));
            points.Add(new Point(center.X, center.Y - halfHeight));

            return points;
        }
    }
}
