using System.Windows.Media;
using System.Windows;
using myShape;
using System.Windows.Shapes;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftFivePointStar
{
    public class myShiftFivePointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftFivePointStar";
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
            Point center = new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);

            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            string status = "";

            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
            {
                status = "normal";
            }
            else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside";
            }
            else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                status = "reverse";
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                status = "upside-reverse";
            }

            var element = new Path
            {
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Stroke = colorValue.colorValue,
                Data = CreateFivePointStarGeometry(center, startPoint, endPoint, width, height, status)
            };

            return element;
        }

        private Geometry CreateFivePointStarGeometry(Point center, Point start, Point end, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, startPoint.Y);
                //figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(center.X + width /6, center.Y - height / 6), true));
            }
            else if (status == "upside")
            {

            }
            else if (status == "reverse")
            {

            }
            else if (status == "upside-reverse")
            {

            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
