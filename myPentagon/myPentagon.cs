using myShape;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myPentagon
{
    public class myPentagon : IShape
    {
        private Point startPoint;
        private Point endPoint;

        public string shapeName => "Pentagon";
        public string shapeImage => "images/shapePentagon.png";

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
            var sideLength = Math.Min(width / 2, height / 2);

            var pentagon = new Polygon
            {
                Fill = Brushes.Blue,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Points = CreatePentagonPoints(center, sideLength)
            };

            return pentagon;
        }

        private PointCollection CreatePentagonPoints(Point center, double sideLength)
        {
            var points = new PointCollection();

            for (int i = 0; i < 5; i++)
            {
                double angle = 2 * Math.PI / 5 * i;
                points.Add(new Point(center.X + sideLength * Math.Cos(angle), center.Y + sideLength * Math.Sin(angle)));
            }

            return points;
        }
    }
}
