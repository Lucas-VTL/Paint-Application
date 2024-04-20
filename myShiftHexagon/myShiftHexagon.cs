using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftHexagon
{
    public class myShiftHexagon : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftHexagon";
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

            var width = Math.Abs(end.X - start.X);
            var height = Math.Abs(end.Y - start.Y);
            //var height = width;

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var sideLength = Math.Min(width / 2, height / 2);

            var hexagon = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Points = CreateHexagonPoints(center, sideLength)
            };

            return hexagon;
        }

        private PointCollection CreateHexagonPoints(Point center, double sideLength)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X, center.Y - sideLength / 2.5));
            points.Add(new Point(center.X - sideLength, center.Y + sideLength / 4));
            points.Add(new Point(center.X - sideLength, center.Y + 1.3 * sideLength));
            points.Add(new Point(center.X, center.Y + 2 * sideLength));
            points.Add(new Point(center.X + sideLength, center.Y + 1.3 * sideLength));
            points.Add(new Point(center.X + sideLength, center.Y + sideLength / 4));

            //for (int i = 0; i < 6; i++)
            //{
            //    double angle = Math.PI / 3 * i;
            //    points.Add(new Point(center.X + sideLength * Math.Cos(angle), center.Y + sideLength * Math.Sin(angle)));
            //}

            return points;
        }
    }
}
