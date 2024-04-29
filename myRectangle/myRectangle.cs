using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace myRectangle
{
    public class myRectangle : IShape
    {
        private Point startPoint;
        private Point endPoint;
        private IWidthness widthness;
        private IStroke strokeStyle;
        private IColor colorValue;
        private bool isFill;
        public string shapeName => "Rectangle";
        public string shapeImage => "images/shapeRectangle.png";

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
        public void setBold(bool bold) { }
        public void setItalic(bool italic) { }
        public void setBackground(byte r, byte g, byte b) { }
        public Point getStartPoint()
        {
            return startPoint;
        }
        public Point getEndPoint()
        {
            return endPoint;
        }
        public Point getCenterPoint()
        {
            return new Point((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2);
        }
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
            var left = Math.Min(startPoint.X, endPoint.X);
            var right = Math.Max(startPoint.X, endPoint.X);

            var top = Math.Min(startPoint.Y, endPoint.Y);
            var bottom = Math.Max(startPoint.Y, endPoint.Y);

            var width = right - left;
            var height = bottom - top;

            Rectangle element;
            
            if (isFill)
            {
                element = new Rectangle()
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Fill = colorValue.colorValue,
                    Width = width,
                    Height = height
                };
            } else
            {
                element = new Rectangle()
                {
                    Stroke = colorValue.colorValue,
                    StrokeThickness = widthness.widthnessValue,
                    StrokeDashArray = strokeStyle.strokeValue,
                    Width = width,
                    Height = height
                };
            }

            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);

            return element;
        }
    }
}
