using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using System.Windows.Shapes;

namespace myRectangleSelection
{
    public class myRectangleSelection : IShape
    {
        private Point startPoint;
        private Point endPoint;
        public IWidthness getWidthness() { return null; }
        public IStroke getStrokeStyle() { return null; }
        public IColor getColor() { return null; }
        public List<Point> getPointList() { return null; }
        public void setAngle(double angle) {}
        public double getAngle()
        {
            return 0;
        }
        public string shapeName => "Rectangle Selection";
        public string shapeImage => "";

        public void addStartPoint(Point point) { startPoint = point; }
        public void addEndPoint(Point point) { endPoint = point; }
        public void addWidthness(IWidthness width) { }
        public void addStrokeStyle(IStroke stroke) { }
        public void addColor(IColor color) { }
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
        public void setShapeFill(bool isShapeFill) { }
        public void setEdit(bool edit) { }
        public Grid getEditGrid()
        {
            return null;
        }
        public Button getStartButton()
        {
            return null;
        }
        public Button getEndButton()
        {
            return null;
        }
        public Button getLeftTopButton()
        {
            return null;
        }
        public Button getRightTopButton()
        {
            return null;
        }
        public Button getLeftBottomButton()
        {
            return null;
        }
        public Button getRightBottomButton()
        {
            return null;
        }
        public Button getLeftCenterButton()
        {
            return null;
        }
        public Button getRightCenterButton()
        {
            return null;
        }
        public Button getTopCenterButton()
        {
            return null;
        }
        public Button getBottomCenterButton()
        {
            return null;
        }
        public Button getRotateButton()
        {
            return null;
        }
        public void setFlipHorizontally(bool flipHorizontally) { }
        public void setFlipVertically(bool flipVertically) { }
        public bool getFlipHorizontally()
        {
            return false;
        }
        public bool getFlipVertically()
        {
            return false;
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

            element = new Rectangle()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection() { 10, 2 },
                Width = width,
                Height = height
            };

            Canvas.SetLeft(element, left);
            Canvas.SetTop(element, top);

            return element;
        }
    }
}
