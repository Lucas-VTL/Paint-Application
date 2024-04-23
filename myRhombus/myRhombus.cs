using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myRhombus
{
    public class myRhombus : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

        public string shapeName => "Rhombus";
        public string shapeImage => "images/shapeRhombus.png";

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
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
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

            Polygon element;

            if (isFill)
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                    Points = CreateRhombusPoints(center, halfWidth, halfHeight)
                };
            } else
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Points = CreateRhombusPoints(center, halfWidth, halfHeight)
                };
            }

            return element;
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
