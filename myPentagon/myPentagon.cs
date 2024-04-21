using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myPentagon
{
    public class myPentagon : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "Pentagon";
        public string shapeImage => "images/shapePentagon.png";

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

            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
            var halfWidth = width / 2;
            var halfHeight = height / 2;

            var pentagon = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Points = CreatePentagonPoints(center, halfWidth, halfHeight)
            };

            return pentagon;
        }

        private PointCollection CreatePentagonPoints(Point center, double halfWidth, double halfHeight)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X, center.Y - halfHeight / 2));
            points.Add(new Point(center.X - 2 * halfWidth, center.Y + halfHeight / 8));
            points.Add(new Point(center.X - halfWidth, center.Y + halfHeight));
            points.Add(new Point(center.X + halfWidth, center.Y + halfHeight));
            points.Add(new Point(center.X + 2 * halfWidth, center.Y + halfHeight / 8));

            return points;
        }
    }
}
