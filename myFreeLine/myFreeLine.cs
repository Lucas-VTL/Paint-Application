using myColor;
using myShape;
using myStroke;
using myWidthness;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Point = System.Windows.Point;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace myFreeLine
{
    public class myFreeLine : IShape
    {
        List<Point> list;
        Canvas cloneDrawSurface;
        public string shapeName => "Free Line";
        public string shapeImage => "";

        public void addStartPoint(Point point) {}
        public void addEndPoint(Point point) {}
        public void addWidthness(IWidthness width) {}
        public void addStrokeStyle(IStroke stroke) {}
        public void addColor(IColor color) {}
        public void setShapeFill(bool isShapeFill) {}
        public void addFontSize(int fontSize) {}
        public void addFontFamily(string fontFamily) {}
        public TextBox getTextBox() { return null; }
        public void setTextString(string text) { }
        public void setFocus(bool focus) { }
        public void setBold(bool bold) { }
        public void setItalic(bool italic) { }
        public void setBackground(byte r, byte g, byte b) { }
        public void setEdit(bool edit) {}
        public Grid getEditGrid() { return null; }
        public Button getStartButton() { return null; }
        public Button getEndButton() { return null; }
        public Button getLeftTopButton() { return null; }
        public Button getRightTopButton() { return null; }
        public Button getLeftBottomButton() { return null; }
        public Button getRightBottomButton() { return null; }
        public Button getLeftCenterButton() { return null; }
        public Button getRightCenterButton() { return null; }
        public Button getTopCenterButton() { return null; }
        public Button getBottomCenterButton() { return null; }
        public Button getRotateButton() { return null; }
        public void setFlipHorizontally(bool flipHorizontally) {}
        public void setFlipVertically(bool flipVertically) {}
        public void setAngle(double angle) { }
        public double getAngle()
        {
            return 0;
        }
        public bool getFlipHorizontally()
        {
            return false;
        }
        public bool getFlipVertically()
        {
            return false;
        }
        public Point getStartPoint()
        {
            return new Point();
        }
        public Point getEndPoint()
        {
            return new Point();
        }
        public Point getCenterPoint()
        {
            return new Point();
        }
        public void addPointList(List<Point> pointList) 
        {
            list = new List<Point>(0);
            list.AddRange(pointList);
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public UIElement convertShapeType()
        {
            if (cloneDrawSurface == null)
            {
                cloneDrawSurface = new Canvas();
                cloneDrawSurface.Background = Brushes.Transparent;
                cloneDrawSurface.IsHitTestVisible = false;

                for (int i = 0; i < list.Count; i++)
                {
                    Ellipse dot = new Ellipse();
                    dot.Fill = Brushes.White;
                    dot.Width = dot.Height = 20;
                    Canvas.SetLeft(dot, list[i].X);
                    if (list[i].Y >= 20)
                    {
                        Canvas.SetTop(dot, list[i].Y - 20);
                    }

                    cloneDrawSurface.Children.Add(dot);
                }

                return cloneDrawSurface;
            } else
            {
                return cloneDrawSurface;
            }
        }
    }
}
