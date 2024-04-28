using myShape;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myWidthness;
using myStroke;
using myColor;
using System.Windows.Controls;

namespace myShiftTriangle
{
    public class myShiftTriangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;

        public string shapeName => "ShiftTriangle";
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
        public void addFontSize(int fontSize) { }
        public void addFontFamily(string fontFamily) { }
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setFocus(bool focus) { }
        public void setShapeFill(bool isShapeFill)
        {
            isFill = isShapeFill;
        }
        public Point getStartPoint() { return startPoint; }
        public Point getEndPoint() { return endPoint; }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            double width = Math.Abs(endPoint.X - startPoint.X);
            double height = Math.Abs(endPoint.Y - startPoint.Y);

            Point center;
            double halfWidth = 0;
            double halfHeight = 0;

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
                halfWidth = width / 2;
                halfHeight = height / 2;
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
                halfWidth = width / 2;
                halfHeight = height / 2;
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
                halfWidth = width / 2;
                halfHeight = height / 2;
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
                halfWidth = width / 2;
                halfHeight = height / 2;
            }

            Polygon element;

            if (isFill)
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                    Points = CreateTrianglePoints(center, halfWidth, halfHeight)
                };
            } else
            {
                element = new Polygon
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Points = CreateTrianglePoints(center, halfWidth, halfHeight)
                };
            }

            return element;
        }

        private PointCollection CreateTrianglePoints(Point center, double halfWidth, double halfHeight)
        {
            var points = new PointCollection();

            points.Add(new Point(center.X, center.Y - halfHeight));
            points.Add(new Point(center.X - halfWidth, center.Y + halfHeight));
            points.Add(new Point(center.X + halfWidth, center.Y + halfHeight));

            return points;
        }
    }
}
