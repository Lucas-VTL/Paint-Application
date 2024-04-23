using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using myColor;
using myShape;
using myStroke;
using myWidthness;

namespace myFourPointStar
{
    public class myFourPointStar : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

        public string shapeName => "FourPointStar";
        public string shapeImage => "images/shape4Star.png";

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

            Path element;

            if (isFill)
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Fill = colorValue.colorValue,
                    Data = CreateFourPointStarGeometry(center, width, height, status)
                };
            } else
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Data = CreateFourPointStarGeometry(center, width, height, status)
                };
            }

            return element;
        }

        private Geometry CreateFourPointStarGeometry(Point center, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, startPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
            }
            else if (status == "upside")
            {
                figure.StartPoint = new Point(center.X, endPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
            }
            else if (status == "reverse")
            {
                figure.StartPoint = new Point(center.X, startPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
            }
            else if (status == "upside-reverse")
            {
                figure.StartPoint = new Point(center.X, endPoint.Y);
                figure.IsClosed = true;

                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X + width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y + height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X, center.Y), true));
                figure.Segments.Add(new LineSegment(new Point(center.X - width / 12, center.Y - height / 12), true));
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
    }
}
