using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using myColor;
using myShape;
using myStroke;
using myWidthness;

namespace myHeart
{
    public class myHeart : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "Heart";
        public string shapeImage => "images/shapeHeart.png";

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
                Data = CreateHeartGeometry(center, startPoint, endPoint, width, height, status)
            };

            return element;
        }

        private Geometry CreateHeartGeometry(Point center, Point start, Point end, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (start.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + start.Y) / 2) - height / 8),
                    Point3 = new Point(start.X + width / 4, start.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(start.X + width / 4, start.Y),
                    Point2 = new Point((((start.X + width / 4) + start.X) / 2) - width / 8, ((start.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(start.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(start.X, center.Y - height / 4),
                    Point2 = new Point(((start.X + (start.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(start.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, end.Y), true));
                figure.Segments.Add(new LineSegment(new Point(end.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(end.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((end.X - width / 4) + end.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(end.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(end.X, center.Y - height / 4),
                    Point2 = new Point(((end.X + (end.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + start.Y) / 2) - height / 8),
                    Point3 = new Point(end.X - width / 4, start.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(end.X - width / 4, start.Y),
                    Point2 = new Point((((end.X - width / 4) + center.X) / 2) - width / 8, ((start.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
