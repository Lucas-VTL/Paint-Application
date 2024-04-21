using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftTriangle
{
    public class myShiftTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftTriangle";
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

            // Find the side length of the equiangular triangle
            var sideLength = Math.Min(width, height);

            // Calculate the height of the equilateral triangle
            var equilateralHeight = Math.Sqrt(3) / 2 * sideLength;

            // Set the height to be the same as the calculated equilateral height
            var value = equilateralHeight;

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var halfWidth = value / 2;
            var halfHeight = value / 2;

            var triangle = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeDashArray = strokeStyle.strokeValue,
                StrokeThickness = widthness.widthnessValue,
                Points = CreateTrianglePoints(center, halfWidth, halfHeight)
            };

            return triangle;
        }

        private PointCollection CreateTrianglePoints(Point center, double halfWidth, double halfHeight)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X, center.Y - halfHeight));
            points.Add(new Point(center.X - halfWidth, center.Y + halfHeight));
            points.Add(new Point(center.X + halfWidth, center.Y + halfHeight));

            return points;
        }
    }
}
