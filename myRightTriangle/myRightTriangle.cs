using myShape;
using myStroke;
using myWidthness;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myRightTriangle
{
    public class myRightTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        public string shapeName => "RightTriangle";
        public string shapeImage => "images/shapeRightTriangle.png";

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

            var hypotenuseLength = Math.Sqrt(width * width + height * height);

            var rightTriangle = new Polygon
            {
                Stroke = Brushes.Black,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Points = CreateRightTrianglePoints(center, width, height, hypotenuseLength)
            };

            return rightTriangle;
        }

        private PointCollection CreateRightTrianglePoints(Point center, double width, double height, double hypotenuseLength)
        {
            var points = new PointCollection();

            // Vertex at the bottom-left corner
            points.Add(new Point(center.X - width / 2, center.Y + height / 2));

            // Vertex at the top-left corner
            points.Add(new Point(center.X - width / 2, center.Y - height / 2));

            // Vertex at the bottom-right corner
            points.Add(new Point(center.X + width / 2, center.Y + height / 2));

            return points;
        }
    }
}
