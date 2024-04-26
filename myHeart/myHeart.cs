using System.Windows;
using System.Windows.Controls;
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
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

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
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setFocus(bool focus) { }
        public void addFontSize(int fontSize) { }
        public void addFontFamily(string fontFamily) { }
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
                    Data = CreateHeartGeometry(center, width, height, status)
                };
            } else
            {
                element = new Path
                {
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Stroke = colorValue.colorValue,
                    Data = CreateHeartGeometry(center, width, height, status)
                };
            }

            return element;
        }

        private Geometry CreateHeartGeometry(Point center, double width, double height, string status)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            if (status == "normal")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (startPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X + width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X + width / 4, startPoint.Y),
                    Point2 = new Point((((startPoint.X + width / 4) + startPoint.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(startPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((endPoint.X - width / 4) + endPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X - width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, startPoint.Y),
                    Point2 = new Point((((endPoint.X - width / 4) + center.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            } else if (status == "upside")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (startPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X + width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X + width / 4, endPoint.Y),
                    Point2 = new Point((((startPoint.X + width / 4) + startPoint.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(startPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(endPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((endPoint.X - width / 4) + endPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X - width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X - width / 4, endPoint.Y),
                    Point2 = new Point((((endPoint.X - width / 4) + center.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            } else if (status == "reverse")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (endPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X + width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X + width / 4, startPoint.Y),
                    Point2 = new Point((((endPoint.X + width / 4) + endPoint.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(endPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, endPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((startPoint.X - width / 4) + startPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + startPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X - width / 4, startPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, startPoint.Y),
                    Point2 = new Point((((startPoint.X - width / 4) + center.X) / 2) - width / 8, ((startPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            } else if (status == "upside-reverse")
            {
                figure.StartPoint = new Point(center.X, center.Y - height / 4);

                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(center.X, center.Y - height / 4),
                    Point2 = new Point(((center.X + (endPoint.X + width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(endPoint.X + width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X + width / 4, endPoint.Y),
                    Point2 = new Point((((endPoint.X + width / 4) + endPoint.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(endPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(endPoint.X, center.Y - height / 4),
                    Point2 = new Point(((endPoint.X + (endPoint.X + width / 4)) / 2) - width / 8, (((center.Y - height / 4) + (center.Y + height / 4)) / 2)),
                    Point3 = new Point(endPoint.X + width / 4, center.Y + height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new LineSegment(new Point(center.X, startPoint.Y), true));
                figure.Segments.Add(new LineSegment(new Point(startPoint.X - width / 4, center.Y + height / 4), true));
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, center.Y + height / 4),
                    Point2 = new Point((((startPoint.X - width / 4) + startPoint.X) / 2) + width / 8, (((center.Y + height / 4) + (center.Y - height / 4)) / 2)),
                    Point3 = new Point(startPoint.X, center.Y - height / 4),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X, center.Y - height / 4),
                    Point2 = new Point(((startPoint.X + (startPoint.X - width / 4)) / 2) + width / 8, (((center.Y - height / 4) + endPoint.Y) / 2) - height / 8),
                    Point3 = new Point(startPoint.X - width / 4, endPoint.Y),
                    IsStroked = true
                });
                figure.Segments.Add(new BezierSegment()
                {
                    Point1 = new Point(startPoint.X - width / 4, endPoint.Y),
                    Point2 = new Point((((startPoint.X - width / 4) + center.X) / 2) - width / 8, ((endPoint.Y + (center.Y - height / 4)) / 2) - height / 8),
                    Point3 = new Point(center.X, center.Y - height / 4),
                    IsStroked = true
                });
            }

            return path;
        }
    }
}
