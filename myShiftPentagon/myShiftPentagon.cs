using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftPentagon
{
    public class myShiftPentagon : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftPentagon";
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

            // Calculate the side length of the pentagon based on the minimum of width and height
            var sideLength = Math.Min(width, height) / 2;

            // Calculate the center point
            var center = new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);

            // Create the pentagon shape
            var pentagon = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeDashArray = strokeStyle.strokeValue,
                StrokeThickness = widthness.widthnessValue,
                Points = CreatePentagonPoints(center, sideLength)
            };

            return pentagon;
        }

        private PointCollection CreatePentagonPoints(Point center, double sideLength)
        {
            var points = new PointCollection();

            // Calculate angles for each vertex of the pentagon
            double angle = -Math.PI / 2; // Starting angle for the top vertex
            double angleIncrement = 2 * Math.PI / 5; // Angle increment for each vertex

            // Loop to create vertices of the pentagon
            for (int i = 0; i < 5; i++)
            {
                double x = center.X + sideLength * Math.Cos(angle);
                double y = center.Y + sideLength * Math.Sin(angle);
                points.Add(new Point(x, y));

                // Increment the angle
                angle += angleIncrement;
            }

            return points;
        }
    }
}
