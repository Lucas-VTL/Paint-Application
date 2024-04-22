using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using myShape;
using myWidthness;
using myStroke;
using myColor;

namespace myShiftEllipse
{
    public class myShiftEllipse : IShape
    {
        private Point startPoint;
        private Point endPoint;
        IWidthness widthness;
        IStroke strokeStyle;
        IColor colorValue;
        public string shapeName => "ShiftEllipse";
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
            var left = Math.Min(endPoint.X, startPoint.X);
            var right = Math.Max(endPoint.X, startPoint.X);

            var top = Math.Min(endPoint.Y, startPoint.Y);
            var bottom = Math.Max(endPoint.Y, startPoint.Y);

            var width = right - left;
            var height = bottom - top;

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

                left = Math.Min(endPoint.X, startPoint.X);
                top = Math.Min(endPoint.Y, startPoint.Y);
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

                left = Math.Min(endPoint.X, startPoint.X);
                top = Math.Min(endPoint.Y, startPoint.Y);
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

                left = Math.Min(endPoint.X, startPoint.X);
                top = Math.Min(endPoint.Y, startPoint.Y);
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

                left = Math.Min(endPoint.X, startPoint.X);
                top = Math.Min(endPoint.Y, startPoint.Y);
            }

            var element = new Ellipse
            {
                Stroke = colorValue.colorValue,
                StrokeThickness = widthness.widthnessValue,
                StrokeDashArray = strokeStyle.strokeValue,
                Width = width,
                Height = height
            };

            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);

            return element;
        }

    }
}
