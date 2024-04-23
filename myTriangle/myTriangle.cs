using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myTriangle
{
    public class myTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "Triangle";
        public string shapeImage => "images/shapeTriangle.png";

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
            var width = Math.Abs(endPoint.X - startPoint.X);
            var height = Math.Abs(endPoint.Y - startPoint.Y);

            var center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
            var halfWidth = width / 2;
            var halfHeight = height / 2;

            var triangle = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
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
