using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftRightTriangle
{
    public class myShiftRightTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftRightTriangle";
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
            double width = Math.Abs(endPoint.X - startPoint.X);
            double height = Math.Abs(endPoint.Y - startPoint.Y);

            Point center;
            double hypotenuseLength = 0;

            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X + height, startPoint.Y + height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X + width, startPoint.Y + width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                hypotenuseLength = Math.Sqrt(width * width + height * height);
            }
            else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X + height, startPoint.Y - height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X + width, startPoint.Y - width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                hypotenuseLength = Math.Sqrt(width * width + height * height);
            }
            else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X - height, startPoint.Y + height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X - width, startPoint.Y + width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                hypotenuseLength = Math.Sqrt(width * width + height * height);
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (width > height)
                {
                    width = height;
                    endPoint = new Point(startPoint.X - height, startPoint.Y - height);
                }
                else
                {
                    height = width;
                    endPoint = new Point(startPoint.X - width, startPoint.Y - width);
                }

                center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
                hypotenuseLength = Math.Sqrt(width * width + height * height);
            }

            var rightTriangle = new Polygon
            {
                Stroke = colorValue.colorValue,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Points = CreateRightTrianglePoints(center, width, height, hypotenuseLength)
            };

            return rightTriangle;
        }

        private PointCollection CreateRightTrianglePoints(Point center, double width, double height, double hypotenuseLength)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X - width / 2, center.Y + height / 2));
            points.Add(new Point(center.X - width / 2, center.Y - height / 2));
            points.Add(new Point(center.X + width / 2, center.Y + height / 2));

            return points;
        }
    }
}
